using Common;
using Common.Commons;
using Microsoft.AspNetCore.Mvc;
using Repository.CustomModel;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Net;
using System.Threading.Tasks;
using UserManagement.Common;
using UserManagement.CustomAttributes;
using UserManagement.Models.Common;
using UserManagement.Models.Main;
using UserManagement.Services.Implement.GeneralSetting;

namespace UserManagement.Controllers.GeneralSetting
{
    [Route("api/permission-object")]
    [ApiController]
    public class PermissionObjectController : ControllerBase
    {
        //private readonly ILogService _logService;
        private readonly IPermissionObjectService _permissionObjectService;
        public PermissionObjectController(
            //ILogService logService,
            IPermissionObjectService permissionObjectService)
        {
            //_logService = logService;
            _permissionObjectService = permissionObjectService;
        }

        [HttpPost]
        [Authorized]
        [Route("get-all")]
        [PermissionAttributeFilter("Permission Object", "access")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResponseService<ListResult<PermissionObjectCustom>>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ResponseService<ListResult<PermissionObjectCustom>>))]
        public async Task<IActionResult> GetAll([FromBody] PagingRequest<PermissionObjectSearchRequest> param)
        {
            ResponseService<ListResult<PermissionObjectCustom>> response = await _permissionObjectService.GetAll(param);
            if (response.status)
            {
                return Ok(response);
            }
            else
            {
                return new ResponseFail<ListResult<PermissionObjectCustom>>().Error(response);
            }
        }

        [HttpPost]
        [Authorized]
        [Route("get-by-id")]
        [PermissionAttributeFilter("Permission Object", "access")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResponseService<PermissionObjectResponse>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ResponseService<PermissionObjectResponse>))]
        public async Task<IActionResult> GetById([FromBody] ItemModel<Guid> obj)
        {
            ResponseService<PermissionObjectResponse> response = await _permissionObjectService.GetById(obj.item);
            if (response.status)
            {
                return Ok(response);
            }
            else
            {
                return new ResponseFail<PermissionObjectResponse>().Error(response);
            }
        }

        [HttpPost]
        [Authorized]
        [Route("create")]
        [PermissionAttributeFilter("Permission Object", "create")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResponseService<PermissionObjectResponse>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ResponseService<PermissionObjectResponse>))]

        public async Task<IActionResult> Create([FromBody] PermissionObjectRequest request)
        {
            ResponseService<PermissionObjectResponse> response = await _permissionObjectService.Create(request);
            if (response.status)
            {

                return Ok(response);

            }
            else
            {
                //await _logService.CreateKafkaErrorLog(Constants.LOG_TYPE_CREATE, Constants.PERMISSION_OBJECT_SERVICE, response.message, request, null);
                return new ResponseFail<PermissionObjectResponse>().Error(response);
            }
        }

        [HttpPost]
        [Authorized]
        [Route("update")]
        [PermissionAttributeFilter("Permission Object", "edit")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResponseService<PermissionObjectResponse>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ResponseService<PermissionObjectResponse>))]
        public async Task<IActionResult> Update([FromBody] PermissionObjectRequest request)
        {
            ResponseService<PermissionObjectResponse> response = await _permissionObjectService.Update(request);
            if (response.status)
            {
                return Ok(response);
            }
            else
            {
                //await _logService.CreateKafkaErrorLog(Constants.LOG_TYPE_UPDATE, Constants.PERMISSION_OBJECT_SERVICE, response.message, null, request);
                return new ResponseFail<PermissionObjectResponse>().Error(response);
            }
        }

        [HttpPost]
        [Authorized]
        [Route("delete")]
        [PermissionAttributeFilter("Permission Object", "delete")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResponseService<bool>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ResponseService<bool>))]
        public async Task<IActionResult> Delete([FromBody] ItemModel<Guid> request)
        {
            ResponseService<bool> response = await _permissionObjectService.Delete(request.item);
            if (response.status)
            {
                return Ok(response);
            }
            else
            {
                //await _logService.CreateKafkaErrorLog(Constants.LOG_TYPE_DELETE, Constants.PERMISSION_OBJECT_SERVICE, response.message, request, null);
                return new ResponseFail<bool>().Error(response);
            }
        }
    }
}
