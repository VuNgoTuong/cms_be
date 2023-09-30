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

namespace UserManagement.Controllers.GeneralSetting
{
    [Route("api/bank")]
    [ApiController]
    public class BankController : ControllerBase
    {
        private readonly IBankService _bankService;
        public BankController(
            IBankService bankService)
        {
            _bankService = bankService;
        }

        [HttpPost]
        [Authorized]
        [Route("get-all")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResponseService<ListResult<BankResponse>>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ResponseService<ListResult<BankResponse>>))]
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
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResponseService<ListResult<BankResponse>>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ResponseService<ListResult<BankResponse>>))]
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

        [HttpPost]
        [Authorized]
        [Route("create")]
        [PermissionAttributeFilter("Bank Management", "create")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResponseService<BankResponse>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ResponseService<BankResponse>))]

        public async Task<IActionResult> Create([FromBody] BankRequest request)
        {
            ResponseService<BankResponse> response = await _bankService.Create(request);
            if (response.status)
            {
                return Ok(response);
            }
            else
            {
                return new ResponseFail<BankResponse>().Error(response);
            }
        }

        [HttpPost]
        [Authorized]
        [Route("update")]
        [PermissionAttributeFilter("Bank Management", "edit")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResponseService<BankResponse>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ResponseService<BankResponse>))]
        public async Task<IActionResult> Update([FromBody] BankRequest request)
        {
            ResponseService<BankResponse> response = await _bankService.Update(request);
            if (response.status)
            {
                return Ok(response);
            }
            else
            {
                return new ResponseFail<BankResponse>().Error(response);
            }
        }

        [HttpPost]
        [Authorized]
        [Route("delete")]
        [PermissionAttributeFilter("Bank Management", "delete")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResponseService<bool>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ResponseService<bool>))]
        public async Task<IActionResult> Delete([FromBody] ItemModel<Guid> request)
        {
            ResponseService<bool> response = await _bankService.Delete(request.item);
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
