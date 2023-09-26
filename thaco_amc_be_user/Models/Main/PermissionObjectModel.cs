using System;
using System.ComponentModel.DataAnnotations;

namespace UserManagement.Models.Main
{
    public class PermissionObjectResponse
    {
        public Guid id { get; set; }
        public string object_name { get; set; }
        public Guid module_id { get; set; }
        public string description { get; set; }
        public bool is_active { get; set; }
        public DateTime create_time { get; set; }
        public string create_by { get; set; }
        public DateTime modify_time { get; set; }
        public string modify_by { get; set; }
        public Guid tenant_id { get; set; }
        public string module_name { get; set; }
    }
    public class PermissionObjectRequest : BaseModelSQL
    {
        [Required]
        [MaxLength(150)]
        public string object_name { get; set; }
        [Required]
        public Guid module_id { get; set; }
        [MaxLength(250)]
        public string description { get; set; }
        public bool is_active { get; set; }
    }

    public class PermissionObjectSearchRequest 
    {
        [MaxLength(150)]
        public string object_name { get; set; }
        [MaxLength(150)]
        public string module_name { get; set; }
        [MaxLength(250)]
        public string description { get; set; }
        [MaxLength(50)]
        public string create_by { get; set; }
        [MaxLength(250)]
        public string create_time { get; set; }
    }
}
