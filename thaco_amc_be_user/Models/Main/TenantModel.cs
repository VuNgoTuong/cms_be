using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.ComponentModel.DataAnnotations;

namespace UserManagement.Models.Main
{
    public class TenantResponse
    {
        public Guid id { get; set; }
        public string tenant_name { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public bool is_active { get; set; }
        public DateTime create_time { get; set; }
        public string create_by { get; set; }
        public DateTime modify_time { get; set; }
        public string modify_by { get; set; }
        public Guid group_id { get; set; }

    }

    public class TenantRequest : BaseModelSQL
    {
        [Required]
        [MaxLength(150)]
        public string tenant_name { get; set; }
        [MaxLength(150)]
        public string address { get; set; }
        [MaxLength(50)]
        public string phone { get; set; }
        [MaxLength(150)]
        public string email { get; set; }
        [Required]
        public bool is_active { get; set; }
    }

}
