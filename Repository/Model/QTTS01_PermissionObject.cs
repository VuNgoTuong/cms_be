using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Model
{
    public partial class QTTS01_PermissionObject
    {
        public QTTS01_PermissionObject()
        {
            QTTS01_Permission = new HashSet<QTTS01_Permission>();
        }
        public Guid id { get; set; }
        public string object_name { get; set; } = null!;
        public Guid module_id { get; set; }
        public string? description { get; set; }
        public bool is_active { get; set; }
        public DateTime create_time { get; set; }
        public string create_by { get; set; } = null!;
        public DateTime modify_time { get; set; }
        public string modify_by { get; set; } = null!;
        public Guid tenant_id { get; set; }

        [ForeignKey("module_id")]
        public virtual QTTS01_Module QTTS01_Module { get; set; }
        [ForeignKey("id")]
        public virtual ICollection<QTTS01_Permission> QTTS01_Permission { get; set; }
    }
}
