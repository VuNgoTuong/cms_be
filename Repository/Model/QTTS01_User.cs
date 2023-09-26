using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Model
{
    public partial class QTTS01_User
    {
        public QTTS01_User()
        {
            QTTS01_MapProfileUser = new HashSet<QTTS01_MapProfileUser>();
        }
        public string username { get; set; } = null!;
        public string? fullname { get; set; }
        public string? phone { get; set; }
        public string password { get; set; } = null!;
        public string email { get; set; } = null!;
        public string? avatar { get; set; }
        public string? description { get; set; }
        public bool is_active { get; set; }
        public bool is_rootuser { get; set; }
        public DateTime create_time { get; set; }
        public string create_by { get; set; } = null!;
        public DateTime modify_time { get; set; }
        public string modify_by { get; set; } = null!;
        public Guid tenant_id { get; set; }
        public virtual ICollection<QTTS01_MapProfileUser> QTTS01_MapProfileUser { get; set; }

        [ForeignKey("tenant_id")]
        public virtual QTTS01_Tenant QTTS01_Tenants { get; set; }
    }
}
