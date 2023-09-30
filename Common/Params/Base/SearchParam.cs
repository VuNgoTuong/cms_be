using System;
using System.ComponentModel.DataAnnotations;

namespace Common.Params.Base
{
    public class SearchParam
    {
        public string name_field { get; set; }
        public Object value_search { get; set; }
    }
    public class Search
    {
        public int page { get; set; }
        public int limit { get; set; }
        [Required(AllowEmptyStrings = true)]
        public string value { get; set; } = "";
        public Guid tenant_id { get; set; }
    }
}
