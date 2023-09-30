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
    [Route("api/group")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;
        public GroupController(
            IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpPost]
        [Authorized]
        [Route("get-all")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResponseService<ListResult<GroupResponse>>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ResponseService<ListResult<GroupResponse>>))]

        public async Task<IActionResult> GetAll([FromBody] PagingParamCustom param)
        {
            ResponseService<ListResult<GroupResponse>> response = await _groupService.GetAll(param);
            if (response.status)
            {
                return Ok(response);
            }
            else
            {
                return new ResponseFail<ListResult<GroupResponse>>().Error(response);
            }
        }

        [HttpPost]
        [Authorized]
        [Route("get-by-id")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResponseService<ListResult<GroupResponse>>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ResponseService<ListResult<GroupResponse>>))]
        public async Task<IActionResult> GetById([FromBody] ItemModelCustom<Guid> obj)
        {
            ResponseService<GroupResponse> response = await _groupService.GetById(obj.item);
            if (response.status)
            {
                return Ok(response);
            }
            else
            {
                return new ResponseFail<GroupResponse>().Error(response);
            }
        }

        [HttpPost]
        [Authorized]
        [Route("create")]
        [PermissionAttributeFilter("Group Management", "create")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResponseService<GroupResponse>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ResponseService<GroupResponse>))]

        public async Task<IActionResult> Create([FromBody] GroupRequest request)
        {
            ResponseService<GroupResponse> response = await _groupService.Create(request);
            if (response.status)
            {
                return Ok(response);
            }
            else
            {
                return new ResponseFail<GroupResponse>().Error(response);
            }
        }

        [HttpPost]
        [Authorized]
        [Route("update")]
        [PermissionAttributeFilter("Group Management", "edit")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResponseService<GroupResponse>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ResponseService<GroupResponse>))]
        public async Task<IActionResult> Update([FromBody] GroupRequest request)
        {
            ResponseService<GroupResponse> response = await _groupService.Update(request);
            if (response.status)
            {
                return Ok(response);
            }
            else
            {
                return new ResponseFail<GroupResponse>().Error(response);
            }
        }

        [HttpPost]
        [Authorized]
        [Route("delete")]
        [PermissionAttributeFilter("Group Management", "delete")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResponseService<bool>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ResponseService<bool>))]
        public async Task<IActionResult> Delete([FromBody] ItemModelCustom<Guid> request)
        {
            ResponseService<bool> response = await _groupService.Delete(request.item);
            if (response.status)
            {
                return Ok(response);
            }
            else
            {
                return new ResponseFail<bool>().Error(response);
            }
        }
    }
}
