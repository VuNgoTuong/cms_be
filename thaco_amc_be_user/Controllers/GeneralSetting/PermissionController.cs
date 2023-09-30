using Common;
using Common.Commons;
using Common.Params.Base;
using Microsoft.AspNetCore.Mvc;
using Repository.CustomModel;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using UserManagement.Common;
using UserManagement.CustomAttributes;
using UserManagement.Models.Common;
using UserManagement.Models.Main;
using UserManagement.Services.Implement.GeneralSetting;

namespace UserManagement.Controllers.GeneralSetting
{
    [Route("api/permission")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        //private readonly ILogService _logService;
        private readonly IPermissionService _permissionService;
        public PermissionController(
            //ILogService logService,
            IPermissionService permissionService)
        {
            //_logService = logService;
            _permissionService = permissionService;
        }

        [HttpPost]
        [Authorized]
        [Route("get-all")]
        [PermissionAttributeFilter("Permission Object", "access")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResponseService<ListResult<PermissionResponse>>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ResponseService<ListResult<PermissionResponse>>))]
        public async Task<IActionResult> GetAll([FromBody] PagingParam param)
        {
            ResponseService<ListResult<PermissionResponse>> response = await _permissionService.GetAll(param);
            if (response.status)
            {
                return Ok(response);
            }
            else
            {
                return new ResponseFail<ListResult<PermissionResponse>>().Error(response);
            }
        }

        [HttpPost]
        [Authorized]
        [Route("create")]
        [PermissionAttributeFilter("Permission Object", "create")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResponseService<ListResult<PermissionResponse>>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ResponseService<ListResult<PermissionResponse>>))]
        public async Task<IActionResult> Create([FromBody] PermissionRequest request)
        {
            ResponseService<PermissionResponse> response = await _permissionService.Create(request);
            if (response.status)
            {
                return Ok(response);
            }
            else
            {
                //await _logService.CreateKafkaErrorLog(Constants.LOG_TYPE_CREATE, Constants.PERMISSION_SERVICE, response.message, request, null);
                return new ResponseFail<PermissionResponse>().Error(response);
            }
        }

        [HttpPost]
        [Authorized]
        [Route("update")]
        [PermissionAttributeFilter("Permission Object", "edit")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResponseService<ListResult<PermissionResponse>>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ResponseService<ListResult<PermissionResponse>>))]
        public async Task<IActionResult> Update([FromBody] PermissionRequest request)
        {
            ResponseService<PermissionResponse> response = await _permissionService.Update(request);
            if (response.status)
            {
                return Ok(response);
            }
            else
            {
                //await _logService.CreateKafkaErrorLog(Constants.LOG_TYPE_UPDATE, Constants.PERMISSION_SERVICE, response.message, null, request);
                return new ResponseFail<PermissionResponse>().Error(response);
            }
        }

        [HttpPost]
        [Authorized]
        [Route("delete")]
        [PermissionAttributeFilter("Permission Object", "delete")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResponseService<ListResult<PermissionResponse>>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ResponseService<ListResult<PermissionResponse>>))]
        public async Task<IActionResult> Delete([FromBody] ItemModel<Guid> request)
        {
            ResponseService<bool> response = await _permissionService.Delete(request.item);
            if (response.status)
            {
                return Ok(response);
            }
            else
            {
                //await _logService.CreateKafkaErrorLog(Constants.LOG_TYPE_DELETE, Constants.PERMISSION_SERVICE, response.message, request, null);
                return new ResponseFail<bool>().Error(response);
            }
        }

        [HttpPost]
        [Authorized]
        [Route("get-by-id")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResponseService<PermissionResponse>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ResponseService<PermissionResponse>))]
        public async Task<IActionResult> GetById([FromBody] ItemModel<Guid> obj)
        {
            ResponseService<PermissionResponse> response = await _permissionService.GetById(obj.item);
            if (response.status)
            {
                return Ok(response);
            }
            else
            {
                return new ResponseFail<PermissionResponse>().Error(response);
            }
        }

        [HttpPost]
        [Authorized]
        [Route("get-list-permission-by-user")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResponseService<List<PermissionResShort>>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ResponseService<List<PermissionResShort>>))]
        public async Task<IActionResult> GetListPermissionByUser([FromBody] ItemModel<string> obj)
        {
            ResponseService<List<PermissionResShort>> response = await _permissionService.GetListPermissionByUser(obj.item);
            if (response.status)
            {
                return Ok(response);
            }
            else
            {
                return new ResponseFail<List<PermissionResShort>>().Error(response);
            }
        }

        [HttpPost]
        [Authorized]
        [Route("get-permission-a-object-by-user")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResponseService<PermissionResShort>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ResponseService<PermissionResShort>))]
        public async Task<IActionResult> GetPermissionAObjectByUser([FromBody] PermissionAObjectRequest request)
        {
            PermissionResShort res = await _permissionService.GetPermissionAObjectByUser(request);
            ResponseService<PermissionResShort> response = new ResponseService<PermissionResShort>(res);
            if (response.status)
            {
                if (response.data == null) response.status = false;
                return Ok(response);
            }
            else
            {
                return new ResponseFail<PermissionResShort>().Error(response);
            }
        }

        [HttpPost]
        [Authorized]
        [Route("get-status-permission-by-type-and-name")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResponseService<bool>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ResponseService<bool>))]
        public async Task<IActionResult> GetStatusPermissionByTypeAndName([FromBody] GetPermissionByTypeAndName request)
        {
            ResponseService<bool> response = await _permissionService.GetStatusPermissionByTypeAndName(request);
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
        [Authorized]
        [Route("get-list-permission-by-profile")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResponseService<ListResult<PermissionResponse>>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ResponseService<ListResult<PermissionResponse>>))]
        public async Task<IActionResult> GetListPermissionByProfileId([FromBody] PagingRequest<PermissonSearchRequest> param)
        {
            ResponseService<ListResult<PermissionResponse>> response = await _permissionService.GetPermissionListByProfile(param);
            if (response.status)
            {
                return Ok(response);
            }
            else
            {
                return new ResponseFail<ListResult<PermissionResponse>>().Error(response);
            }
        }
    }
}
