using Repository.Model;

namespace Repository.Repositories
{
    public interface IBankRepository : IBaseRepositorySql<QTTS01_Bank>
    {
        Task<bool> DeleteCustom(QTTS01_Bank bankEntity);
    }
}