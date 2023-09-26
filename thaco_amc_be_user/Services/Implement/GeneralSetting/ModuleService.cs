using AutoMapper;
using Common;
using Common.Commons;
using Common.Params.Base;
using Dapper;
using Repository.CustomModel;
using Repository.Model;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Common;
using UserManagement.Config;
using UserManagement.Models.Main;

namespace UserManagement.Services.Implement.GeneralSetting
{
    public class ModuleService : BaseService, IModuleService
    {
        //private readonly ILogService _logService;
        private readonly IModuleRepository _moduleRepository;
        private readonly IStoreProcedureExecute _storeProcedureExecute;
        public ModuleService(
            //ILogService logService,
            IModuleRepository moduleRepository,
            IStoreProcedureExecute storeProcedureExecute,
            ILogger logger,
            IConfigManager config,
            IMapper mapper) : base(config, logger, mapper)
        {
            //_logService = logService;
            _moduleRepository = moduleRepository;
            _storeProcedureExecute = storeProcedureExecute;
        }
        #region implement

        public async Task<ResponseService<ListResult<ModuleResponse>>> GetAll(PagingParam param)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));

                param.tenant_id = SessionStore.Get<Guid>(Constants.KEY_SESSION_TENANT_ID);

                var modules = await _moduleRepository.GetAll(param);
                ListResult<ModuleResponse> result = _mapper.Map<ListResult<QTTS01_Module>, ListResult<ModuleResponse>>(modules);
                result.items = result.items.OrderBy(x => x.position).ToList();

                return new ResponseService<ListResult<ModuleResponse>>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<ListResult<ModuleResponse>>(ex);
            }
        }

        public async Task<ResponseService<List<ModuleCustomResponse>>> GetAllWithIsShow()
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));

                Guid tenantId = SessionStore.Get<Guid>(Constants.KEY_SESSION_TENANT_ID);
                string username = SessionStore.Get<string>(Constants.KEY_SESSION_USER_ID);
                bool isAdmin = !string.IsNullOrEmpty(SessionStore.Get<string>(Constants.KEY_SESSION_IS_ADMIN)) ? SessionStore.Get<bool>(Constants.KEY_SESSION_IS_ADMIN) : false;

                List<ModuleCustomResponse> data = new List<ModuleCustomResponse>();
                DynamicParameters parameters = new DynamicParameters(
                    new
                    {
                        username,
                        is_admin = isAdmin,
                        tenant_id = tenantId
                    });

                data = await Task.FromResult(_storeProcedureExecute.ExecuteReturnList<ModuleCustomResponse>("usp_Module_Get_All", parameters).Result.ToList());

                return new ResponseService<List<ModuleCustomResponse>>(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<List<ModuleCustomResponse>>(ex);
            }
        }

        public async Task<ResponseService<ModuleResponse>> GetById(Guid id)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                Guid tenantId = SessionStore.Get<Guid>(Constants.KEY_SESSION_TENANT_ID);

                QTTS01_Module data = await _moduleRepository.GetSingle(x => x.id == id && x.tenant_id == tenantId);
                if (data == null)
                {
                    return new ResponseService<ModuleResponse>(Constants.MODULE_NOT_FOUND).BadRequest(MessCodes.MODULE_NOT_FOUND);
                }

                ModuleResponse result = _mapper.Map<QTTS01_Module, ModuleResponse>(data);
                return new ResponseService<ModuleResponse>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<ModuleResponse>(ex);
            }
        }

        public async Task<ResponseService<ModuleResponse>> Create(ModuleRequest obj)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                obj.AddInfo();

                bool checkExistsModuleName = await _moduleRepository.CheckExistsAnyAsync(x => x.module_name.ToLower() == obj.module_name.Trim().ToLower() && x.tenant_id == obj.tenant_id);
                if (checkExistsModuleName)
                {
                    return new ResponseService<ModuleResponse>(Constants.MODULE_NAME_IS_ALREADY_EXISTS).BadRequest(MessCodes.MODULE_NAME_IS_ALREADY_EXISTS);
                }

                QTTS01_Module entity = _mapper.Map<ModuleRequest, QTTS01_Module>(obj);
                await _moduleRepository.Create(entity);

                ModuleResponse result = _mapper.Map<QTTS01_Module, ModuleResponse>(entity);
                // Send data log
                //await _logService.CreateKafkaLog(Constants.LOG_TYPE_CREATE, Constants.MODULE_SERVICE, entity, null);
                return new ResponseService<ModuleResponse>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<ModuleResponse>(ex);
            }
        }

        public async Task<ResponseService<ModuleResponse>> Update(ModuleRequest obj)
        {
            try
            {
                obj.UpdateInfo();
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));

                QTTS01_Module checkExists = await _moduleRepository.GetSingle(x => x.id == obj.id && x.tenant_id == obj.tenant_id);
                if (checkExists == null)
                {
                    return new ResponseService<ModuleResponse>(Constants.MODULE_NOT_FOUND).BadRequest(MessCodes.MODULE_NOT_FOUND);
                }

                bool checkExistsName = await _moduleRepository.CheckExistsAnyAsync(x => x.id != obj.id && x.tenant_id == obj.tenant_id);
                if (checkExists == null)
                {
                    return new ResponseService<ModuleResponse>(Constants.MODULE_NOT_FOUND).BadRequest(MessCodes.MODULE_NOT_FOUND);
                }

                QTTS01_Module entity = _mapper.Map<ModuleRequest, QTTS01_Module>(obj);
                entity.create_by = checkExists.create_by;
                entity.create_time = checkExists.create_time;
                entity.is_active = checkExists.is_active;

                await _moduleRepository.Update(entity, obj.id);
                ModuleResponse result = _mapper.Map<QTTS01_Module, ModuleResponse>(entity);

                // Send data log
                //await _logService.CreateKafkaLog(Constants.LOG_TYPE_UPDATE, Constants.MODULE_SERVICE, checkExists, entity);
                return new ResponseService<ModuleResponse>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<ModuleResponse>(ex);
            }
        }

        public async Task<ResponseService<bool>> Delete(Guid id)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                Guid tenantId = SessionStore.Get<Guid>(Constants.KEY_SESSION_TENANT_ID);

                QTTS01_Module checkExists = await _moduleRepository.GetSingle(x => x.id == id && x.tenant_id == tenantId);
                if (checkExists == null)
                {
                    return new ResponseService<bool>(Constants.MODULE_NOT_FOUND).BadRequest(MessCodes.MODULE_NOT_FOUND);
                }

                await _moduleRepository.DeleteCustom(checkExists);

                // Send data log
                //await _logService.CreateKafkaLog(Constants.LOG_TYPE_DELETE, Constants.MODULE_SERVICE, checkExists, null);
                return new ResponseService<bool>(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<bool>(ex);
            }
        }
        #endregion
    }
}
