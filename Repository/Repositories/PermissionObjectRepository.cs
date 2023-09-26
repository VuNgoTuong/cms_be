using Common.Params.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Repository.CustomModel;
using Repository.Model;
using Repository.Queries;
using EFCore.BulkExtensions;

namespace Repository.Repositories
{
    public class PermissionObjectRepository : BaseRepositorySql<QTTS01_PermissionObject>, IPermissionObjectRepository
    {
        private Query<PermissionObjectCustom> _query;
        public PermissionObjectRepository() : base()
        {
            _query = new Query<PermissionObjectCustom>(_db);
        }

        public async Task<ListResult<PermissionObjectCustom>> GetAllPermiss(PagingParam param)
        {
            ListResult<PermissionObjectCustom> listresult = new ListResult<PermissionObjectCustom>();
            List<PermissionObjectCustom> result = new List<PermissionObjectCustom>();
            var query = (from pm in _db.QTTS01_PermissionObjects
                         join m in _db.QTTS01_Modules on pm.module_id equals m.id
                         select new PermissionObjectCustom
                         {
                             id = pm.id,
                             object_name = pm.object_name,
                             module_id = pm.module_id,
                             description = pm.description,
                             is_active = pm.is_active,
                             create_time = pm.create_time,
                             create_by = pm.create_by,
                             modify_time = pm.modify_time,
                             modify_by = pm.modify_by,
                             tenant_id = pm.tenant_id,
                             module_name = m.module_name
                         });

            var sqlstring = query.ToParametrizedSql().Item1;
            param.search_list.ForEach(x =>
            {
                if (!x.name_field.Equals("module_name"))
                {
                    x.name_field = $"[m].{x.name_field}";
                }
            });
            listresult = await _query.SearchJoin(param, sqlstring);
            return listresult;
        }

        public async Task CreateCustom(QTTS01_PermissionObject permissionObjectEntity, List<QTTS01_Permission> permissionEntities)
        {
            using (IDbContextTransaction transaction = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    await _db.QTTS01_PermissionObjects.AddAsync(permissionObjectEntity);
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

        public async Task UpdateCustom(QTTS01_PermissionObject permissionObjectEntity, List<QTTS01_Permission> permissionEntities)
        {
            using (IDbContextTransaction transaction = await _db.Database.BeginTransactionAsync())
            {
                try
                {

                    _db.QTTS01_Permissions.UpdateRange(permissionEntities);
                    _db.QTTS01_PermissionObjects.Update(permissionObjectEntity);

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

        public async Task DeleteCustom(QTTS01_PermissionObject permissionObjectEntity, List<QTTS01_Permission> permissionEntities)
        {
            using (IDbContextTransaction transaction = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    _db.QTTS01_Permissions.RemoveRange(permissionEntities);
                    _db.QTTS01_PermissionObjects.Remove(permissionObjectEntity);

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

        public async Task<bool> CheckExistsModule(Guid tenantId, Guid moduleId)
        {
            return await _db.QTTS01_Modules.AnyAsync(x => x.tenant_id == tenantId && x.id == moduleId);
        }

        public async Task<List<Guid>> GetListProfileByTenant(Guid tenantId)
        {
            return await _db.QTTS01_Profiles.Where(x => x.tenant_id == tenantId).Select(x => x.id).ToListAsync();
        }

        public async Task<List<QTTS01_Permission>> GetListPermissionByPermissionObject(Guid permissionObjectId, Guid tenantId)
        {
            return await _db.QTTS01_Permissions.Where(x => x.permissionobject_id == permissionObjectId && x.tenant_id == tenantId).AsNoTracking().ToListAsync();
        }
    }
}
