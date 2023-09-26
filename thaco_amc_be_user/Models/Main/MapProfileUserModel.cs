using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UserManagement.Models.Main
{
    public class MapProfileUserRequest : BaseModelSQL
    {
        public string username { get; set; }
        public Guid profile_id { get; set; }
        public string description { get; set; } = "";
        public bool is_active { get; set; } = true;
    }

    public class AddUserToProfile
    {
        [Required]
        public Guid profile_id { get; set; }
        public List<string> usernames { get; set; }
    }
}
