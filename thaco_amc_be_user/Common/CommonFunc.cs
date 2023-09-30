using Common;
using Common.Commons;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Repository.CustomModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Config;
using UserManagement.Models.Common;
using UserManagement.Models.Main;

namespace UserManagement.Common
{
    public class CommonFunc
    {
        private static ILogger _logger = ConfigContainerDJ.CreateInstance<ILogger>();
        private static readonly string secretKey = ConfigHelper.Get(Constants.CONF_SECRET_KEY);
        public static bool ValidateToken(string token)
        {
            try
            {
                GetPrincipalModel principal = GetPrincipalFromToken(token);
                if (principal.claimsPrincipal == null)
                {
                    return false;
                }

                switch (principal.signatureAlgorithm)
                {
                    case "RS256":
                        ClaimsModel claimModel = new ClaimsModel();
                        string strValue = principal.claimsPrincipal.Claims.FirstOrDefault(claim => claim.Type == "c247_claims")?.Value ?? "";
                        claimModel = JsonConvert.DeserializeObject<ClaimsModel>(strValue);
                        SessionStore.Set(Constants.KEY_SESSION_TOKEN, token);
                        SessionStore.Set(Constants.KEY_SESSION_USER_ID, claimModel?.user_name);
                        SessionStore.Set(Constants.KEY_SESSION_TENANT_ID, claimModel?.tenant_id);
                        //SessionStore.Set(Constants.KEY_SESSION_IS_ADMIN, claimModel?.is_administrator);
                        SessionStore.Set(Constants.KEY_SESSION_IS_ROOT, claimModel?.is_rootuser);
                        //SessionStore.Set(Constants.KEY_SESSION_IS_SUPERVISOR, claimModel?.is_supervisor);                 
                        break;
                    case "HS256":
                        ClaimsIdentity identity = null;
                        identity = (ClaimsIdentity)principal.claimsPrincipal.Identity;
                        SessionStore.Set(Constants.KEY_SESSION_TOKEN, token);
                        SessionStore.Set(Constants.KEY_SESSION_USER_ID, principal.claimsPrincipal.Claims.First(claim => claim.Type == "username").Value);
                        SessionStore.Set(Constants.KEY_SESSION_TENANT_ID, principal.claimsPrincipal.Claims.First(claim => claim.Type == "tenant_id").Value);
                        SessionStore.Set(Constants.KEY_SESSION_IS_ROOT, principal.claimsPrincipal.Claims.First(claim => claim.Type == "is_rootuser").Value);
                        break;
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return false;
            }
        }
        public static GetPrincipalModel GetPrincipalFromToken(string token)
        {
            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken tokenS = tokenHandler.ReadJwtToken(token);
                string signatureAlgorithm = tokenS.SignatureAlgorithm;
                string exp = tokenS.Claims.First(claim => claim.Type == "exp").Value;

                switch (signatureAlgorithm)
                {
                    case "RS256":
                        // Handle SSO token
                        TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateAudience = false,
                            ValidateIssuer = false,
                            ValidateIssuerSigningKey = true,
                            ValidateLifetime = false,
                            IssuerSigningKeys = ConfigAuth.openIdConfig.SigningKeys //Key giải mã token  
                        };

                        SecurityToken securityToken;
                        long currentDate = ConvertToTimestamp(DateTime.UtcNow);
                        if (long.Parse(exp) >= currentDate)
                        {
                            ClaimsPrincipal principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
                            return new GetPrincipalModel(principal, "RS256");
                        }
                        break;
                    case "HS256":
                        // Handle local token
                        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
                        TokenValidationParameters tokenValidationParameters_local = new TokenValidationParameters
                        {
                            ValidateAudience = false,
                            ValidateIssuer = false,
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = key,
                            ValidateLifetime = false
                        };

                        SecurityToken securityTokenLocal;
                        JwtSecurityToken tokenS_local = tokenHandler.ReadToken(token) as JwtSecurityToken;
                        string exp_local = tokenS_local.Claims.First(claim => claim.Type == "exp").Value;
                        long currentDateTime = ConvertToTimestamp(DateTime.UtcNow);
                        if (long.Parse(exp_local) >= currentDateTime)
                        {
                            ClaimsPrincipal principal = tokenHandler.ValidateToken(token, tokenValidationParameters_local, out securityTokenLocal);
                            return new GetPrincipalModel(principal, "HS256");
                        }
                        break;
                }
                return new GetPrincipalModel(null, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new GetPrincipalModel(null, "");
            }
        }
        public static string GenerateToken(GenerateTokenRequest request)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.UTF8.GetBytes(ConfigHelper.Get(Constants.CONF_SECRET_KEY));

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                                    new Claim("username", request.user_name),
                                    new Claim("tenant_id",request.tenant_id.ToString()),
                                    new Claim("is_rootuser",request.is_rootuser.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(request.tokenExpiresTime),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        
        public static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static long ConvertToTimestamp(DateTime value)
        {
            TimeSpan elapsedTime = value - Epoch;
            return (long)elapsedTime.TotalSeconds;
        }
        public static string GetMethodName(StackTrace stackTrace)
        {
            var method = stackTrace.GetFrame(0).GetMethod();

            string _methodName = method.DeclaringType.FullName;

            if (_methodName.Contains(">") || _methodName.Contains("<"))
            {
                _methodName = _methodName.Split('<', '>')[1];
            }
            else
            {
                _methodName = method.Name;
            }

            return _methodName;
        }
        public static T DeepCopy<T>(T obj)
        {
            var str = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            var ret = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(str);
            return ret;
        }
     
        /// <summary>
        ///  Generate unique number
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public static string GenerateUniqueNumber(int start_range, int end_range)
        {
            List<int> randomNumberList = new List<int>();
            int myNumber = 0;
            Random a = new Random();
            myNumber = a.Next(start_range, end_range);
            while (randomNumberList.Contains(myNumber))
                myNumber = a.Next(start_range, end_range);
            return myNumber.ToString();
        }

        /// <summary>
        ///  Method check file is xlsx 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public static bool CheckExcelFile(IFormFile file)
        {
            var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
            return (extension == ".xlsx");
        }

        /// <summary>
        ///  Trim string until to specific character
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public static string GetUntilOrEmpty(string text, string stopAt)
        {
            if (!String.IsNullOrWhiteSpace(text))
            {
                int charLocation = text.IndexOf(stopAt, StringComparison.Ordinal);

                if (charLocation > 0)
                {
                    return text.Substring(0, charLocation);
                }
            }

            return String.Empty;
        }

        public static string GetFileNameFromRequestURL(string requestURL)
        {
            // Get string sau dấu / cuối trong url
            int positionNameInUrl = requestURL.LastIndexOf("/") + 1;
            // Get full file name (có đuôi file)
            string fileNameWithWav = requestURL.Substring(positionNameInUrl, requestURL.Length - positionNameInUrl);
            // Get file name bỏ đuôi file
            string fileName = GetUntilOrEmpty(fileNameWithWav, ".");
            return fileName;
        }

        /// <summary>
        /// Remove Vietnamese string
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public static string RemoveVietnameseString(string str)
        {
            var vietNameseSigns = new string[]
            {

            "aAeEoOuUiIdDyY",

            "áàạảãâấầậẩẫăắằặẳẵ",

            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",

            "éèẹẻẽêếềệểễ",

            "ÉÈẸẺẼÊẾỀỆỂỄ",

            "óòọỏõôốồộổỗơớờợởỡ",

            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",

            "úùụủũưứừựửữ",

            "ÚÙỤỦŨƯỨỪỰỬỮ",

            "íìịỉĩ",

            "ÍÌỊỈĨ",

            "đ",

            "Đ",

            "ýỳỵỷỹ",

            "ÝỲỴỶỸ"
        };


            for (int i = 1; i < vietNameseSigns.Length; i++)
            {
                for (int j = 0; j < vietNameseSigns[i].Length; j++)
                    str = str.Replace(vietNameseSigns[i][j], vietNameseSigns[0][i - 1]);
            }
            return str;

        }

        /// <summary>
        /// Generate skill code
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public static string GenerateSkillCode(string str)
        {
            var removeVietnamese = RemoveVietnameseString(str).Replace(" ", "_").ToLower();
            var randomNumber = GenerateUniqueNumber(0, 999999);
            var result = $"{removeVietnamese}_{randomNumber}";
            return result;
        }

        public static string UploadFileToLocalServer(IFormFile file, string pathFile)
        {
            try
            {
                if (!File.Exists(pathFile))
                {
                    using (Stream stream = new FileStream(pathFile, FileMode.Create))
                    {
                        file.CopyTo(stream);
                        stream.Flush();
                    }
                }

                return pathFile;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static string GenerateAvatar(string username, string pathSaveFile)
        {
            string avatarString = username.Substring(0, 1).ToUpper();
            var randomIndex = new Random().Next(0, Constants.COLOR_CODES.Count - 1);
            Color backgroundColor = ColorTranslator.FromHtml(Constants.COLOR_CODES[randomIndex]);
            Color textColor = ColorTranslator.FromHtml("#FFF");
            Font font = new Font(FontFamily.GenericSansSerif, 45);

            GenerateAvatarImage(avatarString, font, textColor, backgroundColor, pathSaveFile);

            return pathSaveFile;
        }

        public static async Task<bool> SendDataToWebhook<T>(Guid tenantId, T data, string eventKey)
        {
            try
            {
                // Push event data in webhook
                //ProducerWrapper<WebhookModel<T>> producer = new ProducerWrapper<WebhookModel<T>>();
                //WebhookModel<T> webhookModel = new WebhookModel<T>();
                //webhookModel.event_key = eventKey;
                //webhookModel.data = data;
                //await producer.CreateMess(Topic.SEND_WEBHOOK_EVENT, webhookModel,tenantId.ToString());
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return false;
            }
        }


        #region private func
        private static Image GenerateAvatarImage(string text, Font font, Color textColor, Color backColor, string pathSaveFile)
        {
            //First, create a dummy bitmap just to get a graphics object  
            Image img = new Bitmap(1, 1);
            Graphics drawing = Graphics.FromImage(img);
            drawing.PageUnit = GraphicsUnit.Pixel;

            //Measure the string to see how big the image needs to be  
            SizeF textSize = drawing.MeasureString(text, font);

            //Free up the dummy image and old graphics object  
            img.Dispose();
            drawing.Dispose();

            //Create a new image of the right size  
            img = new Bitmap(100, 100);
            drawing = Graphics.FromImage(img);

            //Paint the background  
            drawing.Clear(backColor);

            //Create a brush for the text  
            Brush textBrush = new SolidBrush(textColor);

            //Drawing.DrawString(text, font, textBrush, 0, 0);  
            drawing.DrawString(text, font, textBrush, new PointF((int)((100 - (textSize.Width)) / 2), (int)((100 - (textSize.Height)) / 2)), new StringFormat());
            drawing.Save();

            textBrush.Dispose();
            drawing.Dispose();

            img.Save(pathSaveFile);

            return img;
        }
        #endregion

    }
}
