using AutoMapper;
using Common;
using Common.Commons;
using Common.Params.Base;
using Repository.CustomModel;
using Repository.Model;
using Repository.Repositories;
using System;
using System.Threading.Tasks;
using UserManagement.Common;
using UserManagement.Config;
using UserManagement.Models.Main;

namespace UserManagement.Services.Implement.GeneralSetting
{
    public class GroupService : BaseService, IGroupService
    {
        private readonly IGroupRepository _groupRepository;
        public GroupService(
            IGroupRepository groupRepository,
            ILogger logger,
            IConfigManager config,
            IMapper mapper) : base(config, logger, mapper)
        {
            _groupRepository = groupRepository;
        }

        public async Task<ResponseService<ListResult<GroupResponse>>> GetAll(PagingParamCustom param)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));

                string currentUser = SessionStore.Get<string>(Constants.KEY_SESSION_USER_ID);

                ListResult<QTTS01_Group> groupModel = await _groupRepository.GetAllCustom(param);
                ListResult<GroupResponse> result = _mapper.Map<ListResult<QTTS01_Group>, ListResult<GroupResponse>>(groupModel);

                return new ResponseService<ListResult<GroupResponse>>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<ListResult<GroupResponse>>(ex);
            }
        }

        public async Task<ResponseService<GroupResponse>> GetById(Guid id)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));

                QTTS01_Group data = await _groupRepository.GetSingle(x => x.id == id);
                GroupResponse result = _mapper.Map<QTTS01_Group, GroupResponse>(data);

                return new ResponseService<GroupResponse>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<GroupResponse>(ex);
            }
        }

        public async Task<ResponseService<GroupResponse>> Create(GroupRequest obj)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                obj.AddInfo();

                bool checkExistsBankName = await _groupRepository.CheckExistsAnyAsync(x => x.group_name.ToLower() == obj.group_name.Trim().ToLower());
                if (checkExistsBankName)
                {
                    return new ResponseService<GroupResponse>(Constants.GROUP_NAME_IS_ALREADY_EXISTS).BadRequest(MessCodes.GROUP_NAME_IS_ALREADY_EXISTS);
                }

                QTTS01_Group entity = _mapper.Map<GroupRequest, QTTS01_Group>(obj);
                await _groupRepository.Create(entity);

                GroupResponse result = _mapper.Map<QTTS01_Group, GroupResponse>(entity);
                return new ResponseService<GroupResponse>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<GroupResponse>(ex);
            }
        }

        public async Task<ResponseService<GroupResponse>> Update(GroupRequest obj)
        {
            try
            {
                obj.UpdateInfo();
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));

                QTTS01_Group checkExists = await _groupRepository.GetSingle(x => x.id == obj.id);
                if (checkExists == null)
                {
                    return new ResponseService<GroupResponse>(Constants.GROUP_NOT_FOUND).BadRequest(MessCodes.GROUP_NOT_FOUND);
                }

                bool checkExistsName = await _groupRepository.CheckExistsAnyAsync(x => x.id != obj.id);
                if (checkExists == null)
                {
                    return new ResponseService<GroupResponse>(Constants.GROUP_NOT_FOUND).BadRequest(MessCodes.GROUP_NOT_FOUND);
                }

                QTTS01_Group entity = _mapper.Map<GroupRequest, QTTS01_Group>(obj);
                entity.create_by = checkExists.create_by;
                entity.create_time = checkExists.create_time;
                entity.is_active = checkExists.is_active;

                await _groupRepository.Update(entity, obj.id);
                GroupResponse result = _mapper.Map<QTTS01_Group, GroupResponse>(entity);

                return new ResponseService<GroupResponse>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<GroupResponse>(ex);
            }
        }

        public async Task<ResponseService<bool>> Delete(Guid id)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));

                QTTS01_Group checkExists = await _groupRepository.GetSingle(x => x.id == id);
                if (checkExists == null)
                {
                    return new ResponseService<bool>(Constants.GROUP_NOT_FOUND).BadRequest(MessCodes.GROUP_NOT_FOUND);
                }

                await _groupRepository.DeleteCustom(checkExists);

                return new ResponseService<bool>(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<bool>(ex);
            }
        }
    }
}
