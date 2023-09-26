using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.ComponentModel.DataAnnotations;

namespace UserManagement.Models.Main
{
    public class BankResponse
    {
        public Guid id { get; set; }
        public string bank_code { get; set; }
        public string bank_name { get; set; }
        public string description { get; set; }
        public bool is_active { get; set; }
        public DateTime create_time { get; set; }
        public string create_by { get; set; }
        public DateTime modify_time { get; set; }
        public string modify_by { get; set; }
        public Guid tenant_id { get; set; }
    }

    public class BankRequest : BaseModelSQL
    {
        [MaxLength(150)]
        public string bank_name { get; set; }
        [MaxLength(150)]
        public string description { get; set; }
        [Required]
        public bool is_active { get; set; }
    }

}
