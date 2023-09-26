using System;
using System.Collections.Generic;

namespace Repository.Model
{
    public partial class QTTS01_Group
    {
        public Guid ID { get; set; }
        public string group_code { get; set; } = null!;
        public string group_name { get; set; } = null!;
        public bool is_active { get; set; }
        public DateTime create_time { get; set; }
        public string create_by { get; set; } = null!;
        public DateTime modify_time { get; set; }
        public string modify_by { get; set; } = null!;
    }
}
