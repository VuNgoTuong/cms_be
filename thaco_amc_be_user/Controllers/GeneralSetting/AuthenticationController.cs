using Common.Commons;
using Microsoft.AspNetCore.Mvc;
using Repository.CustomModel;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Threading.Tasks;
using UserManagement.Common;
using UserManagement.CustomAttributes;
using UserManagement.Models.Main;
using UserManagement.Services.Implement.GeneralSetting;

namespace UserManagement.Controllers.GeneralSetting
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        [Route("login")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResponseService<LoginResponse>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ResponseService<LoginResponse>))]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            ResponseService<LoginResponse> response = await _authenticationService.Login(request);
            if (response.status)
            {
                return Ok(response);
            }
            else
            {
                return new ResponseFail<LoginResponse>().Error(response);
            }
        }

        [HttpPost]
        [Authorized]
        [Route("logout")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResponseService<string>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ResponseService<string>))]
        public async Task<IActionResult> Logout()
        {
            var response = await _authenticationService.Logout();
            if (response.status)
            {
                return Ok(response);
            }
            else
            {
                return new ResponseFail<bool>().Error(response);
            }
        }

        [HttpPost]
        [Route("check-authentication")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResponseService<TokenResponse>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ResponseService<TokenResponse>))]
        public async Task<IActionResult> CheckAuthentication()
        {
            var response = await Task.Run(async () =>
            {
                return await _authenticationService.CheckAuthentication();
            });

            if (response.status)
            {
                return Ok(response);
            }
            else
            {
                return Unauthorized(response);
            }
        }
    }
}
