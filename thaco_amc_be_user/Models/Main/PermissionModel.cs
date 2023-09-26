using System;
using System.ComponentModel.DataAnnotations;

namespace UserManagement.Models.Main
{
    public class PermissionRequest : BaseModelSQL
    {
        [Required]
        public Guid profile_id { get; set; }
        [Required]
        public Guid permissionobject_id { get; set; }
        public bool is_show { get; set; }
        public bool is_allow_access { get; set; }
        public bool is_allow_create { get; set; }
        public bool is_allow_edit { get; set; }
        public bool is_allow_delete { get; set; }
        [Required]
        public string object_name { get; set; }
        public string description { get; set; }
        public bool is_active { get; set; }
    }
    public class PermissionResponse
    {
        public Guid id { get; set; }
        public Guid profile_id { get; set; }
        public Guid permissionobject_id { get; set; }
        public bool is_show { get; set; }
        public bool is_allow_access { get; set; }
        public bool is_allow_create { get; set; }
        public bool is_allow_edit { get; set; }
        public bool is_allow_delete { get; set; }
        public string object_name { get; set; }
        public string description { get; set; }
        public bool is_active { get; set; }
        public DateTime create_time { get; set; }
        public string create_by { get; set; }
        public DateTime modify_time { get; set; }
        public string modify_by { get; set; }
        public Guid tenant_id { get; set; }
    }
    public class PermissionResOther
    {
        public Guid id { get; set; }
        public bool is_show { get; set; }
        public bool is_allow_access { get; set; }
        public bool is_allow_create { get; set; }
        public bool is_allow_edit { get; set; }
        public bool is_allow_delete { get; set; }
        public string object_name { get; set; }
        public bool is_active { get; set; }
    }

    public class UpdatePermissonRequest : BaseModelSQL
    {
        [Required]
        public Guid profile_id { get; set; }
        public bool is_show { get; set; }
        public bool is_allow_access { get; set; }
        public bool is_allow_create { get; set; }
        public bool is_allow_edit { get; set; }
        public bool is_allow_delete { get; set; }
        [Required]
        public string object_name { get; set; }
    }

    public class PermissonSearchRequest
    {
        [MaxLength(150)]
        public string object_name { get; set; }
        public Guid profile_id { get; set; }
    }
}
