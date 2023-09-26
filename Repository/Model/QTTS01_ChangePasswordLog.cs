using System;
using System.Collections.Generic;

namespace Repository.Model
{
    public partial class QTTS01_ChangePasswordLog
    {
        public Guid id { get; set; }
        public string username { get; set; } = null!;
        public string old_password { get; set; } = null!;
        public string new_password { get; set; } = null!;
        public DateTime create_time { get; set; }
        public string create_by { get; set; } = null!;
        public DateTime modify_time { get; set; }
        public string modify_by { get; set; } = null!;
        public Guid tenant_id { get; set; }
    }
}
