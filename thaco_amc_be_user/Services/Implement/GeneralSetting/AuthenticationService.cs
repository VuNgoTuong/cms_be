using AutoMapper;
using Common;
using Common.Commons;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Repository.CustomModel;
using Repository.Model;
using Repository.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Common;
using UserManagement.Config;
using UserManagement.Models.Main;

namespace UserManagement.Services.Implement.GeneralSetting
{
    public class AuthenticationService : BaseService, IAuthenticationService
    {
        //private readonly ILogService _logService;
        private IHttpContextAccessor _httpContextAccessor;
        public readonly IAuthenticationRepository _authenticationRepository;
        private readonly int tokenExpirationTimeConf = int.Parse(ConfigHelper.Get(Constants.CONF_TOKEN_EXPIRATION_TIME));
        private readonly int tokenExpirationTimeBonusConf = int.Parse(ConfigHelper.Get(Constants.CONF_TOKEN_EXPIRATION_TIME_BONUS));
        public AuthenticationService(
            IAuthenticationRepository authenticationRepository,
            IHttpContextAccessor httpContextAccessor,
            //ILogService logService,
            ILogger logger,
            IConfigManager config,
            IMapper mapper) : base(config, logger, mapper)
        {
            //_logService = logService;
            _httpContextAccessor = httpContextAccessor;
            _authenticationRepository = authenticationRepository;
        }

        public virtual async Task<ResponseService<LoginResponse>> Login(LoginRequest request)
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));

                LoginResponse response = new LoginResponse();
                request.password = HashString.StringToHash(request.password, Constants.HASH_SHA512);

                // Check exists user
                QTTS01_User checkExistsUser = await _authenticationRepository.CheckExiststUsernameAndPassword(request.email, request.password);
                if (checkExistsUser == null)
                {
                    return new ResponseService<LoginResponse>(Constants.USERNAME_OR_PASSWORD_IS_INCORECT).BadRequest(MessCodes.USERNAME_OR_PASSWORD_INCORECT);
                }

                if (!checkExistsUser.is_active)
                {
                    return new ResponseService<LoginResponse>(Constants.ACCOUNT_LOCKED).BadRequest(MessCodes.ACCOUNT_LOCKED);
                }
                // Check tenant is_active
                QTTS01_Tenant checkTenantIsActive = await _authenticationRepository.CheckTenantIsActive(checkExistsUser.tenant_id);
                if (checkTenantIsActive == null)
                {
                    return new ResponseService<LoginResponse>(Constants.TENANT_IS_DEACTIVE).BadRequest(MessCodes.TENANT_IS_DEACTIVE);
                }

                // Token expire time
                int tokenExpiresTime = tokenExpirationTimeConf;
                if (request.is_remember_me)
                {
                    tokenExpiresTime = tokenExpirationTimeConf + tokenExpirationTimeBonusConf;
                }

                // Create token
                GenerateTokenRequest tokenRequest = new GenerateTokenRequest();
                tokenRequest.user_name = checkExistsUser.username;
                tokenRequest.tenant_id = checkExistsUser.tenant_id;
                tokenRequest.is_rootuser = checkExistsUser.is_rootuser;
                tokenRequest.tokenExpiresTime = tokenExpiresTime;
                string token = CommonFunc.GenerateToken(tokenRequest);

                // Set data login to session
                SessionStore.Set(Constants.KEY_SESSION_TOKEN, token);
                SessionStore.Set(Constants.KEY_SESSION_USER_ID, checkExistsUser.username);
                SessionStore.Set(Constants.KEY_SESSION_TENANT_ID, checkExistsUser.tenant_id.ToString());

                response = CommonFuncMain.ToObject<LoginResponse>(checkExistsUser);
                response.token = token;

                // Ghi log login cho root user
                //if (checkExistsUser.is_rootuser)
                //{
                //    await _logService.CreateKafkaLog(Constants.LOG_TYPE_USER_LOGIN, Constants.AUTHENTICATION_SERVICE, null, null);
                //}

                return new ResponseService<LoginResponse>(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<LoginResponse>(ex);
            }
        }
        public virtual async Task<ResponseService<bool>> Logout()
        {
            try
            {
                _logger.LogInfo(GetMethodName(new System.Diagnostics.StackTrace()));
                Guid tenantId = SessionStore.Get<Guid>(Constants.KEY_SESSION_TENANT_ID);
                string currentUser = SessionStore.Get<string>(Constants.KEY_SESSION_USER_ID);

                QTTS01_User checkExistsUser = await _authenticationRepository.CheckExistsUser(currentUser, tenantId);
                if (checkExistsUser == null)
                {
                    return new ResponseService<bool>(Constants.USER_NOT_FOUND).BadRequest(MessCodes.USER_NOT_FOUND);
                }

                string token = _httpContextAccessor.HttpContext.Request.Headers["authorization"];
                if (string.IsNullOrEmpty(token))
                {
                    return new ResponseService<bool>(Constants.USER_LOGOUT_FAILED).BadRequest(MessCodes.LOGOUT_FAIL);
                }

                await TokenHelper.DeactivateAsync(token.Split(' ').Last());

                //// Ghi log logout cho root user
                //if (checkExistsUser.is_rootuser)
                //{
                //    await _logService.CreateKafkaLog(Constants.LOG_TYPE_USER_LOGOUT, Constants.AUTHENTICATION_SERVICE, null, null);
                //}

                return new ResponseService<bool>(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<bool>(ex);
            }
        }

        public async Task<ResponseService<TokenResponse>> CheckAuthentication()
        {
            try
            {
                TokenResponse tokenRes = new TokenResponse();

                string bearerToken = _httpContextAccessor.HttpContext.Request.Headers["authorization"];
                if (string.IsNullOrEmpty(bearerToken))
                {
                    return new ResponseService<TokenResponse>(Constants.AUTHEN_FAILED).BadRequest(MessCodes.AUTHEN_FAILED);
                }

                string token = bearerToken.Split(' ').Last();
                GetPrincipalModel principal = await Task.Run(() => CommonFunc.GetPrincipalFromToken(token));
                if (principal.claimsPrincipal == null)
                {
                    return new ResponseService<TokenResponse>(Constants.AUTHEN_FAILED).BadRequest(MessCodes.AUTHEN_FAILED);
                }

                switch (principal.signatureAlgorithm)
                {
                    case "RS256":
                        ClaimsModel claimModel = new ClaimsModel();
                        string strValue = principal.claimsPrincipal.Claims.FirstOrDefault(claim => claim.Type == "c247_claims")?.Value ?? "";
                        claimModel = JsonConvert.DeserializeObject<ClaimsModel>(strValue);
                        tokenRes.username = claimModel?.user_name;
                        tokenRes.is_rootuser = !string.IsNullOrEmpty(claimModel.is_rootuser) ? bool.Parse(claimModel.is_rootuser) : false;                       
                        tokenRes.tenant_id = claimModel?.tenant_id;
                        break;
                    case "HS256":
                        tokenRes.username = principal.claimsPrincipal.Claims.First(claim => claim.Type == "username").Value;
                        tokenRes.is_rootuser = !string.IsNullOrEmpty(principal.claimsPrincipal.Claims.First(claim => claim.Type == "is_rootuser")?.Value) ? bool.Parse(principal.claimsPrincipal.Claims.First(claim => claim.Type == "is_rootuser")?.Value) : false;
                        tokenRes.tenant_id = principal.claimsPrincipal.Claims.First(claim => claim.Type == "tenant_id").Value;
                        break;
                }

                return new ResponseService<TokenResponse>(tokenRes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseService<TokenResponse>(ex);
            }
        }
    }
}
