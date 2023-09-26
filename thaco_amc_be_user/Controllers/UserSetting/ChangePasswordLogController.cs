using Common.Commons;
using Common.Params.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.CustomModel;
using Repository.Model;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Threading.Tasks;
using UserManagement.CustomAttributes;
using UserManagement.Services.Implement.UserSetting;

namespace UserManagement.Controllers.UserSetting
{
    [Route("api/change-password-log")]
    public class ChangePasswordLogController : ControllerBase
    {
        private readonly IChangePasswordLogService _changePasswordLogService;
        public ChangePasswordLogController(IChangePasswordLogService changePasswordLogService)
        {
            _changePasswordLogService = changePasswordLogService;
        }
        [HttpPost]
        [Authorized]
        [Route("get-all")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResponseService<ListResult<QTTS01_ChangePasswordLog>>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ResponseService<ListResult<QTTS01_ChangePasswordLog>>))]
        public async Task<IActionResult> GetAll([FromBody] PagingParam param)
        {
            ResponseService<ListResult<QTTS01_ChangePasswordLog>> response = await _changePasswordLogService.GetAll(param);
            if (response.status)
            {
                return Ok(response);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response.exception);
            }
        }

    }
}
