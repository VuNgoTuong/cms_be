using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Model
{
    public partial class QTTS01_Bank
    {
        public Guid id { get; set; }
        public string bank_code { get; set; } = null!;
        public string bank_name { get; set; } = null!;
        public string description { get; set; } = null!;
        public bool is_active { get; set; }
        public DateTime create_time { get; set; }
        public string create_by { get; set; } = null!;
        public DateTime modify_time { get; set; }
        public string modify_by { get; set; } = null!;
        public Guid tenant_id { get; set; }

    }
}
