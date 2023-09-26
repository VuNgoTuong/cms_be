using System;
using System.ComponentModel.DataAnnotations;

namespace UserManagement.Models.Common
{
    public class ItemModel
    {
        [Required]
        public string _id { get; set; }
    }
    public class ItemModel<T>
    {
        [Required]
        public T item { get; set; }
        public Guid tenant_id { get; set; }
    }
    public class CheckRequest
    {
        public bool is_all_level { get; set; }
        public Guid tenant_id { get; set; }
    }
}
