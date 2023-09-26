using Common;
using Common.Commons;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using UserManagement.Common;
using UserManagement.Config;

namespace UserManagement.CustomAttributes
{
    public class PermissionAttributeFilter : ActionFilterAttribute, IActionFilter
    {
        private readonly string _connectionString;
        private string _permissionName = "";
        private string _permissionType = "";
        public PermissionAttributeFilter(string permissonName, string permissionType)
        {
            _permissionName = permissonName;
            _permissionType = permissionType;
            _connectionString = ConfigHelper.Get("ConnectionStrings", "DefaultConnection");
        }
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            StringValues headerValues;
            context.HttpContext.Request.Headers.TryGetValue(Constants.CONF_API_SECRET_KEY, out headerValues);
            string secretKey = null;

            if (headerValues.Any())
            {
                secretKey = headerValues.FirstOrDefault();
            }

            if (string.IsNullOrEmpty(secretKey) || secretKey != ConfigManager.StaticGet(Constants.CONF_API_SECRET_KEY))
            {
                bool result = await CheckPermissionByUser(SessionStore.Get<string>(Constants.KEY_SESSION_USER_ID), _permissionName, _permissionType);
                if (!result)
                {
                    context.Result = new ObjectResult(Constants.ACCESS_DENIED) { StatusCode = (int)HttpStatusCode.Forbidden };
                }
                else
                {
                    await next();
                }
            }
            else
            {
                await next();
            }
        }

        // Lấy quyền của người dùng ở 1 loại quyền cụ thể của 1 đối tượng phân quyền.
        public async Task<bool> CheckPermissionByUser(string username, string permissionName, string permissionType)
        {
            try
            {
                Guid tenantId = SessionStore.Get<Guid>(Constants.KEY_SESSION_TENANT_ID);
                DynamicParameters parameters = new DynamicParameters(
                new
                {
                    username = username,
                    tenantId = tenantId,
                    permissionName = permissionName,
                    permissionType = permissionType,
                    rootPermission = string.Join(',', Constants.ROOT_PERMISSIONS)
                });

                using (SqlConnection sql = new SqlConnection(_connectionString))
                {
                    sql.Open();
                    return await sql.QueryFirstOrDefaultAsync<bool>("usp_Auth_Check_Permision_By_User", parameters, commandType: System.Data.CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
