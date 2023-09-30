using System.Collections.Generic;

namespace Common.Params.Base
{
    public class PagingParamCustom : BaseParamCustom
    {
        public int page { get; set; }
        public int limit { get; set; }
       
    }

    public class PagingParamCustomGetAll : BaseParamCustomGetAll
    {
        public int page { get; set; }
        public int limit { get; set; }

    }

}
