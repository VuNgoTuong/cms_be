using System;

namespace UserManagement.Models.Common
{
    public class UpdateIsActiveModel
    {
        public bool is_active { get; set; }
        public Guid tenant_id { get; set; }
    }
}
