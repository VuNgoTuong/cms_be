using System;
using System.ComponentModel.DataAnnotations;

namespace Repository.CustomModel
{
    public class UserModel
    {
        public string username { get; set; }
        public string fullname { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string description { get; set; }
        //public bool is_administrator { get; set; }
        public bool is_rootuser { get; set; }
        //public bool? is_supervisor { get; set; }
        public bool is_active { get; set; }
        public DateTime create_time { get; set; }
        public string create_by { get; set; }
        public DateTime modify_time { get; set; }
        public string modify_by { get; set; }
        public Guid tenant_id { get; set; }
        //public string report_to { get; set; }
        public Guid profile_id { get; set; }
    }
    public class UserCustomResponse
    {
        public string username { get; set; }
        public string fullname { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string description { get; set; }

        //public bool? is_supervisor { get; set; }
        //public bool? is_agent { get; set; }
        public string avatar { get; set; }
        public bool is_active { get; set; }
        public DateTime create_time { get; set; }
        public string create_by { get; set; }
        public DateTime modify_time { get; set; }
        public string modify_by { get; set; }
        public Guid tenant_id { get; set; }
    }

    public class UserRoleResponse
    {
        public string username { get; set; }
        public string fullname { get; set; }
        public Guid role_id { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string report_to { get; set; }
        public string avatar { get; set; }
        public bool is_administrator { get; set; }
        public bool is_rootuser { get; set; }
        public bool? is_supervisor { get; set; }
        public bool? is_agent { get; set; }

    }
    public class UpdateAvatarRequest
    {
        public string username { get; set; }
        public string avatar { get; set; }
    }

    public class UpdateInformation
    {
        [MaxLength(50)]
        public string username { get; set; }
        [MaxLength(250)]
        public string fullname { get; set; }
        [MaxLength(50)]
        public string phone { get; set; }
        public string avatar { get; set; }
        [MaxLength(150)]
        public string description { get; set; }
    }

    public class UsernameRequest
    {
        public string username { get; set; }
        public Guid tenant_id { get; set; }
    }

    public class CheckUserStateByExtensionRequest
    {
        [MaxLength(50)]
        public string extension_number { get; set; }
        public Guid tenant_id { get; set; }
    }

    public class UserIsActive
    {
        public string username { get; set; }
        public string fullname { get; set; }
        public string email { get; set; }
        public Guid tenant_id { get; set; }
    } 
}
