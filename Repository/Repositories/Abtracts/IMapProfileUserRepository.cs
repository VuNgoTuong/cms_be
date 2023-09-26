using Repository.Model;

namespace Repository.Repositories
{
    public interface IMapProfileUserRepository: IBaseRepositorySql<QTTS01_MapProfileUser>
    {
        Task<bool> CheckExistsProfile(Guid tenantId, Guid profileId);
        Task<bool> CheckExistsUserInTenant(Guid tenantId, List<string> usernameList);
        Task CreateList(List<QTTS01_MapProfileUser> mapProfileUserEntities);
    }
}