using System;

namespace Repository.CustomModel
{
    public class ModuleModel
    {
        public Guid id { get; set; }
        public string module_name { get; set; }
        public string display_name { get; set; }
        public bool is_active { get; set; }
        public int position { get; set; }
        public string description { get; set; }
    }

    public class ModuleCustomResponse
    {
        public Guid id { get; set; }
        public string module_name { get; set; }
        public string display_name { get; set; }
        public int position { get; set; }
        public string description { get; set; }
        public bool is_active { get; set; }
        public bool is_show { get; set; }
        public DateTime create_time { get; set; }
        public string create_by { get; set; }
        public DateTime modify_time { get; set; }
        public string modify_by { get; set; }
        public Guid tenant_id { get; set; }
    }
}
