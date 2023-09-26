using AutoMapper;
using Common;
using Common.Commons;
using Dapper;
using Repository.CustomModel;
using Repository.Model;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Common;
using UserManagement.Config;
using UserManagement.Models.Main;

namespace UserManagement.Services.Implement.GeneralSetting
{
    public class PermissionObjectService : BaseService, IPermissionObjectService
    {
        //private readonly ILogService _logService;
        private readonly IStoreProcedureExecute _storeProcedureExecute;
        private readonly IPermissionObjectRepository _permissionObjectRepository;
        public PermissionObjectService(
            //ILogService logService,
            IStoreProcedureExecute storeProcedureExecute,
            IPermissionObjectRepository permissionObjectRepository,
            ILogger logger,
            IConfigManager config,
            IMapper mapper) : base(config, logger, mapper)
        {
            //_logService = logService;
            _storeProcedureExecute = storeProcedureExecute;
            _permissionObjectRepository = permissionObjectRepository;
        }
        #region implement
        public async Task<ResponseService<ListResult<PermissionObjectCustom>>> GetAll(PagingRequest<PermissionObjectSearchRequest> param)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                param.tenant_id = SessionStore.Get<Guid>(Constants.KEY_SESSION_TENANT_ID);
                PermissionObjectSearchRequest request = param.request != null ? param.request : new PermissionObjectSearchRequest();
                ListResult<PermissionObjectCustom> response = new ListResult<PermissionObjectCustom>();
                DynamicParameters parameters = new DynamicParameters(
                    new
                    {
                        objectName = request.object_name,
                        moduleName = request.module_name,
                        description = request.description,
                        createBy = request.create_by,
                        createTime = request.create_time,
                        page = param.page,
                        limit = param.limit,
                        tenantId = param.tenant_id
                    });

                parameters.Add("totalRow", dbType: DbType.Int32, direction: ParameterDirection.Output);

                response.items = await Task.FromResult(_storeProcedureExecute.ExecuteReturnList<PermissionObjectCustom>("usp_PermissionObject_Get_All", parameters).Result.ToList());
                response.total = parameters.Get<int>("totalRow");

                return new ResponseService<ListResult<PermissionObjectCustom>>(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<ListResult<PermissionObjectCustom>>(ex);
            }
        }

        public async Task<ResponseService<PermissionObjectResponse>> GetById(Guid id)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                Guid tenantId = SessionStore.Get<Guid>(Constants.KEY_SESSION_TENANT_ID);

                QTTS01_PermissionObject data = await _permissionObjectRepository.GetSingle(x => x.id == id && x.tenant_id == tenantId);

