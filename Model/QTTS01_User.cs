using System;
using System.Collections.Generic;

namespace Repository.Model

{
    public partial class QTTS01_User
    {
        public string username { get; set; } = null!;
        public string? fullname { get; set; }
        public string? phone { get; set; }
        public string password { get; set; } = null!;
        public string email { get; set; } = null!;
        public string? avatar { get; set; }
        public string? description { get; set; }
        public bool is_active { get; set; }
        public DateTime create_time { get; set; }
        public string create_by { get; set; } = null!;
        public DateTime modify_time { get; set; }
        public string modify_by { get; set; } = null!;
        public Guid tenant_id { get; set; }
    }
}
