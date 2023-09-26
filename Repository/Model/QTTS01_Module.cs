using System;
using System.Collections.Generic;

namespace Repository.Model
{
    public partial class QTTS01_Module
    {
        public Guid id { get; set; }
        public string module_name { get; set; } = null!;
        public string? description { get; set; }
        public DateTime create_time { get; set; }
        public string create_by { get; set; } = null!;
        public DateTime modify_time { get; set; }
        public string modify_by { get; set; } = null!;
        public Guid tenant_id { get; set; }
        public bool is_active { get; set; }
        public string? display_name { get; set; }
        public int position { get; set; }
    }
}
