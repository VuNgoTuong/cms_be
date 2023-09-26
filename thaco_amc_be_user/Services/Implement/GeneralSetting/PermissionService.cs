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
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Common;
using UserManagement.Config;
using UserManagement.Models.Main;

namespace UserManagement.Services.Implement.GeneralSetting
{
    public class PermissionService : BaseService, IPermissionService
    {
        //private readonly ILogService _logService;
        private readonly IPermissionRepository _permissionRepository;
        private readonly IStoreProcedureExecute _storeProcedureExecute;
        public PermissionService(
            //ILogService logService,
            IPermissionRepository permissionRepository,
            IStoreProcedureExecute storeProcedureExecute,
            ILogger logger,
            IConfigManager config,
            IMapper mapper) : base(config, logger, mapper)
        {
            //_logService = logService;
            _permissionRepository = permissionRepository;
            _storeProcedureExecute = storeProcedureExecute;
        }
        #region implement
        public async Task<ResponseService<ListResult<PermissionResponse>>> GetAll(PagingParam param)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                param.tenant_id = SessionStore.Get<Guid>(Constants.KEY_SESSION_TENANT_ID);

                var data = await _permissionRepository.GetAll(param);
                ListResult<PermissionResponse> result = _mapper.Map<ListResult<QTTS01_Permission>, ListResult<PermissionResponse>>(data);

