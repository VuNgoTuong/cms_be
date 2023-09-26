using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Model
{
    public partial class QTTS01_Permission
    {
        public Guid id { get; set; }
        public Guid profile_id { get; set; }
        public Guid permissionobject_id { get; set; }
        public bool is_allow_access { get; set; }
        public bool is_allow_create { get; set; }
        public bool is_allow_edit { get; set; }
        public bool is_allow_delete { get; set; }
        public bool is_show { get; set; }
        public string object_name { get; set; } = null!;
        public string? description { get; set; }
        public bool is_active { get; set; }
        public DateTime create_time { get; set; }
        public string create_by { get; set; } = null!;
        public DateTime modify_time { get; set; }
        public string modify_by { get; set; } = null!;
        public Guid tenant_id { get; set; }

        [ForeignKey("permissionobject_id")]
        public virtual QTTS01_PermissionObject QTTS01_PermissionObject { get; set; }
        [ForeignKey("profile_id")]
        public virtual QTTS01_Profile QTTS01_Profile { get; set; }
    }
}
