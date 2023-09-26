using Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using System;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Config;

namespace UserManagement.Common
{
    public static class TokenHelper
    {
        private static MemoryDistributedCache _cache;
        private static IHttpContextAccessor _httpContextAccessor;
        private static int tokenExpirationTime = int.Parse(ConfigManager.StaticGet(Constants.CONF_TOKEN_EXPIRATION_TIME));
        static TokenHelper()
        {
            _cache = new MemoryDistributedCache(Options.Create(new MemoryDistributedCacheOptions()));
            _httpContextAccessor = new HttpContextAccessor();
        }

        public static async Task<bool> IsCurrentActiveToken() => await IsActiveAsync(GetCurrentAsync());

        public static async Task DeactivateCurrentAsync() => await DeactivateAsync(GetCurrentAsync());

        public static async Task<bool> IsActiveAsync(string token) => await _cache.GetStringAsync(GetKey(token)) == null;

        public static async Task DeactivateAsync(string token)
        {
            await _cache.SetStringAsync(GetKey(token),
                  " ", new DistributedCacheEntryOptions
                  {
                      AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(tokenExpirationTime)
                  });
        }
        private static string GetCurrentAsync()
        {
            string authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["authorization"].ToString();

            return authorizationHeader == StringValues.Empty ? string.Empty : authorizationHeader.Split(' ').Last();
        }

        private static string GetKey(string token) => $"tokens:{token}:deactivated";
    }
}