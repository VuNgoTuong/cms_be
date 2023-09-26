using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Model
{
    public partial class QTTS01_Profile
    {
        public QTTS01_Profile()
        {
            QTTS01_MapProfileUser = new HashSet<QTTS01_MapProfileUser>();
            QTTS01_Permission = new HashSet<QTTS01_Permission>();
        }
        public Guid id { get; set; }
        public string profile_name { get; set; } = null!;
        public string? description { get; set; }
        public bool is_active { get; set; }
        public DateTime create_time { get; set; }
        public string create_by { get; set; } = null!;
        public DateTime modify_time { get; set; }
        public string modify_by { get; set; } = null!;
        public Guid tenant_id { get; set; }
        [ForeignKey("id")]
        public virtual ICollection<QTTS01_MapProfileUser> QTTS01_MapProfileUser { get; set; }
        [ForeignKey("id")]
        public virtual ICollection<QTTS01_Permission> QTTS01_Permission { get; set; }
    }
}