                PermissionObjectResponse result = _mapper.Map<QTTS01_PermissionObject, PermissionObjectResponse>(data);
                return new ResponseService<PermissionObjectResponse>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<PermissionObjectResponse>(ex);
            }
        }
        public async Task<ResponseService<PermissionObjectResponse>> Create(PermissionObjectRequest request)
        {
            try
            {
                request.AddInfo();
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));

                bool checkExistsModule = await _permissionObjectRepository.CheckExistsModule(request.tenant_id, request.module_id);
                if (!checkExistsModule)
                {
                    return new ResponseService<PermissionObjectResponse>(Constants.MODULE_NOT_FOUND).BadRequest(MessCodes.MODULE_NOT_FOUND);
                }

                bool checkExistsPermissionName = await _permissionObjectRepository.CheckExistsAnyAsync(x => x.tenant_id == request.tenant_id && x.module_id == request.module_id && x.object_name.ToLower() == request.object_name.Trim().ToLower());
                if (checkExistsPermissionName)
                {
                    return new ResponseService<PermissionObjectResponse>(Constants.PERMISSION_OBJECT_HAS_ALREADY_EXISTED).BadRequest(MessCodes.PERMISSION_OBJECT_HAS_ALREADY_EXISTED);
                }

                // Get list profile by tenant
                List<Guid> profileByTenant = await _permissionObjectRepository.GetListProfileByTenant(request.tenant_id);
                List<QTTS01_Permission> permissionEntities = new List<QTTS01_Permission>();
                foreach (var item in profileByTenant)
                {
                    QTTS01_Permission permissionEntity = new QTTS01_Permission();
                    permissionEntity.id = Guid.NewGuid();
                    permissionEntity.profile_id = item;
                    permissionEntity.permissionobject_id = request.id;
                    permissionEntity.object_name = request.object_name;
                    permissionEntity.is_active = request.is_active;
                    permissionEntity.is_allow_access = true;
                    permissionEntity.is_allow_create = true;
                    permissionEntity.is_allow_delete = true;
                    permissionEntity.is_allow_edit = true;
                    permissionEntity.create_by = request.create_by;
                    permissionEntity.modify_by = request.modify_by;
                    permissionEntity.create_time = request.create_time;
                    permissionEntity.modify_time = request.modify_time;
                    permissionEntity.tenant_id = request.tenant_id;

                    permissionEntities.Add(permissionEntity);
                }

                QTTS01_PermissionObject permissionObjectEntity = _mapper.Map<PermissionObjectRequest, QTTS01_PermissionObject>(request);
                await _permissionObjectRepository.CreateCustom(permissionObjectEntity, permissionEntities);

                PermissionObjectResponse result = _mapper.Map<QTTS01_PermissionObject, PermissionObjectResponse>(permissionObjectEntity);

                // Send data log
                //await _logService.CreateKafkaLog(Constants.LOG_TYPE_CREATE, Constants.PERMISSION_OBJECT_SERVICE, request, null);
                return new ResponseService<PermissionObjectResponse>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<PermissionObjectResponse>(ex);
            }
        }
        public async Task<ResponseService<PermissionObjectResponse>> Update(PermissionObjectRequest request)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                request.UpdateInfo();

                QTTS01_PermissionObject checkExists = await _permissionObjectRepository.GetSingle(x => x.id == request.id && x.tenant_id == request.tenant_id);
                if (checkExists == null)
                {
                    return new ResponseService<PermissionObjectResponse>(Constants.PERMISSION_OBJECT_NOT_FOUND).BadRequest(MessCodes.PERMISSION_OBJECT_NOT_FOUND);
                }

                bool checkExistsModule = await _permissionObjectRepository.CheckExistsModule(request.tenant_id, request.module_id);
                if (!checkExistsModule)
                {
                    return new ResponseService<PermissionObjectResponse>(Constants.MODULE_NOT_FOUND).BadRequest(MessCodes.MODULE_NOT_FOUND);
                }

                bool checkExistsPermissionName = await _permissionObjectRepository.CheckExistsAnyAsync(x => x.id != request.id && x.tenant_id == request.tenant_id && x.module_id == request.module_id && x.object_name.ToLower() == request.object_name.Trim().ToLower());
                if (checkExistsPermissionName)
                {
                    return new ResponseService<PermissionObjectResponse>(Constants.PERMISSION_OBJECT_HAS_ALREADY_EXISTED).BadRequest(MessCodes.PERMISSION_OBJECT_HAS_ALREADY_EXISTED);
                }

                QTTS01_PermissionObject permissionObjectEntity = _mapper.Map<PermissionObjectRequest, QTTS01_PermissionObject>(request);
                permissionObjectEntity.create_by = checkExists.create_by;
                permissionObjectEntity.create_time = checkExists.create_time;

                // Case change permission object name or update is_active
                if (request.object_name.Trim().ToLower() != checkExists.object_name.ToLower() || request.is_active != checkExists.is_active)
                {
                    List<QTTS01_Permission> permissionEntities = await _permissionObjectRepository.GetListPermissionByPermissionObject(request.id, request.tenant_id);

                    foreach (var item in permissionEntities)
                    {
                        item.is_active = request.is_active;
                        item.object_name = request.object_name.Trim();
                        item.modify_by = request.modify_by;
                        item.modify_time = request.modify_time;
                    }

                    await _permissionObjectRepository.UpdateCustom(permissionObjectEntity, permissionEntities);
                }
                else
                {
                    await _permissionObjectRepository.Update(permissionObjectEntity, request.id);
                }

                PermissionObjectResponse result = _mapper.Map<QTTS01_PermissionObject, PermissionObjectResponse>(permissionObjectEntity);

                // Send data log
                //await _logService.CreateKafkaLog(Constants.LOG_TYPE_UPDATE, Constants.PERMISSION_OBJECT_SERVICE, checkExists, result);
                return new ResponseService<PermissionObjectResponse>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<PermissionObjectResponse>(ex);
            }
        }


        public async Task<ResponseService<bool>> Delete(Guid id)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                Guid tenantId = SessionStore.Get<Guid>(Constants.KEY_SESSION_TENANT_ID);

                QTTS01_PermissionObject checkExists = await _permissionObjectRepository.GetSingle(x => x.id == id && x.tenant_id == tenantId);
                if (checkExists == null)
                {
                    return new ResponseService<bool>(Constants.DATA_NOT_FOUND).BadRequest(MessCodes.DATA_NOT_FOUND);
                }

                List<QTTS01_Permission> permissionEntities = await _permissionObjectRepository.GetListPermissionByPermissionObject(id, tenantId);

                await _permissionObjectRepository.DeleteCustom(checkExists, permissionEntities);

                // Send data log
                //await _logService.CreateKafkaLog(Constants.LOG_TYPE_DELETE, Constants.PERMISSION_OBJECT_SERVICE, checkExists, null);
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
