using Common.Commons;
using Common.Params.Base;
using Microsoft.AspNetCore.Mvc;
using Repository.CustomModel;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System;
using System.Threading.Tasks;
using UserManagement.Common;
using UserManagement.CustomAttributes;
using UserManagement.Models.Common;
using UserManagement.Models.Main;
using UserManagement.Services.Implement.GeneralSetting;
using UserManagement.Services.Implement.UserSetting;

namespace UserManagement.Controllers.GeneralSetting
{
    [Route("api/bank")]
    [ApiController]
    public class BankController : ControllerBase
    {
        //private readonly ILogService _logService;
        private readonly IBankService _bankService;
        private readonly IMapProfileUserService _mapProfileUserService;
        public BankController(
            //ILogService logService,
            IBankService bankService,
            IMapProfileUserService mapProfileUserService)
        {
            //_logService = logService;
            _bankService = bankService;
            _mapProfileUserService = mapProfileUserService;
        }

        [HttpPost]
        [Authorized]
        [Route("get-all")]
        public async Task<IActionResult> GetAll([FromBody] PagingParam param)
        {
            ResponseService<ListResult<BankResponse>> response = await _bankService.GetAll(param);
            if (response.status)
            {
                return Ok(response);
            }
            else
            {
                return new ResponseFail<ListResult<BankResponse>>().Error(response);
            }
        }

        [HttpPost]
        [Authorized]
        [Route("get-by-id")]
        public async Task<IActionResult> GetById([FromBody] ItemModel<Guid> obj)
        {
            ResponseService<BankResponse> response = await _bankService.GetById(obj.item);
            if (response.status)
            {
                return Ok(response);
            }
            else
            {
                return new ResponseFail<BankResponse>().Error(response);
            }
        }

        //[HttpPost]
        //[Authorized]
        //[Route("create")]
        //[PermissionAttributeFilter("User Management", "create")]
        //[SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResponseService<ProfileResponse>))]
        //[SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ResponseService<ProfileResponse>))]

        //public async Task<IActionResult> Create([FromBody] ProfileRequest request)
        //{
        //    ResponseService<ProfileResponse> response = await _profileService.Create(request);
        //    if (response.status)
        //    {
        //        return Ok(response);
        //    }
        //    else
        //    {
        //        //await _logService.CreateKafkaErrorLog(Constants.LOG_TYPE_CREATE, Constants.PROFILE_SERVICE, response.message, request, null);
        //        return new ResponseFail<ProfileResponse>().Error(response);
        //    }
        //}

        //[HttpPost]
        //[Authorized]
        //[Route("add-user-to-profile")]
        //[PermissionAttributeFilter("User Management", "create")]
        //[SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResponseService<ProfileResponse>))]
        //[SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ResponseService<ProfileResponse>))]

        //public async Task<IActionResult> AddUserToProfile([FromBody] AddUserToProfile request)
        //{
        //    var response = await _mapProfileUserService.Create(request);
        //    if (response.status)
        //    {
        //        return Ok(response);
        //    }
        //    else
        //    {
        //        //await _logService.CreateKafkaErrorLog(Constants.LOG_TYPE_UPDATE,Constants.ADD_USER_TO_PROFILE_FAILED, Constants.PROFILE_SERVICE, response.message, request, null);
        //        return new ResponseFail<bool>().Error(response);
        //    }
        //}

        //[HttpGet]
        //[Authorized]
        //[Route("get-list-user-by-profile")]
        //[PermissionAttributeFilter("User Management", "access")]
        //[SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResponseService<UserModel>))]
        //[SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ResponseService<UserModel>))]
        //public async Task<IActionResult> GetUsersByProfile([FromBody] PagingParam param)
        //{
        //    var response = await _profileService.GetListUserByProfile(param);
        //    if (response.status)
        //    {
        //        return Ok(response);
        //    }
        //    else
        //    {
        //        return new ResponseFail<ListResult<UserModel>>().Error(response);
        //    }
        //}

