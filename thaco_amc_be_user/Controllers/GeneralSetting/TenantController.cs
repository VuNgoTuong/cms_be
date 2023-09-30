using Common.Commons;
using Common.Params.Base;
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
    [Route("api/tenant")]
    [ApiController]
    public class TenantController : ControllerBase
    {
        private readonly ITenantService _tenantService;
        public TenantController(
            ITenantService tenantService)
        {
            _tenantService = tenantService;
        }

        [HttpPost]
        [Authorized]
        [Route("get-all")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResponseService<ListResult<TenantResponse>>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ResponseService<ListResult<TenantResponse>>))]
        public async Task<IActionResult> GetAll([FromBody] PagingParamCustom param)
        {
            ResponseService<ListResult<TenantResponse>> response = await _tenantService.GetAllCustom(param);
            if (response.status)
            {
                return Ok(response);
            }
            else
            {
                return new ResponseFail<ListResult<TenantResponse>>().Error(response);
            }
        }

        [HttpPost]
        [Authorized]
        [Route("get-by-id")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResponseService<ListResult<TenantResponse>>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ResponseService<ListResult<TenantResponse>>))]
        public async Task<IActionResult> GetById([FromBody] ItemModelCustom<Guid> obj)
        {
            ResponseService<TenantResponse> response = await _tenantService.GetById(obj.item);
            if (response.status)
            {
                return Ok(response);
            }
            else
            {
                return new ResponseFail<TenantResponse>().Error(response);
            }
        }

        [HttpPost]
        [Authorized]
        [Route("create")]
        [PermissionAttributeFilter("Tenant Management", "create")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResponseService<TenantResponse>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ResponseService<TenantResponse>))]

        public async Task<IActionResult> Create([FromBody] TenantRequest request)
        {
            ResponseService<TenantResponse> response = await _tenantService.Create(request);
            if (response.status)
            {
                return Ok(response);
            }
            else
            {
                return new ResponseFail<TenantResponse>().Error(response);
            }
        }

        [HttpPost]
        [Authorized]
        [Route("update")]
        [PermissionAttributeFilter("Tenant Management", "edit")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResponseService<TenantResponse>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ResponseService<TenantResponse>))]
        public async Task<IActionResult> Update([FromBody] TenantRequest request)
        {
            ResponseService<TenantResponse> response = await _tenantService.Update(request);
            if (response.status)
            {
                return Ok(response);
            }
            else
            {
                return new ResponseFail<TenantResponse>().Error(response);
            }
        }

        [HttpPost]
        [Authorized]
        [Route("delete")]
        [PermissionAttributeFilter("Tenant Management", "delete")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResponseService<bool>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ResponseService<bool>))]
        public async Task<IActionResult> Delete([FromBody] ItemModelCustom<Guid> request)
        {
            ResponseService<bool> response = await _tenantService.Delete(request.item);
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
