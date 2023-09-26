using Common.Params.Base;
using System;
using System.Collections.Generic;

namespace Repository.CustomModel
{
    public class PagingRequest<T>
    {
        public T request { get; set; }
        public int page { get; set; }
        public int limit { get; set; }
        public Guid tenant_id { get; set; }
    }

    public class PagingRequestWithSearch<T>
    {
        public T request { get; set; }
        public int page { get; set; }
        public int limit { get; set; }
        public Guid tenant_id { get; set; }
        public List<SearchParam> search_list { get; set; }
    }
    public class PagingRequestCustom
    {
        public int page { get; set; }
        public int limit { get; set; }
        public Guid tenant_id { get; set; }
        public List<SearchParam> search_list { get; set; }
    }
}
