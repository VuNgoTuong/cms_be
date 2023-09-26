using AutoMapper;
using Common;
using Common.Commons;
using Repository.Model;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Common;
using UserManagement.Config;
using UserManagement.Models.Main;
using UserManagement.Services.Implement.GeneralSetting;

namespace UserManagement.Services.Implement.UserSetting
{
    public class MapProfileUserService : BaseService, IMapProfileUserService
    {
        //private readonly ILogService _logService;
        private readonly IMapProfileUserRepository _mapProfileUserRepository;
        public MapProfileUserService(
            //ILogService logService,
            IMapProfileUserRepository mapProfileUserRepository,
            ILogger logger,
            IConfigManager config,
            IMapper mapper) : base(config, logger, mapper)
        {
            //_logService = logService;
            _mapProfileUserRepository = mapProfileUserRepository;
        }

        #region implement
        public async Task<ResponseService<bool>> Create(AddUserToProfile request)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));

                DateTime currentDatetime = DateTime.Now;
                Guid tenantId = SessionStore.Get<Guid>(Constants.KEY_SESSION_TENANT_ID);
                string currentUser = SessionStore.Get<string>(Constants.KEY_SESSION_USER_ID);

                bool checkExistsProfile = await _mapProfileUserRepository.CheckExistsProfile(tenantId, request.profile_id);
                if (!checkExistsProfile)
                {
                    return new ResponseService<bool>(Constants.PROFILE_NOT_FOUND).BadRequest(MessCodes.PROFILE_NOT_FOUND);
                }

                bool checkUser = await _mapProfileUserRepository.CheckExistsUserInTenant(tenantId, request.usernames);
                if (!checkUser)
                {
                    return new ResponseService<bool>(Constants.USER_IS_INVALID).BadRequest(MessCodes.DATA_INVALID);
                }

                List<QTTS01_MapProfileUser> entities = new List<QTTS01_MapProfileUser>();

                foreach (var item in request.usernames)
                {
                    QTTS01_MapProfileUser entity = new QTTS01_MapProfileUser();
                    entity.id = Guid.NewGuid();
                    entity.username = item;
                    entity.profile_id = request.profile_id;
                    entity.description = "";
                    entity.is_active = true;
                    entity.create_time = currentDatetime;
                    entity.create_by = currentUser;
                    entity.modify_time = currentDatetime;
                    entity.modify_by = "";
                    entity.tenant_id = tenantId;

                    entities.Add(entity);
                }

                await _mapProfileUserRepository.CreateList(entities);

                // Send data log
                //await _logService.CreateKafkaLog(Constants.LOG_TYPE_UPDATE,Constants.ADD_USER_TO_PROFILE_SUCCESSFULLY, Constants.PROFILE_SERVICE, request, null);
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
