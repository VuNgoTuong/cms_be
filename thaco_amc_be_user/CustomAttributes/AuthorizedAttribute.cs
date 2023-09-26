using Common;
using Common.Commons;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Net.Http.Headers;
using System;
using System.Net;
using System.Threading.Tasks;
using UserManagement.Common;
using UserManagement.Config;
using static Common.Constants;

namespace UserManagement.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizedAttribute : Attribute, IAsyncAuthorizationFilter
    {
        private static ILogger _logger = ConfigContainerDJ.CreateInstance<ILogger>();
        private AUTHOR authorDefault = AUTHOR.TOKEN_OR_KEY;

        public AuthorizedAttribute()
        {
            this.authorDefault = AUTHOR.TOKEN_OR_KEY;
        }

        public AuthorizedAttribute(AUTHOR authorType)
        {
            this.authorDefault = authorType;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (!await Authorize(context.HttpContext))
            {
                ResponseService<string> response = new ResponseService<string>(Constants.USER_IS_INVALID).Unauthorized();
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                context.Result = new JsonResult(response);
            }

            return;
        }

        private async Task<bool> Authorize(HttpContext actionContext)
        {
            try
            {
                var header = actionContext.Request.Headers;

                string paramSecretKey = header.ContainsKey(Constants.CONF_API_SECRET_KEY) ? header[Constants.CONF_API_SECRET_KEY].ToString() : null;
                string token = actionContext.Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
                // validate token
                if (this.authorDefault == AUTHOR.TOKEN)
                {
                    return CommonFunc.ValidateToken(token);
                }
                else if (this.authorDefault == AUTHOR.SECRET_KEY) // author secret, key
                {
                    if (paramSecretKey == ConfigManager.StaticGet(Constants.CONF_API_SECRET_KEY)) return true;
                }
                else if (this.authorDefault == AUTHOR.TOKEN_OR_KEY)
                {
                    if (paramSecretKey == ConfigManager.StaticGet(Constants.CONF_API_SECRET_KEY)) return true;
                    return CommonFunc.ValidateToken(token);
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return false;
            }
        }
    }
}
