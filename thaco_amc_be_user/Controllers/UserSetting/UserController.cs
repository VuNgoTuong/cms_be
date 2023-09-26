using Common;
using Common.Commons;
using Common.Params.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.CustomModel;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Threading.Tasks;
using UserManagement.Common;
using UserManagement.CustomAttributes;
using UserManagement.Models.Common;
using UserManagement.Models.Main;
using UserManagement.Services.Implement.UserSetting;

namespace UserManagement.Controllers.UserSetting
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //private readonly ILogService _logService;
        private readonly IUserService _userService;
        public UserController(IUserService usertService
            //ILogService logService
            )
        {
            //_logService = logService;
            _userService = usertService; /// ghhyhhhh
        }

        [HttpGet]
        [Authorized]
        [Route("get-all")]
        [PermissionAttributeFilter("User Management", "access")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResponseService<ListResult<UserCustomResponse>>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ResponseService<ListResult<UserCustomResponse>>))]
        public async Task<IActionResult> GetAll([FromBody] PagingParam param)
        {
            ResponseService<ListResult<UserCustomResponse>> response = await _userService.GetAll(param);
            if (response.status)
            {
                return Ok(response);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response.exception);
            }
        }

        [HttpGet]
        [Authorized]
        [Route("get-by-id")]
        [PermissionAttributeFilter("User Management", "access")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResponseService<UserCustomResponse>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ResponseService<UserCustomResponse>))]
        public async Task<IActionResult> GetById([FromBody] ItemModel<string> obj)
        {
            ResponseService<UserCustomResponse> response = await _userService.GetById(obj.item);
            if (response.status)
            {
                return Ok(response);
            }
            else
            {
                return new ResponseFail<UserCustomResponse>().Error(response);
            }
        }

        [HttpPost]
        [Authorized]
        [Route("create")]
        [PermissionAttributeFilter("User Management", "create")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResponseService<UserCustomResponse>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ResponseService<UserCustomResponse>))]

        public async Task<IActionResult> Create([FromBody] UserRequest request)
        {
            ResponseService<UserCustomResponse> response = await _userService.Create(request);
            if (response.status)
            {
                return Ok(response);
            }
            else
            {
                //await _logService.CreateKafkaErrorLog(Constants.LOG_TYPE_CREATE, Constants.USER_MANAGEMENT_SERVICE, response.message, request, null);
                return new ResponseFail<UserCustomResponse>().Error(response);
            }
        }

        [HttpPost]
        [Authorized]
        [Route("update")]
        [PermissionAttributeFilter("User Management", "edit")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResponseService<UserCustomResponse>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ResponseService<UserCustomResponse>))]
        public async Task<IActionResult> Update([FromBody] UserUpdateRequest request)
        {

            ResponseService<UserCustomResponse> response = await _userService.Update(request);
            if (response.status)
            {
                return Ok(response);
            }
            else
            {
                //await _logService.CreateKafkaErrorLog(Constants.LOG_TYPE_UPDATE, Constants.USER_MANAGEMENT_SERVICE, response.message, null, request);
                return new ResponseFail<UserCustomResponse>().Error(response);
            }
        }

        [HttpPost]
        [Authorized]
        [Route("delete")]
        [PermissionAttributeFilter("User Management", "delete")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResponseService<bool>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ResponseService<bool>))]
        public async Task<IActionResult> Delete([FromBody] ItemModel<string> request)
        {
            ResponseService<bool> response = await _userService.Delete(request.item);
            if (response.status)
            {
                return Ok(response);
            }
            else
            {
                //await _logService.CreateKafkaErrorLog(Constants.LOG_TYPE_DELETE, Constants.USER_MANAGEMENT_SERVICE, response.message, request, null);
                return new ResponseFail<bool>().Error(response);
            }
        }

        [HttpPost]
        [Authorized]
        [Route("update-information")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResponseService<UserCustomResponse>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ResponseService<UserCustomResponse>))]
        public async Task<IActionResult> UpdateInformation([FromBody] UpdateInformation request)
        {
            ResponseService<UserCustomResponse> response = await _userService.UpdateInformation(request);
            if (response.status)
            {
                return Ok(response);
            }
            else
            {
                //await _logService.CreateKafkaErrorLog(Constants.LOG_TYPE_UPDATE, Constants.USER_MANAGEMENT_SERVICE, response.message, request, null);
                return new ResponseFail<UserCustomResponse>().Error(response);
            }
        }

        [HttpPost]
        [Authorized]
        [Route("change-password")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResponseService<bool>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ResponseService<bool>))]
        public async Task<IActionResult> ChangePassword([FromBody] UpdatePasswordRequest request)
        {
            ResponseService<bool> response = await _userService.ChangePassword(request);
            if (response.status)
            {
                return Ok(response);
            }
            else
            {
                //await _logService.CreateKafkaErrorLog(Constants.LOG_TYPE_UPDATE, Constants.CHANGE_PASSWORD_FAILED, Constants.USER_MANAGEMENT_SERVICE, response.message, request, null);
                return new ResponseFail<bool>().Error(response);
            }
        }

        //[HttpPost]
        //[Authorized]
        //[Route("get-list-user-role")]
        //[SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResponseService<ListResult<UserCustomResponse>>))]
        //[SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ResponseService<ListResult<UserCustomResponse>>))]
        //public async Task<IActionResult> GetListUserRole([FromBody] UsernameRequest request)
        //{
        //    ResponseService<ListResult<UserCustomResponse>> response = await _userService.GetListUserRole(request);
        //    if (response.status)
        //    {
        //        return Ok(response);
        //    }
        //    else
        //    {
        //        return new ResponseFail<ListResult<UserCustomResponse>>().Error(response);
        //    }
        //}

  
        //[HttpPost]
        //[Authorized]
        //[Route("create-root")]
        //[PermissionAttributeFilter("User Management", "create")]
        //public async Task<IActionResult> CreateRootUser(RootUserAddRequest request)
        //{
        //    ResponseService<UserCustomResponse> response = await _userService.CreateRootUser(request);
        //    if (response.status)
        //    {
        //        return Ok(response);
        //    }
        //    else
        //    {
        //        await _logService.CreateKafkaErrorLog(Constants.LOG_TYPE_CREATE, Constants.USER_MANAGEMENT_SERVICE, response.message, request, null);
        //        return new ResponseFail<UserCustomResponse>().Error(response);
        //    }
        //}

        //[HttpPost]
        //[Authorized]
        //[Route("update-root")]
        //[PermissionAttributeFilter("User Management", "edit")]
        //public async Task<IActionResult> UpdateRootUser(RootUserUpdateRequest request)
        //{
        //    ResponseService<UserCustomResponse> response = await _userService.UpdateRootUser(request);
        //    if (response.status)
        //    {
        //        return Ok(response);
        //    }
        //    else
        //    {
        //        await _logService.CreateKafkaErrorLog(Constants.LOG_TYPE_UPDATE, Constants.USER_MANAGEMENT_SERVICE, response.message, request, null);
        //        return new ResponseFail<UserCustomResponse>().Error(response);
        //    }
        //}

        //[HttpPost]
        //[Authorized]
        //[Route("update-information-root")]
        //public async Task<IActionResult> UpdateInformationRootUser(UpdateInformation request)
        //{
        //    ResponseService<UserCustomResponse> response = await _userService.RootUserUpdateInformation(request);
        //    if (response.status)
        //    {
        //        return Ok(response);
        //    }
        //    else
        //    {
        //        await _logService.CreateKafkaErrorLog(Constants.LOG_TYPE_UPDATE, Constants.USER_MANAGEMENT_SERVICE, response.message, request, null);
        //        return new ResponseFail<UserCustomResponse>().Error(response);
        //    }
        //}

        //[HttpPost]
        //[Authorized]
        //[Route("delete-root")]
        //[PermissionAttributeFilter("User Management", "delete")]
        //public async Task<IActionResult> DeleteRootUser([FromBody] ItemModel<string> request)
        //{
        //    ResponseService<bool> response = await _userService.DeleteRootUser(request.item);
        //    if (response.status)
        //    {
        //        return Ok(response);
        //    }
        //    else
        //    {
        //        await _logService.CreateKafkaErrorLog(Constants.LOG_TYPE_DELETE, Constants.USER_MANAGEMENT_SERVICE, response.message, request, null);
        //        return new ResponseFail<bool>().Error(response);
        //    }
        //}

        //[HttpPost]
        //[Authorized]
        //[Route("check-information")]
        //public async Task<IActionResult> CheckInformation([FromBody] CheckInformationRequest request)
        //{
        //    ResponseService<bool> response = await _userService.CheckInformation(request);
        //    if (response.status)
        //    {
        //        return Ok(response);
        //    }
        //    else
        //    {
        //        return new ResponseFail<bool>().Error(response);
        //    }
        //}
    }    
}
