using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Repository.Model;

namespace Repository.Repositories
{
    public class ModuleRepository : BaseRepositorySql<QTTS01_Module>, IModuleRepository
    {
        public async Task<bool> DeleteCustom(QTTS01_Module moduleEntity)
        {
            using (IDbContextTransaction transaction = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    // Data remove 
                    List<QTTS01_PermissionObject> permissionObjectEntities = await _db.QTTS01_PermissionObjects.Where(x => x.module_id == moduleEntity.id && x.tenant_id == moduleEntity.tenant_id).ToListAsync();
                    List<Guid> permissionObjectIds = permissionObjectEntities.Select(x => x.id).ToList();
                    List<QTTS01_Permission> permissionEntities = await _db.QTTS01_Permissions.Where(x => x.tenant_id == moduleEntity.tenant_id && permissionObjectIds.Contains(x.permissionobject_id)).ToListAsync();

                    _db.QTTS01_PermissionObjects.RemoveRange(permissionObjectEntities);
                    _db.QTTS01_Permissions.RemoveRange(permissionEntities);
                    _db.QTTS01_Modules.Remove(moduleEntity);

                    await _db.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return true;
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
