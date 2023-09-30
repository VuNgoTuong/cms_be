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
    [Route("api/module")]
    [ApiController]
    public class ModuleController : ControllerBase
    {
        //private readonly ILogService _logService;
        private readonly IModuleService _moduleService;
        public ModuleController(
            //ILogService logService,
            IModuleService moduleService)
        {
            //_logService = logService;
            _moduleService = moduleService;
        }

        [HttpPost]
        [Authorized]
        [Route("get-all")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResponseService<ListResult<ModuleResponse>>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ResponseService<ListResult<ModuleResponse>>))]
        public async Task<IActionResult> GetAll([FromBody] PagingParam param)
        {
            ResponseService<ListResult<ModuleResponse>> response = await _moduleService.GetAll(param);
            if (response.status)
            {
                return Ok(response);
            }
            else
            {
                return new ResponseFail<ListResult<ModuleResponse>>().Error(response);
            }
        }

        [HttpPost]
        [Authorized]
        [Route("get-all-is-show")]
        public async Task<IActionResult> GetAllWithIsShow()
        {
            var response = await _moduleService.GetAllWithIsShow();
            if (response.status)
            {
                return Ok(response);
            }
            else
            {
                return new ResponseFail<List<ModuleCustomResponse>>().Error(response);
            }
        }

        [HttpPost]
        [Authorized]
        [Route("get-by-id")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResponseService<ModuleResponse>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ResponseService<ModuleResponse>))]
        public async Task<IActionResult> GetById([FromBody] ItemModel<Guid> obj)
        {
            ResponseService<ModuleResponse> response = await _moduleService.GetById(obj.item);
            if (response.status)
            {
                return Ok(response);
            }
            else
            {
                return new ResponseFail<ModuleResponse>().Error(response);
            }
        }

        [HttpPost]
        [Authorized]
        [Route("create")]
        [PermissionAttributeFilter("Module", "create")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResponseService<ModuleResponse>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ResponseService<ModuleResponse>))]
        public async Task<IActionResult> Create([FromBody] ModuleRequest request)
        {
            ResponseService<ModuleResponse> response = await _moduleService.Create(request);
            if (response.status)
            {
                return Ok(response);
            }
            else
            {
                //await _logService.CreateKafkaErrorLog(Constants.LOG_TYPE_CREATE, Constants.MODULE_SERVICE, response.message, request, null);
                return new ResponseFail<ModuleResponse>().Error(response);
            }
        }

        [HttpPost]
        [Authorized]
        [Route("update")]
        [PermissionAttributeFilter("Module", "edit")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResponseService<ModuleResponse>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ResponseService<ModuleResponse>))]
        public async Task<IActionResult> Update([FromBody] ModuleRequest request)
        {
            ResponseService<ModuleResponse> response = await _moduleService.Update(request);
            if (response.status)
            {
                return Ok(response);
            }
            else
            {
                //await _logService.CreateKafkaErrorLog(Constants.LOG_TYPE_UPDATE, Constants.MODULE_SERVICE, response.message, null, request);
                return new ResponseFail<ModuleResponse>().Error(response);
            }
        }

        [HttpPost]
        [Authorized]
        [Route("delete")]
        [PermissionAttributeFilter("Module", "delete")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResponseService<bool>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ResponseService<bool>))]
        public async Task<IActionResult> Delete([FromBody] ItemModel<Guid> request)
        {
            ResponseService<bool> response = await _moduleService.Delete(request.item);
            if (response.status)
            {
                return Ok(response);
            }
            else
            {
                //await _logService.CreateKafkaErrorLog(Constants.LOG_TYPE_DELETE, Constants.MODULE_SERVICE, response.message, request, null);
                return new ResponseFail<bool>().Error(response);
            }
        }
    }
}
