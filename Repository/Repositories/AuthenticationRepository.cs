using Microsoft.EntityFrameworkCore;
using Repository.Model;

namespace Repository.Repositories
{
    public class AuthenticationRepository : BaseRepositorySql<QTTS01_User>, IAuthenticationRepository
    {
        public async Task<QTTS01_User> CheckExiststUsernameAndPassword(string username, string password)
       {
            return await _db.QTTS01_Users.Where(x => x.username == username && x.password == password).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<QTTS01_Tenant> CheckTenantIsActive(Guid tenantId)
        {
            return await _db.QTTS01_Tenants.Where(x => x.id == tenantId && x.is_active).FirstOrDefaultAsync();
        }
        public async Task<QTTS01_User> CheckExistsUser(string username, Guid tenantId)
        {
            return await _db.QTTS01_Users.Where(x => x.username == username && x.tenant_id == tenantId).AsNoTracking().FirstOrDefaultAsync();
        }

    }
}
