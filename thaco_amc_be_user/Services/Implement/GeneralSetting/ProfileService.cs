using AutoMapper;
using Common;
using Common.Commons;
using Common.Params.Base;
using Dapper;
using Repository.CustomModel;
using Repository.Model;
using Repository.Queries;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Common;
using UserManagement.Config;
using UserManagement.Models.Main;
using IStoreProcedureExecute = Repository.Repositories.IStoreProcedureExecute;

namespace UserManagement.Services.Implement.GeneralSetting
{
    public class ProfileService : BaseService, IProfileService
    {
        //private readonly ILogService _logService;
        private ConditionLinq<UserModel> _conditionLinq;
        private readonly IProfileRepository _profileRepository;
        private readonly IStoreProcedureExecute _storeProcedureExecute;
        public ProfileService(
            //ILogService logService,
            IProfileRepository profileRepository,
            IStoreProcedureExecute storeProcedureExecute,
            ILogger logger,
            IConfigManager config,
            IMapper mapper) : base(config, logger, mapper)
        {
            //_logService = logService;
            _profileRepository = profileRepository;
            _storeProcedureExecute = storeProcedureExecute;
            _conditionLinq = new ConditionLinq<UserModel>();
        }
        #region implement
        public async Task<ResponseService<ListResult<ProfileResponse>>> GetAll(PagingParam param)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));

                param.tenant_id = SessionStore.Get<Guid>(Constants.KEY_SESSION_TENANT_ID);
                ListResult<QTTS01_Profile> data = await _profileRepository.GetAll(param);
                ListResult<ProfileResponse> result = _mapper.Map<ListResult<QTTS01_Profile>, ListResult<ProfileResponse>>(data);

                return new ResponseService<ListResult<ProfileResponse>>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<ListResult<ProfileResponse>>(ex);
            }
        }

        public async Task<ResponseService<ProfileResponse>> GetById(Guid id)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                Guid tenantId = SessionStore.Get<Guid>(Constants.KEY_SESSION_TENANT_ID);

                QTTS01_Profile data = await _profileRepository.GetSingle(x => x.id == id && x.tenant_id == tenantId);
                ProfileResponse result = _mapper.Map<QTTS01_Profile, ProfileResponse>(data);

                return new ResponseService<ProfileResponse>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<ProfileResponse>(ex);
            }
        }
        public async Task<ResponseService<ListResult<UserModel>>> GetListUserByProfile(PagingParam param)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                ListResult<UserModel> response = new ListResult<UserModel>();

                param.tenant_id = SessionStore.Get<Guid>(Constants.KEY_SESSION_TENANT_ID);
                string currentUser = SessionStore.Get<string>(Constants.KEY_SESSION_USER_ID);

                ListResult<UserModel> res = await _profileRepository.GetUsersByProfile(param);
                //List<UserRoleResponse> listUser = await _roleHierarchyRepository.GetListUserAgent(currentUser, param.tenant_id);
                UserModel usercurrentfound = res.items.Find(x => x.username.Equals(currentUser));
                response.items = new List<UserModel>();

                if (usercurrentfound != null)
                {
                    response.items.Add(usercurrentfound);
                }

                //foreach (var item in listUser)
                //{
                //    if (res.items.Any())
                //    {
                //        var userfound = res.items.Find(x => x.username.Equals(item.username));
                //        if (userfound != null)
                //        {
                //            response.items.Add(userfound);
                //        }
                //    }
                //}

                response = _conditionLinq.Process(param, response.items);
                return new ResponseService<ListResult<UserModel>>(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<ListResult<UserModel>>(ex);
            }
        }

        public async Task<ResponseService<ProfileResponse>> Create(ProfileRequest request)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                request.AddInfo();

                bool checkExistsProfileName = await _profileRepository.CheckExistsAnyAsync(x => x.tenant_id == request.tenant_id && x.profile_name.ToLower() == request.profile_name.Trim().ToLower());
                if (checkExistsProfileName)
                {
                    return new ResponseService<ProfileResponse>(Constants.PROFILE_HAS_ALREADY_EXISTED).BadRequest(MessCodes.PROFILE_HAS_ALREADY_EXISTED);
                }

                // Profile entity
                QTTS01_Profile profileEntity = _mapper.Map<ProfileRequest, QTTS01_Profile>(request);
                // Permission object entity
                List<QTTS01_Permission> permissionEntities = new List<QTTS01_Permission>();
                List<QTTS01_PermissionObject> permissionObjectList = await _profileRepository.GetPermissionObjectListByTenant(request.tenant_id);
                foreach (QTTS01_PermissionObject permissionObject in permissionObjectList)
                {
                    QTTS01_Permission permissionEntity = new QTTS01_Permission();
                    permissionEntity.id = Guid.NewGuid();
                    permissionEntity.profile_id = profileEntity.id;
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

                await _profileRepository.CreateCustom(profileEntity, permissionEntities);

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

        public async Task<ResponseService<ProfileResponse>> Update(ProfileRequest request)
        {
            try
            {
                request.UpdateInfo();
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));

                QTTS01_Profile checkExists = await _profileRepository.GetSingle(x => x.id == request.id && x.tenant_id == request.tenant_id);
                if (checkExists == null)
                {
                    return new ResponseService<ProfileResponse>(Constants.PROFILE_NOT_FOUND).BadRequest(MessCodes.PROFILE_NOT_FOUND);
                }

                bool checkExistsProfileName = await _profileRepository.CheckExistsAnyAsync(x => x.id != request.id && x.tenant_id == request.tenant_id && x.profile_name.ToLower() == request.profile_name.Trim().ToLower());
                if (checkExistsProfileName)
                {
                    return new ResponseService<ProfileResponse>(Constants.PROFILE_HAS_ALREADY_EXISTED).BadRequest(MessCodes.PROFILE_HAS_ALREADY_EXISTED);
                }

                if (checkExists.profile_name == Constants.PROFILE_ADMIN)
                {
                    return new ResponseService<ProfileResponse>(Constants.ADMIN_PROFILE_CANNOT_BE_UPDATED).BadRequest(MessCodes.ADMIN_PROFILE_CANNOT_BE_UPDATED);
                }

                QTTS01_Profile profileEntity = _mapper.Map<ProfileRequest, QTTS01_Profile>(request);
                profileEntity.create_by = checkExists.create_by;
                profileEntity.create_time = checkExists.create_time;

                await _profileRepository.Update(profileEntity, request.id);
                ProfileResponse response = _mapper.Map<QTTS01_Profile, ProfileResponse>(profileEntity);

                // Send data log
                //await _logService.CreateKafkaLog(Constants.LOG_TYPE_UPDATE, Constants.PROFILE_SERVICE, checkExists, profileEntity);
                return new ResponseService<ProfileResponse>(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<ProfileResponse>(ex);
            }
        }

        public async Task<ResponseService<bool>> Delete(Guid id)
        {
            _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
            Guid tenantId = SessionStore.Get<Guid>(Constants.KEY_SESSION_TENANT_ID);

            var checkExists = await _profileRepository.GetSingle(x => x.id == id && x.tenant_id == tenantId);
            if (checkExists == null)
            {
                return new ResponseService<bool>(Constants.PROFILE_NOT_FOUND).BadRequest(MessCodes.PROFILE_NOT_FOUND);
            }

            if (checkExists.profile_name == Constants.PROFILE_ADMIN)
            {
                return new ResponseService<bool>(Constants.ADMIN_PROFILE_CANNOT_BE_DELETED).BadRequest(MessCodes.ADMIN_PROFILE_CANNOT_BE_DELETED);
            }

            await _profileRepository.DeleteProfile(checkExists);

            // Send data log
            //await _logService.CreateKafkaLog(Constants.LOG_TYPE_DELETE, Constants.PROFILE_SERVICE, checkExists, null);
            return new ResponseService<bool>(true);
        }

        public async Task<ResponseService<bool>> UpdatePermissionInProfile(List<UpdatePermissonRequest> request)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                DateTime currentDateTime = DateTime.Now;
                Guid tenantId = SessionStore.Get<Guid>(Constants.KEY_SESSION_TENANT_ID);
                string currentUser = SessionStore.Get<string>(Constants.KEY_SESSION_USER_ID);

                List<Guid> permissionIdList = request.Select(x => x.id).ToList();
                Guid profileId = request.Select(x => x.profile_id).FirstOrDefault();
                List<QTTS01_Permission> permissionList = await _profileRepository.GetListPermissionInProfile(permissionIdList, profileId, tenantId);

                List<QTTS01_Permission> updateEntities = new List<QTTS01_Permission>();
                foreach (var item in request)
                {
                    var permissionEntity = permissionList.FirstOrDefault(x => x.id == item.id);
                    if (permissionEntity != null)
                    {
                        permissionEntity.is_show = item.is_show;
                        permissionEntity.is_allow_access = item.is_allow_access;
                        permissionEntity.is_allow_create = item.is_allow_create;
                        permissionEntity.is_allow_delete = item.is_allow_delete;
                        permissionEntity.is_allow_edit = item.is_allow_edit;
                        permissionEntity.modify_by = currentUser;
                        permissionEntity.modify_time = currentDateTime;

                        updateEntities.Add(permissionEntity);
                    }
                }

                await _profileRepository.UpdateListPermissionInProfile(updateEntities);

                // Send data log
                //await _logService.CreateKafkaLog(Constants.LOG_TYPE_UPDATE, Constants.UPDATED_PERMISSION_ON_PROFILE_SUCCESSFULLY, Constants.PROFILE_SERVICE, request, updateEntities);
                return new ResponseService<bool>(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<bool>(ex);
            }
        }

        public async Task<ResponseService<bool>> DeleteUserInProfile(DeleteUserInProfile request)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                Guid tenandId = SessionStore.Get<Guid>(Constants.KEY_SESSION_TENANT_ID);

                QTTS01_Profile checkExistsProfile = await _profileRepository.GetSingle(x => x.id == request.profile_id && x.tenant_id == tenandId);
                if (checkExistsProfile == null)
                {
                    return new ResponseService<bool>(Constants.PROFILE_NOT_FOUND).BadRequest(MessCodes.PROFILE_NOT_FOUND);
                }

                // Administrator không thể xóa khỏi admin profile
                //if (checkExistsProfile.profile_name == Constants.PROFILE_ADMIN)
                //{
                //    bool checkUserIsAdmin = await _profileRepository.CheckUserRemoveIsAdmin(tenandId, request.username);
                //    if (checkUserIsAdmin)
                //    {
                //        return new ResponseService<bool>(Constants.ADMINISTRATOR_CANNOT_BE_REMOVED_ADMIN_PROFILE).BadRequest(MessCodes.PROFILE_NOT_FOUND);
                //    }
                //}

                QTTS01_MapProfileUser checkExistsDataRemove = await _profileRepository.CheckExistsDataRemove(tenandId, request.username, request.profile_id);
                if (checkExistsDataRemove == null)
                {
                    return new ResponseService<bool>(Constants.DATA_NOT_FOUND).BadRequest(MessCodes.DATA_NOT_FOUND);
                }

                await _profileRepository.DeleteUserInProfile(checkExistsDataRemove);

                // Send data log
                //await _logService.CreateKafkaLog(Constants.LOG_TYPE_UPDATE, Constants.REMOVE_USER_FROM_PROFILE_SUCCESSFULLY, Constants.PROFILE_SERVICE, request, null);
                return new ResponseService<bool>(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<bool>(ex);
            }
        }
        #endregion

        #region for root user
        //public async Task<ResponseService<ListResult<UserInProfileCustomResponse>>> GetRootUserByProfile(PagingRequest<UserInProfileSearchRequest> param)
        //{
        //    try
        //    {
        //        _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));

        //        bool isRoot = !string.IsNullOrEmpty(SessionStore.Get<string>(Constants.KEY_SESSION_IS_ROOT)) ? SessionStore.Get<bool>(Constants.KEY_SESSION_IS_ROOT) : false;
        //        if (!isRoot)
        //        {
        //            return new ResponseService<ListResult<UserInProfileCustomResponse>>(Constants.ONLY_ROOT_USER_CAN_DO_THIS).BadRequest(MessCodes.ONLY_ROOT_USER_CAN_DO_THIS);
        //        }

        //        param.tenant_id = SessionStore.Get<Guid>(Constants.KEY_SESSION_TENANT_ID);
        //        ListResult<UserInProfileCustomResponse> response = new ListResult<UserInProfileCustomResponse>();
        //        UserInProfileSearchRequest request = param.request != null ? param.request : new UserInProfileSearchRequest();

        //        DynamicParameters parameters = new DynamicParameters(
        //            new
        //            {
        //                userName = request.username,
        //                fullName = request.fullname,
        //                phone = request.phone,
        //                profileId = request.profile_id,
        //                tenantId = param.tenant_id
        //            });

        //        response.items = await Task.FromResult(_storeProcedureExecute.ExecuteReturnList<UserInProfileCustomResponse>("usp_Profile_Get_Root_User_In_Profile", parameters).Result.ToList());
        //        response.total = response.items.Count;

        //        return new ResponseService<ListResult<UserInProfileCustomResponse>>(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex);
        //        return new ResponseService<ListResult<UserInProfileCustomResponse>>(ex);
        //    }
        //}
        #endregion
    }
}
