using Common.Params.Base;
using Repository.CustomModel;
using Repository.Model;

namespace Repository.Repositories
{
    public interface IGroupRepository : IBaseRepositorySql<QTTS01_Group>
    {
        Task<bool> DeleteCustom(QTTS01_Group groupEntity);
        Task<ListResult<QTTS01_Group>> GetAllCustom(PagingParamCustom param);
    }
}