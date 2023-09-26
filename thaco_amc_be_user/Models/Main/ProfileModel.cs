using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.ComponentModel.DataAnnotations;

namespace UserManagement.Models.Main
{
    public class ProfileResponse
    {
        public Guid id { get; set; }
        public string profile_name { get; set; }
        public string description { get; set; }
        public bool is_active { get; set; }
        public DateTime create_time { get; set; }
        public string create_by { get; set; }
        public DateTime modify_time { get; set; }
        public string modify_by { get; set; }
        public Guid tenant_id { get; set; }
    }

    public class ProfileRequest : BaseModelSQL
    {
        [MaxLength(150)]
        public string profile_name { get; set; }
        [MaxLength(150)]
        public string description { get; set; }
        [Required]
        public bool is_active { get; set; }
    }

    public class UserInProfileSearchRequest
    {
        [MaxLength(50)]
        public string username { get; set; }
        [MaxLength(50)]
        public string phone { get; set; }
        [MaxLength(150)]
        public string fullname { get; set; }
        public Guid profile_id { get; set; }
    }

    public class UserInProfileCustomResponse
    {
        public string username { get; set; }
        public string fullname { get; set; }
        public string phone { get; set; }
        public Guid profile_id { get; set; }
        public Guid tenant_id { get; set; }
    }
}
