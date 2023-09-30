using Common.Params.Base;
using Repository.CustomModel;
using Repository.Model;

namespace Repository.Repositories
{
    public interface ITenantRepository : IBaseRepositorySql<QTTS01_Tenant>
    {
        Task<bool> DeleteCustom(QTTS01_Tenant tenantEntity);
        Task<ListResult<QTTS01_Tenant>> GetAllCustom(PagingParamCustom param);
    }
}