using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using UserManagement.Config;

namespace UserManagement.Common
{
    public static class SessionStore
    {
        private static IHttpContextAccessor httpContextAccessor = ConfigContainerDJ.CreateInstance<IHttpContextAccessor>();
        public static void Set<T>(string key, T data)
        {
            string serializedData = JsonConvert.SerializeObject(data);
            httpContextAccessor.HttpContext.Session.SetString(key, serializedData);
        }

        public static T Get<T>(string key)
        {
            if (httpContextAccessor.HttpContext.Session == null)
            {
                return default;
            }

            string data = httpContextAccessor.HttpContext.Session.GetString(key);
            if (!string.IsNullOrEmpty(data))
            {
                return JsonConvert.DeserializeObject<T>(data);
            }

            return default(T);
        }
    }
}

