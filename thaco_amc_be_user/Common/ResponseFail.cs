using Common.Commons;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UserManagement.Common
{
    public class ResponseFail<T> : IActionResult
    {
        private HttpStatusCode statusCode { get; set; }
        private string message { get; set; }
        private Exception exception { get; set; }
        private ResponseService<T> resService { get; set; }
        public ResponseFail() { }

        public ResponseFail(HttpStatusCode statusCode, string errorMessage) : this(statusCode, errorMessage, null)
        { }

        public ResponseFail(HttpStatusCode statusCode, string errorMessage, Exception exception)
        {
            this.statusCode = statusCode;
            this.message = errorMessage;
            this.exception = exception;
        }
        public ResponseFail<T> Error(ResponseService<T> resService)
        {
            this.statusCode = resService.status_code;
            this.message = resService.message;
            this.resService = resService;
            return this;
        }
        public ResponseFail<T> BadRequest(ResponseService<T> resService)
        {
            this.statusCode = HttpStatusCode.BadRequest;
            this.message = resService.message;
            this.resService = resService;
            return this;
        }
        public ResponseFail<T> Unauthorized(ResponseService<T> resService)
        {
            this.statusCode = HttpStatusCode.Unauthorized;
            this.message = resService.message;
            this.resService = resService;
            return this;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            context.HttpContext.Response.StatusCode = (int)statusCode;
            var mes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(resService));
            context.HttpContext.Response.ContentType = "application/json";
            await context.HttpContext.Response.Body.WriteAsync(mes);
        }
    }
}