        //[HttpPost]
        //[Authorized]
        //[Route("update")]
        //[PermissionAttributeFilter("User Management", "edit")]
        //[SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResponseService<ProfileResponse>))]
        //[SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ResponseService<ProfileResponse>))]
        //public async Task<IActionResult> Update([FromBody] ProfileRequest request)
        //{
        //    ResponseService<ProfileResponse> response = await _profileService.Update(request);
        //    if (response.status)
        //    {
        //        return Ok(response);
        //    }
        //    else
        //    {
        //        //await _logService.CreateKafkaErrorLog(Constants.LOG_TYPE_UPDATE, Constants.PROFILE_SERVICE, response.message, null, request);
        //        return new ResponseFail<ProfileResponse>().Error(response);
        //    }
        //}

        //[HttpPost]
        //[Authorized]
        //[Route("delete")]
        //[PermissionAttributeFilter("User Management", "delete")]
        //[SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResponseService<bool>))]
        //[SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ResponseService<bool>))]
        //public async Task<IActionResult> Delete([FromBody] ItemModel<Guid> request)
        //{
        //    ResponseService<bool> response = await _profileService.Delete(request.item);
        //    if (response.status)
        //    {
        //        return Ok(response);
        //    }
        //    else
        //    {
        //        //await _logService.CreateKafkaErrorLog(Constants.LOG_TYPE_DELETE, Constants.PROFILE_SERVICE, response.message, request, null);
        //        return new ResponseFail<bool>().Error(response);
        //    }
        //}

        //[HttpPost]
        //[Authorized]
        //[Route("update-permission-in-profile")]
        //[PermissionAttributeFilter("User Management", "edit")]
        //[SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResponseService<bool>))]
        //[SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ResponseService<bool>))]
        //public async Task<IActionResult> UpdatePermissionInProfile([FromBody] List<UpdatePermissonRequest> request)
        //{
        //    ResponseService<bool> response = await _profileService.UpdatePermissionInProfile(request);
        //    if (response.status)
        //    {
        //        return Ok(response);
        //    }
        //    else
        //    {
        //        //await _logService.CreateKafkaErrorLog(Constants.LOG_TYPE_UPDATE, Constants.UPDATED_PERMISSION_ON_PROFILE_FAILED, Constants.PROFILE_SERVICE, response.message, request, null);
        //        return new ResponseFail<bool>().Error(response);
        //    }
        //}

        //[HttpPost]
        //[Authorized]
        //[Route("delete-user-in-profile")]
        //[PermissionAttributeFilter("User Management", "delete")]
        //[SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResponseService<bool>))]
        //[SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ResponseService<bool>))]
        //public async Task<IActionResult> DeleteUserInProfile([FromBody] DeleteUserInProfile request)
        //{
        //    ResponseService<bool> response = await _profileService.DeleteUserInProfile(request);
        //    if (response.status)
        //    {
        //        return Ok(response);
        //    }
        //    else
        //    {
        //        //await _logService.CreateKafkaErrorLog(Constants.LOG_TYPE_UPDATE,Constants.REMOVE_USER_FROM_PROFILE_FAILED, Constants.PROFILE_SERVICE, response.message, request, null);
        //        return new ResponseFail<bool>().Error(response);
        //    }
        //}

        //[HttpPost]
        //[Authorized]
        //[Route("get-list-user-by-profile-root")]
        ////[PermissionAttributeFilter("User Management", "access")]
        //public async Task<IActionResult> GetRootUsersByProfile([FromBody] PagingRequest<UserInProfileSearchRequest> param)
        //{
        //    var response = await _profileService.GetRootUserByProfile(param);
        //    if (response.status)
        //    {
        //        return Ok(response);
        //    }
        //    else
        //    {
        //        return new ResponseFail<ListResult<UserInProfileCustomResponse>>().Error(response);
        //    }
        //}
    }
}
