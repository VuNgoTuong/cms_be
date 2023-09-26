using Common.Params.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Repository.CustomModel;
using Repository.Model;

namespace Repository.Repositories
{
    public class ProfileRepository : BaseRepositorySql<QTTS01_Profile>, IProfileRepository
    {
        public virtual async Task<ListResult<UserModel>> GetUsersByProfile(PagingParam param)
        {
            ListResult<UserModel> response = new ListResult<UserModel>();
            Guid profile_id = Guid.Empty;
            foreach (var item in param.search_list)
            {
                if (item.name_field.Equals("profile_id"))
                    profile_id = Guid.Parse(item.value_search.ToString());
            }
            var query = (from mp in _db.QTTS01_MapProfileUsers
                         join u in _db.QTTS01_Users on mp.username equals u.username
                         select new UserModel
                         {
                             username = u.username,
                             fullname = u.fullname,
                             email = u.email,
                             phone = u.phone,
                             description = u.description,
                             is_rootuser = u.is_rootuser,
                             is_active = u.is_active,
                             create_time = u.create_time,
                             create_by = u.create_by,
                             modify_time = u.modify_time,
                             modify_by = u.modify_by,
                             tenant_id = u.tenant_id,
                             profile_id = mp.profile_id
                         }).Where(x => x.tenant_id.Equals(param.tenant_id) && x.profile_id.Equals(profile_id));
            response.items = await query.ToListAsync();
            response.total = await query.CountAsync();
            return response;
        }

        public async Task CreateCustom(QTTS01_Profile entity, List<QTTS01_Permission> permissionEntities)
        {
            using (IDbContextTransaction transaction = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    await _db.QTTS01_Profiles.AddAsync(entity);
                    await _db.QTTS01_Permissions.AddRangeAsync(permissionEntities);

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

        public async Task DeleteUserInProfile(QTTS01_MapProfileUser dataRemove)
        {
            try
            {
                _db.QTTS01_MapProfileUsers.Remove(dataRemove);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteProfile(QTTS01_Profile profileEntity)
        {
            using (IDbContextTransaction transaction = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    List<QTTS01_Permission> listPermissionByProfile = await _db.QTTS01_Permissions.Where(x => x.profile_id == profileEntity.id && x.tenant_id == profileEntity.tenant_id).ToListAsync();
                    List<QTTS01_MapProfileUser> listUserMapProfile = await _db.QTTS01_MapProfileUsers.Where(x => x.profile_id == profileEntity.id && x.tenant_id == profileEntity.tenant_id).ToListAsync();

                    _db.QTTS01_Permissions.RemoveRange(listPermissionByProfile);
                    _db.QTTS01_MapProfileUsers.RemoveRange(listUserMapProfile);
                    _db.QTTS01_Profiles.Remove(profileEntity);

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

        public async Task UpdateListPermissionInProfile(List<QTTS01_Permission> permisionEntities)
        {
            using (IDbContextTransaction transaction = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    _db.QTTS01_Permissions.UpdateRange(permisionEntities);
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

        public async Task<List<QTTS01_Permission>> GetListPermissionInProfile(List<Guid> permissionIdList, Guid profileId, Guid tenantId)
        {
            return await _db.QTTS01_Permissions.Where(x => x.tenant_id == tenantId && x.profile_id == profileId && permissionIdList.Contains(x.id)).AsNoTracking().ToListAsync();
        }

        public async Task<bool> CheckUserRemoveIsAdmin(Guid tenantId, string username)
        {
            return await _db.QTTS01_Users.AnyAsync(x => x.tenant_id == tenantId && x.username == username && !x.is_rootuser);
        }
        public async Task<QTTS01_MapProfileUser> CheckExistsDataRemove(Guid tenantId, string username, Guid profileId)
        {
            return await _db.QTTS01_MapProfileUsers.Where(x => x.tenant_id == tenantId && x.username == username && x.profile_id == profileId).FirstOrDefaultAsync();
        }

        public async Task<List<QTTS01_PermissionObject>> GetPermissionObjectListByTenant(Guid tenantId)
        {
            return await _db.QTTS01_PermissionObjects.Where(x => x.is_active && x.tenant_id == tenantId).AsNoTracking().ToListAsync();
        }
    }
}
