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
using System.Threading.Tasks;
using UserManagement.Common;
using UserManagement.Config;
using UserManagement.Models.Main;

namespace UserManagement.Services.Implement.GeneralSetting
{
    public class BankService : BaseService, IBankService
    {
        //private readonly ILogService _logService;
        private readonly IBankRepository _bankRepository;
        public BankService(
            //ILogService logService,
            IBankRepository bankRepository,
            ILogger logger,
            IConfigManager config,
            IMapper mapper) : base(config, logger, mapper)
        {
            //_logService = logService;
            _bankRepository = bankRepository;
        }

        public async Task<ResponseService<ListResult<BankResponse>>> GetAll(PagingParam param)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));

                param.tenant_id = SessionStore.Get<Guid>(Constants.KEY_SESSION_TENANT_ID);
                string currentUser = SessionStore.Get<string>(Constants.KEY_SESSION_USER_ID);

                ListResult<QTTS01_Bank> bankModel = await _bankRepository.GetAll(param);
                ListResult<BankResponse> result = _mapper.Map<ListResult<QTTS01_Bank>, ListResult<BankResponse>>(bankModel);

                return new ResponseService<ListResult<BankResponse>>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<ListResult<BankResponse>>(ex);
            }
        }

        public async Task<ResponseService<BankResponse>> GetById(Guid id)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                Guid tenantId = SessionStore.Get<Guid>(Constants.KEY_SESSION_TENANT_ID);

                QTTS01_Bank data = await _bankRepository.GetSingle(x => x.ID == id && x.tenant_id == tenantId);
                BankResponse result = _mapper.Map<QTTS01_Bank, BankResponse>(data);

                return new ResponseService<BankResponse>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<BankResponse>(ex);
            }
        }

        public async Task<ResponseService<BankResponse>> Create(BankRequest request)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                request.AddInfo();

                bool checkExistsProfileName = await _bankRepository.CheckExistsAnyAsync(x => x.tenant_id == request.tenant_id && x.bank_name.ToLower() == request.bank_name.Trim().ToLower());
                if (checkExistsProfileName)
                {
                    return new ResponseService<BankResponse>(Constants.PROFILE_HAS_ALREADY_EXISTED).BadRequest(MessCodes.PROFILE_HAS_ALREADY_EXISTED);
                }

                // Profile entity
                QTTS01_Bank profileEntity = _mapper.Map<BankRequest, QTTS01_Bank>(request);
                // Permission object entity
                List<QTTS01_Permission> permissionEntities = new List<QTTS01_Permission>();
                List<QTTS01_PermissionObject> permissionObjectList = await _bankRepository.GetPermissionObjectListByTenant(request.tenant_id);
                foreach (QTTS01_PermissionObject permissionObject in permissionObjectList)
                {
                    QTTS01_Permission permissionEntity = new QTTS01_Permission();
                    permissionEntity.id = Guid.NewGuid();
                    permissionEntity.profile_id = profileEntity.ID;
                    permissionEntity.permissionobject_id = permissionObject.id;
                    permissionEntity.object_name = permissionObject.object_name;
                    permissionEntity.description = permissionObject.object_name;
                    permissionEntity.is_allow_create = true;
                    permissionEntity.is_allow_edit = true;
                    permissionEntity.is_allow_delete = true;
                    permissionEntity.is_allow_access = true;
                    permissionEntity.is_active = true;
                    permissionEntity.create_time = profileEntity.create_time;
                    permissionEntity.create_by = profileEntity.create_by;
                    permissionEntity.modify_time = profileEntity.create_time;
                    permissionEntity.modify_by = profileEntity.create_by;
                    permissionEntity.tenant_id = profileEntity.tenant_id;

                    permissionEntities.Add(permissionEntity);
                }

                await _bankRepository.CreateCustom(profileEntity, permissionEntities);

                ProfileResponse result = _mapper.Map<QTTS01_Profile, ProfileResponse>(profileEntity);

                // Send data log
                //await _logService.CreateKafkaLog(Constants.LOG_TYPE_CREATE, Constants.PROFILE_SERVICE, request, null);
                return new ResponseService<ProfileResponse>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<ProfileResponse>(ex);
            }
        }
    }
}
