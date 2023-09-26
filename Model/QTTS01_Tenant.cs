using System;
using System.Collections.Generic;

namespace Repository.Model

{
    public partial class QTTS01_Tenant
    {
        public Guid id { get; set; }
        public string tenant_name { get; set; } = null!;
        public string address { get; set; } = null!;
        public string phone { get; set; } = null!;
        public string email { get; set; } = null!;
        public Guid group_id { get; set; }
        public bool is_active { get; set; }
        public DateTime create_time { get; set; }
        public string create_by { get; set; } = null!;
        public DateTime modify_time { get; set; }
        public string modify_by { get; set; } = null!;
    }
}
