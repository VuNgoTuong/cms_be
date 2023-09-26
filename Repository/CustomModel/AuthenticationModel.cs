using Common;
using Common.Commons;
using System;
using System.ComponentModel.DataAnnotations;

namespace Repository.CustomModel
{
    public class RegisterRequest : BaseRegister
    {
        [Required]
        [MaxLength(250)]
        public string tenant_name { get; set; }
        [MaxLength(50)]
        public string province_id { get; set; } = "";
        [Required]
        [MaxLength(250)]
        public string address { get; set; }
        [Phone]
        [Required]
        [MaxLength(50)]
        public string phone { get; set; }
        [Required]
        [MaxLength(50)]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Input valid email address!")]
        public string email { get; set; }
    }
    public class BaseRegister
    {
        public Guid id { get; set; }
        public string register_key { get; set; }
        [Required]
        [MaxLength(50)]
        [RegularExpression(@"^.*(?=.{8})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=]).*$", ErrorMessage = "Password must be  least  8 characters and contain one uppercase letter, one lowercase letter, one digit and one special character.")]
        public string password { get; set; }       
        public DateTime? expire_time { get; set; }
        public bool is_active { get; set; }
        public DateTime create_time { get; set; }
        public string create_by { get; set; }
        public DateTime modify_time { get; set; }
        public string modify_by { get; set; }
        public void AddInfo()
        {
            DateTime currenttime = DateTime.Now;
            id = Guid.NewGuid();
            register_key = CommonFuncMain.GenerateCoupon();
            password = HashString.StringToHash(password, Constants.HASH_SHA512);
            is_active = true;
            create_time = currenttime;
            create_by = "Auto";
            modify_time = currenttime;
            modify_by = "Auto";
        }

    }
    public class ForgotPasswordRequest
    {
        [Required]
        [MaxLength(50)]
        public string email { get; set; }
    }
    public class ResetPasswordRequest
    {
        [Required]
        public string public_key { get; set; }
        [Required]
        [MaxLength(50)]
        public string password { get; set; }
        [Required]
        [MaxLength(50)]
        [Compare("password", ErrorMessage = "Error compare password")]
        public string comparepassword { get; set; }

        public void UpdateInfo()
        {
            password = HashString.StringToHash(password, Constants.HASH_SHA512);
            comparepassword = HashString.StringToHash(comparepassword, Constants.HASH_SHA512);
        }
    }
    public class UpdatePasswordRequest
    {
        [Required]
        public string oldpassword { get; set; }
        [Required]
        [RegularExpression(@"^.*(?=.{8})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=]).*$", ErrorMessage = "Password must be  least  8 characters and contain one uppercase letter, one lowercase letter, one digit and one special character.")]
        public string password { get; set; }
        [Required]
        [Compare("password", ErrorMessage = "Error compare password")]
        public string comparepassword { get; set; }

        public void UpdateInfo()
        {
            oldpassword = HashString.StringToHash(oldpassword, Constants.HASH_SHA512);
            password = HashString.StringToHash(password, Constants.HASH_SHA512);
            comparepassword = HashString.StringToHash(comparepassword, Constants.HASH_SHA512);
        }
    }
    public class ReceivePasswordModel
    {
        public string email { get; set; }
    }
    public class LoginRequest
    {
        [Required]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
        public bool is_remember_me { get; set; }
    }
    public class LoginResponse
    {
        public string username { get; set; }
        public string fullname { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        //public bool is_administrator { get; set; }
        public bool is_rootuser { get; set; }
        public bool is_active { get; set; }
        public Guid tenant_id { get; set; }
        public string token { get; set; }
    }

    public class GenerateTokenRequest
    {
        public string user_name { get; set; }
        public Guid tenant_id { get; set; }
        //public bool is_administrator { get; set; }
        public bool is_rootuser { get; set; }
        public int tokenExpiresTime { get; set; }
    }
}
