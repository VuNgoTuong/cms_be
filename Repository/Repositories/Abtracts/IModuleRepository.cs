using Repository.Model;

namespace Repository.Repositories
{
    public interface IModuleRepository : IBaseRepositorySql<QTTS01_Module>
    {
        Task<bool> DeleteCustom(QTTS01_Module moduleEntity);
    }
}