using Repository.Model;

namespace Repository.Repositories
{
    public interface IAuthenticationRepository
    {
        Task<QTTS01_User> CheckExiststUsernameAndPassword(string username, string password);
        Task<QTTS01_User> CheckExistsUser(string username, Guid tenantId);
        Task<QTTS01_Tenant> CheckTenantIsActive(Guid tenantId);
    }
}