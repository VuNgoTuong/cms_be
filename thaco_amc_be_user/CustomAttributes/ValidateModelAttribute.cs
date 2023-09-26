using Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace UserManagement.CustomAttributes
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var modelState = context.ModelState;

            if (!modelState.IsValid)
                context.Result = new ContentResult()
                {
                    Content = Constants.MODEL_STATE_INVALID,
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            base.OnActionExecuting(context);
        }
    }
}
