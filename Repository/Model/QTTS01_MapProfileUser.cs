using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Model
{
    public partial class QTTS01_MapProfileUser
    {
        public Guid id { get; set; }
        public string username { get; set; } = null!;
        public Guid profile_id { get; set; }
        public string? description { get; set; }
        public bool is_active { get; set; }
        public DateTime create_time { get; set; }
        public string create_by { get; set; } = null!;
        public DateTime modify_time { get; set; }
        public string modify_by { get; set; } = null!;
        public Guid tenant_id { get; set; }

        [ForeignKey("profile_id")]
        public virtual QTTS01_Profile QTTS01_Profile { get; set; }
        [ForeignKey("username")]
        public virtual QTTS01_User QTTS01_User { get; set; }
    }
}
