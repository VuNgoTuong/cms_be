using Common.Params.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UserManagement.Models.Main
{
    public class GroupResponse
    {
        public Guid id { get; set; }
        public string group_code { get; set; }
        public string group_name { get; set; }
        public bool is_active { get; set; }
        public DateTime create_time { get; set; }
        public string create_by { get; set; }
        public DateTime modify_time { get; set; }
        public string modify_by { get; set; }
    }

    public class GroupRequest : BaseModelSQL
    {
        [MaxLength(150)]
        public string group_code { get; set; }
        [MaxLength(150)]
        public string group_name { get; set; }
        [Required]
        public bool is_active { get; set; }
    }

}