                return new ResponseService<ListResult<PermissionResponse>>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<ListResult<PermissionResponse>>(ex);
            }
        }

        public async Task<ResponseService<PermissionResponse>> GetById(Guid id)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                Guid tenantId = SessionStore.Get<Guid>(Constants.KEY_SESSION_TENANT_ID);

                QTTS01_Permission data = await _permissionRepository.GetSingle(x => x.id == id && x.tenant_id == tenantId);
                if (data == null)
                {
                    return new ResponseService<PermissionResponse>(Constants.DATA_NOT_FOUND).BadRequest(MessCodes.DATA_NOT_FOUND);
                }

                PermissionResponse result = _mapper.Map<QTTS01_Permission, PermissionResponse>(data);
                return new ResponseService<PermissionResponse>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<PermissionResponse>(ex);
            }
        }

        public async Task<ResponseService<PermissionResponse>> Create(PermissionRequest request)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                request.AddInfo();

                bool checkExistsObjectName = await _permissionRepository.CheckExistsAnyAsync(x => x.object_name.ToLower() == request.object_name.Trim().ToLower() && x.profile_id == request.profile_id && x.tenant_id == request.tenant_id);
                if (checkExistsObjectName)
                {
                    return new ResponseService<PermissionResponse>(Constants.OBJECT_NAME_HAS_ALREADY_EXISTED).BadRequest(MessCodes.OBJECT_NAME_HAS_ALREADY_EXISTED);
                }

                QTTS01_Permission entity = _mapper.Map<PermissionRequest, QTTS01_Permission>(request);
                await _permissionRepository.Create(entity);

                PermissionResponse response = _mapper.Map<QTTS01_Permission, PermissionResponse>(entity);

                // Send data log
                //await _logService.CreateKafkaLog(Constants.LOG_TYPE_CREATE, Constants.PERMISSION_SERVICE, request, null);
                return new ResponseService<PermissionResponse>(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<PermissionResponse>(ex);
            }
        }

        public async Task<ResponseService<PermissionResponse>> Update(PermissionRequest request)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                request.UpdateInfo();

                QTTS01_Permission checkExists = await _permissionRepository.GetSingle(x => x.id == request.id && x.tenant_id == request.tenant_id);
                if (checkExists == null)
                {
                    return new ResponseService<PermissionResponse>(Constants.DATA_NOT_FOUND).BadRequest(MessCodes.DATA_NOT_FOUND);
                }

                bool checkExistsObjectName = await _permissionRepository.CheckExistsAnyAsync(x => x.id != request.id && x.tenant_id == request.tenant_id && x.object_name.ToLower() == request.object_name.Trim().ToLower());
                if (checkExistsObjectName)
                {
                    return new ResponseService<PermissionResponse>(Constants.OBJECT_NAME_HAS_ALREADY_EXISTED).BadRequest(MessCodes.OBJECT_NAME_HAS_ALREADY_EXISTED);
                }

                bool checkExistsProfile = await _permissionRepository.CheckExistsProfile(request.tenant_id, request.profile_id);
                if (!checkExistsProfile)
                {
                    return new ResponseService<PermissionResponse>(Constants.PROFILE_NOT_FOUND).BadRequest(MessCodes.PROFILE_NOT_FOUND);
                }

                bool checkExistsPermissionObject = await _permissionRepository.CheckExistsPermissionObject(request.tenant_id, request.permissionobject_id);
                if (!checkExistsPermissionObject)
                {
                    return new ResponseService<PermissionResponse>(Constants.PERMISSION_OBJECT_NOT_FOUND).BadRequest(MessCodes.PERMISSION_OBJECT_NOT_FOUND);
                }

                QTTS01_Permission entity = _mapper.Map<PermissionRequest, QTTS01_Permission>(request);
                entity.create_by = checkExists.create_by;
                entity.create_time = checkExists.create_time;

                await _permissionRepository.Update(entity, request.id);
                PermissionResponse response = _mapper.Map<QTTS01_Permission, PermissionResponse>(entity);

                // Send data log
                //await _logService.CreateKafkaLog(Constants.LOG_TYPE_UPDATE, Constants.PERMISSION_SERVICE, checkExists, entity);
                return new ResponseService<PermissionResponse>(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<PermissionResponse>(ex);
            }
        }

        public async Task<ResponseService<bool>> Delete(Guid id)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                Guid tenantId = SessionStore.Get<Guid>(Constants.KEY_SESSION_TENANT_ID);

                QTTS01_Permission checkExists = await _permissionRepository.GetSingle(x => x.id == id && x.tenant_id == tenantId);
                if (checkExists == null)
                {
                    return new ResponseService<bool>(Constants.DATA_NOT_FOUND).BadRequest(MessCodes.DATA_NOT_FOUND);
                }

                bool result = await _permissionRepository.Delete(id);

                // Send data log
                //await _logService.CreateKafkaLog(Constants.LOG_TYPE_DELETE, Constants.PERMISSION_SERVICE, checkExists, null);
                return new ResponseService<bool>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<bool>(ex);
            }
        }

        public async Task<ResponseService<ListResult<PermissionResponse>>> GetPermissionListByProfile(PagingRequest<PermissonSearchRequest> param)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                param.tenant_id = SessionStore.Get<Guid>(Constants.KEY_SESSION_TENANT_ID);

                PermissonSearchRequest request = param.request != null ? param.request : new PermissonSearchRequest();
                ListResult<PermissionResponse> response = new ListResult<PermissionResponse>();

                DynamicParameters parameters = new DynamicParameters(
                    new
                    {
                        profileId = request.profile_id,
                        objectName = request.object_name,
                        tenantId = param.tenant_id.ToString().ToUpper(),
                        page = param.page,
                        limit = param.limit
                    });

                parameters.Add("totalRow", dbType: DbType.Int32, direction: ParameterDirection.Output);

                response.items = await Task.FromResult(_storeProcedureExecute.ExecuteReturnList<PermissionResponse>("usp_Profile_Get_Permission_By_Profile", parameters).Result.ToList());
                response.total = parameters.Get<int>("totalRow");

                return new ResponseService<ListResult<PermissionResponse>>(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<ListResult<PermissionResponse>>(ex);
            }
        }

        public async Task<ResponseService<List<PermissionResShort>>> GetListPermissionByUser(string username)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                Guid tenantId = SessionStore.Get<Guid>(Constants.KEY_SESSION_TENANT_ID);
                bool isRoot = !string.IsNullOrEmpty(SessionStore.Get<string>(Constants.KEY_SESSION_IS_ROOT)) ? SessionStore.Get<bool>(Constants.KEY_SESSION_IS_ROOT) : false;
                bool isAdmin = !string.IsNullOrEmpty(SessionStore.Get<string>(Constants.KEY_SESSION_IS_ADMIN)) ? SessionStore.Get<bool>(Constants.KEY_SESSION_IS_ADMIN) : false;
                List<PermissionResShort> result = await _permissionRepository.GetListPermissionByUser(username, isRoot, isAdmin);
                return new ResponseService<List<PermissionResShort>>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<List<PermissionResShort>>(ex);
            }
        }

        public async Task<PermissionResShort> GetPermissionAObjectByUser(PermissionAObjectRequest request)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                PermissionResShort result = CommonFuncMain.ToObject<PermissionResShort>(await _permissionRepository.GetPermissionAObjectByUser(request));
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new PermissionResShort();
            }
        }

        public async Task<ResponseService<bool>> GetStatusPermissionByTypeAndName(GetPermissionByTypeAndName request)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                Guid tenantId = SessionStore.Get<Guid>(Constants.KEY_SESSION_TENANT_ID);
                DynamicParameters parameters = new DynamicParameters(
                new
                {
                    username = request.username,
                    tenantId = tenantId,
                    permissionName = request.permission_name,
                    permissionType = request.permission_type,
                    rootPermission = string.Join(',', Constants.ROOT_PERMISSIONS)
                });

                bool result = await _storeProcedureExecute.ExecuteReturnSingle<bool>("usp_Auth_Check_Permision_By_User", parameters);

                return new ResponseService<bool>(result);
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
