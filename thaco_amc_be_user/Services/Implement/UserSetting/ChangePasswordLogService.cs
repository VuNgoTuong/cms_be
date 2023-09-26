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
using UserManagement.Services.Implement.GeneralSetting;

namespace UserManagement.Services.Implement.UserSetting
{
    public class ChangePasswordLogService : BaseService, IChangePasswordLogService
    {
        private readonly IChangePasswordLogRepository _changePasswordLogRepository;
        public ChangePasswordLogService(IChangePasswordLogRepository changePasswordLogRepository, ILogger logger, IConfigManager config,
                                 IMapper mapper) : base(config, logger, mapper)
        {
            _changePasswordLogRepository = changePasswordLogRepository;
        }
        #region implement

        public async Task<ResponseService<ListResult<QTTS01_ChangePasswordLog>>> GetAll(PagingParam param)
        {
            try
            {
                if (param.tenant_id.Equals(Guid.Empty))
                {
                    param.tenant_id = Guid.Parse(SessionStore.Get<string>(Constants.KEY_SESSION_TENANT_ID));
                }
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                var result = await _changePasswordLogRepository.GetAll(param);
                return new ResponseService<ListResult<QTTS01_ChangePasswordLog>>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<ListResult<QTTS01_ChangePasswordLog>>(ex);
            }
        }
        #endregion
    }
}
