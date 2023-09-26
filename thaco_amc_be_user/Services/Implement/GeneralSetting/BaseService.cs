using AutoMapper;
using Common.Commons;
using System.Diagnostics;
using UserManagement.Common;
using UserManagement.Config;

namespace UserManagement.Services.Implement.GeneralSetting
{
    public class BaseService
    {
        protected readonly IConfigManager _config;
        protected readonly ILogger _logger;
        protected readonly IMapper _mapper;

        protected BaseService(IConfigManager config, ILogger logger, IMapper mapper)
        {
            _config = config;
            _logger = logger;
            _mapper = mapper;
        }

        public static string GetMethodName(StackTrace stackTrace)
        {
            return CommonFunc.GetMethodName(stackTrace);
        }
    }
}
