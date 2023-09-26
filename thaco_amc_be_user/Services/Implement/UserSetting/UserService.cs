using AutoMapper;
using Common;
using Common.Commons;
using Common.Params.Base;
using Dapper;
using Microsoft.AspNetCore.Hosting;
using Repository.CustomModel;
using Repository.Model;
using Repository.Repositories;
using System;
using System.IO;
using System.Threading.Tasks;
using UserManagement.Common;
using UserManagement.Config;
using UserManagement.Models.Main;
using UserManagement.Services.Implement.GeneralSetting;

namespace UserManagement.Services.Implement.UserSetting
{
    public class UserService : BaseService, IUserService
    {
        //private readonly ILogService _logService;
        private readonly IUserRepository _userRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IStoreProcedureExecute _storeProcedureExecute;
        private readonly string urlKongBCC = ConfigHelper.Get(Constants.CONF_LINK_USER_QTTS);
        public UserService(
            IUserRepository userRepository,
            IWebHostEnvironment webHostEnvironment,
            IStoreProcedureExecute storeProcedureExecute,
            ILogger logger,
            IConfigManager config,
            IMapper mapper
           ) : base(config, logger, mapper)
        {
            _userRepository = userRepository;
            _webHostEnvironment = webHostEnvironment;
            _storeProcedureExecute = storeProcedureExecute;
            //_roleHierarchyRepository = roleHierarchyRepository;
        }

