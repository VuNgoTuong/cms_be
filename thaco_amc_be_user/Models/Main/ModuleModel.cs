using System;
using System.ComponentModel.DataAnnotations;

namespace UserManagement.Models.Main
{
    public class ModuleResponse
    {
        public Guid id { get; set; }
        public string module_name { get; set; }
        public string display_name { get; set; }
        public int position { get; set; }
        public string description { get; set; }
        public DateTime create_time { get; set; }
        public string create_by { get; set; }
        public DateTime modify_time { get; set; }
        public string modify_by { get; set; }
        public bool is_active { get; set; }
        public Guid tenant_id { get; set; }
    }
    public class ModuleRequest : BaseModelSQL
    {
        [Required]
        [MaxLength(150)]
        public string module_name { get; set; }
        [MaxLength(150)]
        public string description { get; set; }
        [Required]
        [MaxLength(150)]
        public string display_name { get; set; }
        public int position { get; set; }
    }
}
