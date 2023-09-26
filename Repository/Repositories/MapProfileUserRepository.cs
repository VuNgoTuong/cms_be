using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Repository.Model;

namespace Repository.Repositories
{
    public class MapProfileUserRepository : BaseRepositorySql<QTTS01_MapProfileUser>, IMapProfileUserRepository
    {
        public async Task<bool> CheckExistsUserInTenant(Guid tenantId, List<string> usernameList)
        {
            List<QTTS01_User> agentByTenant = await _db.QTTS01_Users.Where(x => x.tenant_id == tenantId).AsNoTracking().ToListAsync();
            foreach (var item in usernameList)
            {
                var checkAgentByTenant = agentByTenant.FirstOrDefault(x => x.username == item);
                if (checkAgentByTenant == null)
                {
                    return false;
                }
            }
            return true;
        }

        public async Task<bool> CheckExistsProfile(Guid tenantId, Guid profileId)
        {
            return await _db.QTTS01_Profiles.AnyAsync(x => x.tenant_id == tenantId && x.id == profileId);
        }

        public async Task CreateList(List<QTTS01_MapProfileUser> mapProfileUserEntities)
        {
            using (IDbContextTransaction transaction = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    foreach (var item in mapProfileUserEntities)
                    {
                        bool checkExists = await _db.QTTS01_MapProfileUsers.AnyAsync(x => x.profile_id == item.profile_id && x.username == item.username);
                        if (!checkExists)
                        {
                            await _db.QTTS01_MapProfileUsers.AddAsync(item);
                        }
                    }

                    await _db.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}