        public async Task<ResponseService<ListResult<UserCustomResponse>>> GetAll(PagingParam param)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));

                param.tenant_id = SessionStore.Get<Guid>(Constants.KEY_SESSION_TENANT_ID);
                string currentUser = SessionStore.Get<string>(Constants.KEY_SESSION_USER_ID);

                ListResult<UserCustomResponse> result = await _userRepository.GetListUser(param, currentUser);

                return new ResponseService<ListResult<UserCustomResponse>>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<ListResult<UserCustomResponse>>(ex);
            }
        }
        public async Task<ResponseService<UserCustomResponse>> GetById(string username)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                Guid tenantId = SessionStore.Get<Guid>(Constants.KEY_SESSION_TENANT_ID);

                UserCustomResponse response = new UserCustomResponse();
                DynamicParameters parameters = new DynamicParameters(
                    new
                    {
                        username,
                        tenant_id = tenantId
                    });

                response = await Task.FromResult(_storeProcedureExecute.ExecuteReturnSingle<UserCustomResponse>("usp_User_Get_User_Information", parameters).Result);

                return new ResponseService<UserCustomResponse>(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<UserCustomResponse>(ex);
            }
        }


        public async Task<ResponseService<UserCustomResponse>> Create(UserRequest obj)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                obj.AddInfo();

                UploadFileReponse generateDefaultAvatar = GenerateAvatar(obj.username, obj.tenant_id);
                obj.avatar = generateDefaultAvatar.file;
                string accessKey = Guid.NewGuid().ToString();
                obj.password = HashString.StringToHash(obj.password, Constants.HASH_SHA512);

                // Add agent to database
                QTTS01_User userEntity = _mapper.Map<UserRequest, QTTS01_User>(obj);

                await _userRepository.CreateAndMapUserToProfile(userEntity);

                // Model response
                UserCustomResponse result = _mapper.Map<QTTS01_User, UserCustomResponse>(userEntity);

                return new ResponseService<UserCustomResponse>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<UserCustomResponse>(ex);
            }
        }

        public async Task<ResponseService<UserCustomResponse>> Update(UserUpdateRequest obj)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                obj.UpdateInfo();

                QTTS01_User checkAgent = await _userRepository.GetSingle(x => x.username == obj.username && x.tenant_id == obj.tenant_id);
                if (checkAgent == null)
                {
                    return new ResponseService<UserCustomResponse>(Constants.USER_NOT_FOUND).BadRequest(MessCodes.USER_NOT_FOUND);
                }

                // Old data to create log
                //UserCustomResponse oldData = _mapper.Map<QTTS01_User, UserCustomResponse>(checkAgent);

                // User entity
                QTTS01_User userEntity = _mapper.Map<UserUpdateRequest, QTTS01_User>(obj);
                userEntity.password = checkAgent.password;
                userEntity.create_time = checkAgent.create_time;
                userEntity.create_by = checkAgent.create_by;
                userEntity.avatar = checkAgent.avatar;

                // Update entity
                await _userRepository.Update(userEntity, userEntity.username);

                // Model response
                UserCustomResponse response = _mapper.Map<QTTS01_User, UserCustomResponse>(userEntity);

                return new ResponseService<UserCustomResponse>(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<UserCustomResponse>(ex);
            }
        }
        public async Task<ResponseService<bool>> Delete(string username)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                Guid tenantId = SessionStore.Get<Guid>(Constants.KEY_SESSION_TENANT_ID);

                var checkAgent = await _userRepository.GetSingle(x => x.username == username && x.tenant_id == tenantId);
                if (checkAgent == null)
                {
                    return new ResponseService<bool>(Constants.USER_NOT_FOUND).BadRequest(MessCodes.DATA_NOT_FOUND);
                }

                await _userRepository.RemoveUserData(checkAgent);

                // Send data log
                //await _logService.CreateKafkaLog(Constants.LOG_TYPE_DELETE, Constants.USER_MANAGEMENT_SERVICE, checkAgent, null);
                return new ResponseService<bool>(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<bool>(ex);
            }
        }
        public async Task<ResponseService<UserCustomResponse>> UpdateInformation(UpdateInformation request)
        {
            try
            {
                UserCustomResponse result = null;
                Guid tenantId = SessionStore.Get<Guid>(Constants.KEY_SESSION_TENANT_ID);
                Guid asteriskId = SessionStore.Get<Guid>(Constants.KEY_SESSION_ASTERISK_ID);
                request.username = SessionStore.Get<string>(Constants.KEY_SESSION_USER_ID);

                QTTS01_User checkExistsUser = await _userRepository.GetSingle(x => x.username == request.username && x.tenant_id == tenantId);
                if (checkExistsUser == null)
                {
                    return new ResponseService<UserCustomResponse>(Constants.USER_NOT_FOUND).BadRequest(MessCodes.DATA_NOT_FOUND);
                }

                // Old data to create log
                UserCustomResponse oldData = _mapper.Map<QTTS01_User, UserCustomResponse>(checkExistsUser);

                checkExistsUser.fullname = string.IsNullOrEmpty(request.fullname) ? checkExistsUser.fullname : request.fullname;
                checkExistsUser.phone = string.IsNullOrEmpty(request.phone) ? checkExistsUser.phone : request.phone;
                checkExistsUser.avatar = string.IsNullOrEmpty(request.avatar) ? checkExistsUser.avatar : request.avatar;
                checkExistsUser.description = string.IsNullOrEmpty(request.description) ? checkExistsUser.description : request.description;

                // Update entity
                await _userRepository.Update(checkExistsUser, checkExistsUser.username);
                result = _mapper.Map<QTTS01_User, UserCustomResponse>(checkExistsUser);

                return new ResponseService<UserCustomResponse>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<UserCustomResponse>(ex);
            }
        }
        public virtual async Task<ResponseService<bool>> ChangePassword(UpdatePasswordRequest request)
        {
            try
            {
                request.UpdateInfo();
                Guid tenantId = SessionStore.Get<Guid>(Constants.KEY_SESSION_TENANT_ID);
                string username = SessionStore.Get<string>(Constants.KEY_SESSION_USER_ID);

                if (request.oldpassword == request.password)
                {
                    return new ResponseService<bool>(Constants.NEW_PASSWORD_CANNOT_SAME_OLD_PASSWORD).BadRequest(MessCodes.NEW_PASSWORD_CANNOT_SAME_OLD_PASSWORD);
                }

                QTTS01_User checkExistsUser = await _userRepository.GetSingle(x => x.username == username && x.password == request.oldpassword && x.tenant_id == tenantId);
                if (checkExistsUser == null)
                {
                    return new ResponseService<bool>(Constants.OLD_PASSWORD_IS_NOT_CORRECT).BadRequest(MessCodes.OLD_PASSWORD_IS_NOT_CORRECT);
                }

                DateTime currentDatetime = DateTime.Now;
                checkExistsUser.password = request.password;
                checkExistsUser.modify_by = username;
                checkExistsUser.modify_time = currentDatetime;

                QTTS01_ChangePasswordLog passwordLogEntity = new QTTS01_ChangePasswordLog();
                passwordLogEntity.id = Guid.NewGuid();
                passwordLogEntity.username = checkExistsUser.username;
                passwordLogEntity.old_password = request.oldpassword;
                passwordLogEntity.new_password = request.password;
                passwordLogEntity.create_time = currentDatetime;
                passwordLogEntity.modify_time = currentDatetime;
                passwordLogEntity.modify_by = username;
                passwordLogEntity.create_by = username;
                passwordLogEntity.tenant_id = checkExistsUser.tenant_id;

                await _userRepository.ChangePasswordAndCreateChangePasswordLog(checkExistsUser, passwordLogEntity);

                // Send data log
                //await _logService.CreateKafkaLog(Constants.LOG_TYPE_UPDATE, Constants.CHANGE_PASSWORD_SUCCESSFULLY, Constants.USER_MANAGEMENT_SERVICE, username, null);
                return new ResponseService<bool>(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<bool>(ex);
            }
        }

        //public async Task<ResponseService<ListResult<UserCustomResponse>>> GetListUserRole(UsernameRequest request)
        //{
        //    try
        //    {
        //        _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
        //        if (string.IsNullOrEmpty(request.username))
        //        {
        //            request.username = SessionStore.Get<string>(Constants.KEY_SESSION_USER_ID);
        //        }

        //        if (request.tenant_id == Guid.Empty)
        //        {
        //            request.tenant_id = SessionStore.Get<Guid>(Constants.KEY_SESSION_TENANT_ID);
        //        }

        //        ListResult<UserCustomResponse> result = new ListResult<UserCustomResponse>();
        //        DynamicParameters parameters = new DynamicParameters(
        //                   new
        //                   {
        //                       currentUser = request.username,
        //                       tenantId = request.tenant_id
        //                   });

        //        result.items = await Task.FromResult(_storeProcedureExecute.ExecuteReturnList<UserCustomResponse>("usp_User_Get_List_User_Role", parameters).Result.ToList());
        //        result.total = result.items.Count;

        //        return new ResponseService<ListResult<UserCustomResponse>>(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex);
        //        return new ResponseService<ListResult<UserCustomResponse>>(ex);
        //    }
        //}
        //public async Task<ResponseService<ListResult<UserRoleResponse>>> GetListUserOnLevel(UsernameRequest request)
        //{
        //    try
        //    {
        //        _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
        //        if (string.IsNullOrEmpty(request.username))
        //        {
        //            request.username = SessionStore.Get<string>(Constants.KEY_SESSION_USER_ID);

        //        }

        //        if (request.tenant_id == Guid.Empty)
        //        {
        //            request.tenant_id = SessionStore.Get<Guid>(Constants.KEY_SESSION_TENANT_ID);
        //        }

        //        ListResult<UserRoleResponse> result = new ListResult<UserRoleResponse>();
        //        DynamicParameters parameters = new DynamicParameters(
        //                   new
        //                   {
        //                       currentUser = request.username,
        //                       tenantId = request.tenant_id
        //                   });

        //        result.items = await Task.FromResult(_storeProcedureExecute.ExecuteReturnList<UserRoleResponse>("usp_User_Get_List_User_On_Level", parameters).Result.ToList());
        //        result.total = result.items.Count;

        //        return new ResponseService<ListResult<UserRoleResponse>>(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex);
        //        return new ResponseService<ListResult<UserRoleResponse>>(ex);
        //    }
        //}


        //// Api get tất cả user state của tenant và không check phân quyền
        //public async Task<ResponseService<ListResult<UserState>>> GetAllUserWithState(PagingRequest<UserGetByFullNameOrExtensionNumber> param)
        //{
        //    try
        //    {
        //        _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));

        //        ListResult<UserState> result = new ListResult<UserState>();
        //        List<UserState> userStates = new List<UserState>();

        //        if (param.tenant_id == Guid.Empty)
        //        {
        //            param.tenant_id = SessionStore.Get<Guid>(Constants.KEY_SESSION_TENANT_ID);
        //        }

        //        ListResult<BCC01_User> userList = await GetAgentWithFullNameOrExtensionNumber(param);
        //        BCC01_StateList stateOffLineByTenant = await _userRepository.GetOfflineState(param.tenant_id);
        //        foreach (var user in userList.items)
        //        {
        //            ConnectionManager userDataOnRedis = await Redis<ConnectionManager>.GetAsync(user.username, Constants.HASHTAG_CONNECTION);
        //            if (userDataOnRedis != null)
        //            {
        //                UserState userState = new UserState();
        //                userState.username = user.username;
        //                userState.extension_number = user.extension_number;
        //                userState.fullname = user.fullname;
        //                userState.avatar = user.avatar;
        //                userState.phone = user.phone;
        //                userState.email = user.email;
        //                userState.is_administrator = user.is_administrator;
        //                userState.is_rootuser = user.is_rootuser;
        //                userState.is_rona = userDataOnRedis.is_rona;
        //                userState.state_id = userDataOnRedis.state_id;
        //                userState.role_id = user.role_id.ToString();
        //                userState.report_to = user.report_to;
        //                userState.state = userDataOnRedis.state;
        //                userState.state_detail = userDataOnRedis.state_detail;
        //                userState.is_supervisor = user.is_supervisor;
        //                userState.is_agent = user.is_agent;
        //                userState.last_change_state_time = userDataOnRedis.last_change_state_time;

        //                userStates.Add(userState);
        //            }
        //            else
        //            {
        //                UserState userState = new UserState();
        //                userState.username = user.username;
        //                userState.extension_number = user.extension_number;
        //                userState.fullname = user.fullname;
        //                userState.avatar = user.avatar;
        //                userState.phone = user.phone;
        //                userState.email = user.email;
        //                userState.is_administrator = user.is_administrator;
        //                userState.is_rootuser = user.is_rootuser;
        //                userState.is_rona = false;
        //                userState.state_id = stateOffLineByTenant != null ? stateOffLineByTenant.id : Guid.Empty;
        //                userState.role_id = user.role_id.ToString();
        //                userState.report_to = user.report_to;
        //                userState.state = stateOffLineByTenant != null ? stateOffLineByTenant.state_name : string.Empty;
        //                userState.state_detail = stateOffLineByTenant != null ? stateOffLineByTenant.state_key : string.Empty;
        //                userState.is_supervisor = user.is_supervisor;
        //                userState.is_agent = user.is_agent;
        //                userState.last_change_state_time = DateTime.Now.AddDays(2);

        //                userStates.Add(userState);
        //            }
        //        }

        //        result.items = userStates;
        //        result.total = userList.total;

        //        return new ResponseService<ListResult<UserState>>(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex);
        //        return new ResponseService<ListResult<UserState>>(ex);
        //    }
        //}

        //// Api get tất cả user state của tenant và có check phân quyền
        //public async Task<ResponseService<ListResultMonitoring<UserState>>> GetAllUserWithStateClient(PagingRequest<UserGetByFullNameOrExtensionNumberClient> param)
        //{
        //    ListResultMonitoring<UserState> result = new ListResultMonitoring<UserState>();
        //    List<UserState> userStates = new List<UserState>();
        //    try
        //    {
        //        _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
        //        param.tenant_id = SessionStore.Get<Guid>(Constants.KEY_SESSION_TENANT_ID);
        //        param.request.current_user = SessionStore.Get<string>(Constants.KEY_SESSION_USER_ID);

        //        BCC01_StateList stateOffLineByTenant = await _userRepository.GetOfflineState(param.tenant_id);
        //        ListResult<BCC01_User> userList = await GetAgentWithFullNameOrExtensionNumberClient(param);

        //        foreach (var user in userList.items)
        //        {
        //            ConnectionManager userDataOnRedis = await Redis<ConnectionManager>.GetAsync(user.username, Constants.HASHTAG_CONNECTION);
        //            if (userDataOnRedis != null)
        //            {
        //                UserState userState = new UserState();
        //                userState.username = user.username;
        //                userState.extension_number = user.extension_number;
        //                userState.fullname = user.fullname;
        //                userState.avatar = user.avatar;
        //                userState.phone = user.phone;
        //                userState.email = user.email;
        //                userState.is_administrator = user.is_administrator;
        //                userState.is_rootuser = user.is_rootuser;
        //                userState.is_rona = userDataOnRedis.is_rona;
        //                userState.state_id = userDataOnRedis.state_id;
        //                userState.role_id = user.role_id.ToString();
        //                userState.report_to = user.report_to;
        //                userState.state = userDataOnRedis.state;
        //                userState.state_detail = userDataOnRedis.state_detail;
        //                userState.is_supervisor = user.is_supervisor;
        //                userState.is_agent = user.is_agent;
        //                userState.last_change_state_time = userDataOnRedis.last_change_state_time;

        //                userStates.Add(userState);
        //            }
        //            else
        //            {
        //                UserState userState = new UserState();
        //                userState.username = user.username;
        //                userState.extension_number = user.extension_number;
        //                userState.fullname = user.fullname;
        //                userState.avatar = user.avatar;
        //                userState.phone = user.phone;
        //                userState.email = user.email;
        //                userState.is_administrator = user.is_administrator;
        //                userState.is_rootuser = user.is_rootuser;
        //                userState.is_rona = false;
        //                userState.state_id = stateOffLineByTenant != null ? stateOffLineByTenant.id : Guid.Empty;
        //                userState.role_id = user.role_id.ToString();
        //                userState.report_to = user.report_to;
        //                userState.state = stateOffLineByTenant != null ? stateOffLineByTenant.state_name : string.Empty;
        //                userState.state_detail = stateOffLineByTenant != null ? stateOffLineByTenant.state_key : string.Empty;
        //                userState.is_supervisor = user.is_supervisor;
        //                userState.is_agent = user.is_agent;
        //                userState.last_change_state_time = DateTime.Now.AddDays(2);

        //                userStates.Add(userState);
        //            }
        //        }

        //        result.items = userStates;
        //        result.total = userList.total;
        //        result.totalReady = userStates.Count(x => x.state_detail == Constants.AGENT_STATE_READY);

        //        return new ResponseService<ListResultMonitoring<UserState>>(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex);
        //        return new ResponseService<ListResultMonitoring<UserState>>(ex);
        //    }
        //}
        //public async Task<ResponseService<bool>> CheckInformation(CheckInformationRequest request)
        //{
        //    try
        //    {
        //        request.password = HashString.StringToHash(request.password, Constants.HASH_SHA512);
        //        bool data = await _userRepository.CheckInformation(request.username, request.password, request.tenant_id);
        //        return new ResponseService<bool>(data);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex);
        //        return new ResponseService<bool>(ex);
        //    }
        //}

        //#region CRUD Root User
        //public async Task<ResponseService<ListResult<UserCustomResponse>>> GetAllRootUser(PagingRequest<RootUserSearchRequest> param)
        //{
        //    try
        //    {
        //        _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));

        //        bool isRoot = !string.IsNullOrEmpty(SessionStore.Get<string>(Constants.KEY_SESSION_IS_ROOT)) ? SessionStore.Get<bool>(Constants.KEY_SESSION_IS_ROOT) : false;
        //        if (!isRoot)
        //        {
        //            return new ResponseService<ListResult<UserCustomResponse>>(Constants.ONLY_ROOT_USER_CAN_DO_THIS).BadRequest(MessCodes.ONLY_ROOT_USER_CAN_DO_THIS);
        //        }

        //        param.tenant_id = SessionStore.Get<Guid>(Constants.KEY_SESSION_TENANT_ID);

        //        ListResult<UserCustomResponse> response = new ListResult<UserCustomResponse>();
        //        RootUserSearchRequest request = param.request != null ? param.request : new RootUserSearchRequest();
        //        DynamicParameters parameters = new DynamicParameters(
        //            new
        //            {
        //                fullname = request.fullname,
        //                email = request.email,
        //                phone = request.phone,
        //                createBy = request.create_by,
        //                createTime = request.create_time,
        //                roleName = request.role_name,
        //                page = param.page,
        //                limit = param.limit,
        //                tenantId = param.tenant_id
        //            });

        //        parameters.Add("totalRow", dbType: DbType.Int32, direction: ParameterDirection.Output);

        //        response.items = await Task.FromResult(_storeProcedureExecute.ExecuteReturnList<UserCustomResponse>("usp_User_Get_All_Root", parameters).Result.ToList());
        //        response.total = parameters.Get<int>("totalRow");

        //        return new ResponseService<ListResult<UserCustomResponse>>(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex);
        //        return new ResponseService<ListResult<UserCustomResponse>>(ex);
        //    }
        //}

        //public async Task<ResponseService<bool>> DeleteRootUser(string username)
        //{
        //    try
        //    {
        //        _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
        //        string currentUser = SessionStore.Get<string>(Constants.KEY_SESSION_USER_ID);
        //        bool isRoot = !string.IsNullOrEmpty(SessionStore.Get<string>(Constants.KEY_SESSION_IS_ROOT)) ? SessionStore.Get<bool>(Constants.KEY_SESSION_IS_ROOT) : false;
        //        if (!isRoot)
        //        {
        //            return new ResponseService<bool>(Constants.ONLY_ROOT_USER_CAN_DO_THIS).BadRequest(MessCodes.ONLY_ROOT_USER_CAN_DO_THIS);
        //        }

        //        Guid tenantId = SessionStore.Get<Guid>(Constants.KEY_SESSION_TENANT_ID);
        //        BCC01_User checkExists = await _userRepository.GetSingle(x => x.username == username && x.tenant_id == tenantId);
        //        if (checkExists == null)
        //        {
        //            return new ResponseService<bool>(Constants.USER_NOT_FOUND).BadRequest(MessCodes.DATA_NOT_FOUND);
        //        }

        //        if (checkExists.username == currentUser)
        //        {
        //            return new ResponseService<bool>(Constants.THIS_USER_CANNOT_BE_DELETED).BadRequest(MessCodes.THIS_USER_CANNOT_BE_DELETED);
        //        }

        //        // Old data to create log
        //        UserCustomResponse oldData = _mapper.Map<BCC01_User, UserCustomResponse>(checkExists);

        //        await _userRepository.DeleteRootUser(checkExists);

        //        // Send data log
        //        await _logService.CreateKafkaLog(Constants.LOG_TYPE_DELETE, Constants.USER_MANAGEMENT_SERVICE, oldData, null);
        //        return new ResponseService<bool>(true);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex);
        //        return new ResponseService<bool>(ex);
        //    }
        //}

        //public async Task<ResponseService<UserCustomResponse>> CreateRootUser(RootUserAddRequest obj)
        //{
        //    try
        //    {
        //        _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
        //        obj.AddInfo();

        //        bool isRoot = !string.IsNullOrEmpty(SessionStore.Get<string>(Constants.KEY_SESSION_IS_ROOT)) ? SessionStore.Get<bool>(Constants.KEY_SESSION_IS_ROOT) : false;
        //        if (!isRoot)
        //        {
        //            return new ResponseService<UserCustomResponse>(Constants.ONLY_ROOT_USER_CAN_DO_THIS).BadRequest(MessCodes.ONLY_ROOT_USER_CAN_DO_THIS);
        //        }

        //        UploadFileReponse generateDefaultAvatar = GenerateAvatar(obj.username, obj.tenant_id);
        //        obj.avatar = generateDefaultAvatar.file;
        //        obj.password = HashString.StringToHash(obj.password, Constants.HASH_SHA512);

        //        bool checkExistsUsername = await _userRepository.CheckExistsAnyAsync(x => x.username == obj.username);
        //        if (checkExistsUsername)
        //        {
        //            return new ResponseService<UserCustomResponse>(Constants.USERNAME_OR_EXTENSION_NUMBER_IS_EXISTS).BadRequest(MessCodes.USERNAME_OR_EXTENSION_NUMBER_IS_EXISTS);
        //        }

        //        bool checkExistsProfile = await _userRepository.CheckExistsProfile(obj.profile_id, obj.tenant_id);
        //        if (!checkExistsProfile)
        //        {
        //            return new ResponseService<UserCustomResponse>(Constants.PROFILE_NOT_FOUND).BadRequest(MessCodes.PROFILE_NOT_FOUND);
        //        }

        //        // User entity
        //        BCC01_User userEntity = _mapper.Map<RootUserAddRequest, BCC01_User>(obj);
        //        userEntity.access_key = Guid.NewGuid().ToString();
        //        userEntity.extension_number = string.Empty;
        //        userEntity.extension_password = Guid.NewGuid().ToString();
        //        userEntity.role_id = Constants.ROOT_ROLE;
        //        userEntity.is_rootuser = true;
        //        userEntity.is_active = true;
        //        userEntity.report_to = string.Empty;

        //        // Map user to profile entity
        //        BCC01_MapProfileUser mapRootUserProfileEntity = new BCC01_MapProfileUser();
        //        mapRootUserProfileEntity.id = Guid.NewGuid();
        //        mapRootUserProfileEntity.username = userEntity.username;
        //        mapRootUserProfileEntity.profile_id = obj.profile_id;
        //        mapRootUserProfileEntity.description = "System";
        //        mapRootUserProfileEntity.is_active = true;
        //        mapRootUserProfileEntity.create_time = userEntity.create_time;
        //        mapRootUserProfileEntity.create_by = userEntity.create_by;
        //        mapRootUserProfileEntity.modify_time = userEntity.modify_time;
        //        mapRootUserProfileEntity.modify_by = userEntity.modify_by;
        //        mapRootUserProfileEntity.tenant_id = userEntity.tenant_id;

        //        await _userRepository.CreateRootUser(userEntity, mapRootUserProfileEntity);

        //        // Model response
        //        UserCustomResponse result = _mapper.Map<BCC01_User, UserCustomResponse>(userEntity);
        //        result.role_name = "Root";
        //        result.role_parent_id = Guid.Empty;

        //        // Send data log
        //        await _logService.CreateKafkaLog(Constants.LOG_TYPE_CREATE, Constants.USER_MANAGEMENT_SERVICE, result, null);
        //        return new ResponseService<UserCustomResponse>(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex);
        //        return new ResponseService<UserCustomResponse>(ex);
        //    }
        //}

        //public async Task<ResponseService<UserCustomResponse>> UpdateRootUser(RootUserUpdateRequest request)
        //{
        //    try
        //    {
        //        _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
        //        request.UpdateInfo();
        //        string currentUser = SessionStore.Get<string>(Constants.KEY_SESSION_USER_ID);

        //        bool isRoot = !string.IsNullOrEmpty(SessionStore.Get<string>(Constants.KEY_SESSION_IS_ROOT)) ? SessionStore.Get<bool>(Constants.KEY_SESSION_IS_ROOT) : false;
        //        if (!isRoot)
        //        {
        //            return new ResponseService<UserCustomResponse>(Constants.ONLY_ROOT_USER_CAN_DO_THIS).BadRequest(MessCodes.ONLY_ROOT_USER_CAN_DO_THIS);
        //        }

        //        BCC01_User checkAgentExec = await _userRepository.GetSingle(x => x.username == currentUser && x.tenant_id == request.tenant_id);
        //        if (checkAgentExec == null)
        //        {
        //            return new ResponseService<UserCustomResponse>(Constants.USER_NOT_FOUND).BadRequest(MessCodes.USER_NOT_FOUND);
        //        }

        //        BCC01_User checkAgentUpdate = await _userRepository.GetSingle(x => x.username == request.username && x.tenant_id == request.tenant_id);
        //        if (checkAgentUpdate == null)
        //        {
        //            return new ResponseService<UserCustomResponse>(Constants.USER_NOT_FOUND).BadRequest(MessCodes.USER_NOT_FOUND);
        //        }

        //        // Không thể tự update thành admin nếu không phải admin
        //        if (!checkAgentExec.is_administrator && checkAgentUpdate.is_administrator != request.is_administrator)
        //        {
        //            return new ResponseService<UserCustomResponse>(Constants.YOU_DONT_HAVE_PERMISSION_TO_DO_THIS).BadRequest(MessCodes.YOU_DONT_HAVE_PERMISSION_TO_DO_THIS);
        //        }

        //        // Old data to create log
        //        UserCustomResponse oldData = _mapper.Map<BCC01_User, UserCustomResponse>(checkAgentUpdate);
        //        oldData.role_name = "Root";

        //        // User entity
        //        BCC01_User userEntity = _mapper.Map<RootUserUpdateRequest, BCC01_User>(request);
        //        userEntity.password = checkAgentUpdate.password;
        //        userEntity.extension_number = checkAgentUpdate.extension_number;
        //        userEntity.extension_password = checkAgentUpdate.extension_password;
        //        userEntity.create_time = checkAgentUpdate.create_time;
        //        userEntity.create_by = checkAgentUpdate.create_by;
        //        userEntity.avatar = checkAgentUpdate.avatar;
        //        userEntity.access_key = checkAgentUpdate.access_key;
        //        userEntity.role_id = checkAgentUpdate.role_id;
        //        userEntity.is_rootuser = checkAgentUpdate.is_rootuser;

        //        // Update entity
        //        await _userRepository.Update(userEntity, userEntity.username);

        //        // Model response
        //        UserCustomResponse response = _mapper.Map<BCC01_User, UserCustomResponse>(userEntity);
        //        response.role_name = "Root";
        //        response.role_parent_id = Guid.Empty;

        //        // Send data log
        //        await _logService.CreateKafkaLog(Constants.LOG_TYPE_UPDATE, Constants.USER_MANAGEMENT_SERVICE, oldData, response);
        //        return new ResponseService<UserCustomResponse>(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex);
        //        return new ResponseService<UserCustomResponse>(ex);
        //    }
        //}

        //public async Task<ResponseService<UserCustomResponse>> RootUserUpdateInformation(UpdateInformation request)
        //{
        //    try
        //    {
        //        UserCustomResponse result = null;
        //        Guid tenantId = SessionStore.Get<Guid>(Constants.KEY_SESSION_TENANT_ID);
        //        request.username = SessionStore.Get<string>(Constants.KEY_SESSION_USER_ID);

        //        bool isRoot = !string.IsNullOrEmpty(SessionStore.Get<string>(Constants.KEY_SESSION_IS_ROOT)) ? SessionStore.Get<bool>(Constants.KEY_SESSION_IS_ROOT) : false;
        //        if (!isRoot)
        //        {
        //            return new ResponseService<UserCustomResponse>(Constants.ONLY_ROOT_USER_CAN_DO_THIS).BadRequest(MessCodes.ONLY_ROOT_USER_CAN_DO_THIS);
        //        }

        //        BCC01_User checkExistsUser = await _userRepository.GetSingle(x => x.username == request.username && x.tenant_id == tenantId);
        //        if (checkExistsUser == null)
        //        {
        //            return new ResponseService<UserCustomResponse>(Constants.USER_NOT_FOUND).BadRequest(MessCodes.DATA_NOT_FOUND);
        //        }

        //        // Old data to create log
        //        UserCustomResponse oldData = _mapper.Map<BCC01_User, UserCustomResponse>(checkExistsUser);
        //        oldData.role_name = "Root";

        //        checkExistsUser.fullname = string.IsNullOrEmpty(request.fullname) ? checkExistsUser.fullname : request.fullname;
        //        checkExistsUser.phone = string.IsNullOrEmpty(request.phone) ? checkExistsUser.phone : request.phone;
        //        checkExistsUser.avatar = string.IsNullOrEmpty(request.avatar) ? checkExistsUser.avatar : request.avatar;
        //        checkExistsUser.language = string.IsNullOrEmpty(request.language) ? checkExistsUser.language : request.language;
        //        checkExistsUser.description = string.IsNullOrEmpty(request.description) ? checkExistsUser.description : request.description;

        //        // Update entity
        //        await _userRepository.Update(checkExistsUser, checkExistsUser.username);

        //        result = _mapper.Map<BCC01_User, UserCustomResponse>(checkExistsUser);
        //        result.role_name = "Root";
        //        result.role_parent_id = Guid.Empty;

        //        // Send data log
        //        await _logService.CreateKafkaLog(Constants.LOG_TYPE_UPDATE, Constants.USER_MANAGEMENT_SERVICE, oldData, result);
        //        return new ResponseService<UserCustomResponse>(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex);
        //        return new ResponseService<UserCustomResponse>(ex);
        //    }
        //}

        //#endregion

        //#region private func
        private UploadFileReponse GenerateAvatar(string username, Guid tenantId)
        {
            var fileName = $"{Guid.NewGuid()}{Constants.EXTENSION_FILE_AVATAR}";
            string pathResourceByTenant = $"{Constants.PATH_RESOURCE_BY_TENANT}{tenantId}";
            if (!Directory.Exists($"{_webHostEnvironment.WebRootPath}{pathResourceByTenant}"))
            {
                Directory.CreateDirectory($"{_webHostEnvironment.WebRootPath}{pathResourceByTenant}");
            }

            string pathResourceAvatarByTenant = $"{pathResourceByTenant}{Constants.PATH_FOLDER_AVATAR}";
            if (!Directory.Exists($"{_webHostEnvironment.WebRootPath}{pathResourceAvatarByTenant}"))
            {
                Directory.CreateDirectory($"{_webHostEnvironment.WebRootPath}{pathResourceAvatarByTenant}");
            }

            string pathSaveFile = $"{_webHostEnvironment.WebRootPath}{pathResourceAvatarByTenant}/{fileName}";
            string pathResponse = $"{urlKongBCC}{pathResourceAvatarByTenant}/{fileName}";

            CommonFunc.GenerateAvatar(username, pathSaveFile);

            UploadFileReponse response = new UploadFileReponse();
            response.file = pathResponse;
            response.file_url = pathResponse;
            return response;
        }

    }
}
