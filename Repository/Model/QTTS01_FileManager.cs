using System;
using System.Collections.Generic;

namespace Repository.Model
{
    public partial class QTTS01_FileManager
    {
        public Guid id { get; set; }
        public string file_name { get; set; } = null!;
        public string size { get; set; } = null!;
        public string url_file { get; set; } = null!;
        public string object_id { get; set; } 
        public string object_file { get; set; } = null!;
        public DateTime create_time { get; set; }
        public string create_by { get; set; } = null!;
        public DateTime modify_time { get; set; }
        public string modify_by { get; set; } = null!;
        public Guid tenant_id { get; set; }
    }
}
