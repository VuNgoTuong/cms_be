using System;

namespace Repository.CustomModel
{
    public class PermissionObjectCustom
    {
        public Guid id { get; set; }
        public string object_name { get; set; }
        public Guid module_id { get; set; }
        public string module_name { get; set; }
        public string description { get; set; }
        public bool is_active { get; set; }
        public DateTime create_time { get; set; }
        public string create_by { get; set; }
        public DateTime modify_time { get; set; }
        public string modify_by { get; set; }
        public Guid tenant_id { get; set; }
    }
    public class PermissionObjectModel
    {
        public Guid id { get; set; }
        public string object_name { get; set; }
        public Guid module_id { get; set; }
        public string description { get; set; }
    }
}
