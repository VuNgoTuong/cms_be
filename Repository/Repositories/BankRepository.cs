﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Repository.Model;

namespace Repository.Repositories
{
    public class BankRepository : BaseRepositorySql<QTTS01_Bank>, IBankRepository
    {
        public async Task<bool> DeleteCustom(QTTS01_Bank bankEntity)
        {
            using (IDbContextTransaction transaction = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    List<QTTS01_PermissionObject> permissionObjectEntities = await _db.QTTS01_PermissionObjects.Where(x => x.module_id == bankEntity.id && x.tenant_id == bankEntity.tenant_id).ToListAsync();
                    List<Guid> permissionObjectIds = permissionObjectEntities.Select(x => x.id).ToList();
                    List<QTTS01_Permission> permissionEntities = await _db.QTTS01_Permissions.Where(x => x.tenant_id == bankEntity.tenant_id && permissionObjectIds.Contains(x.permissionobject_id)).ToListAsync();

                    _db.QTTS01_PermissionObjects.RemoveRange(permissionObjectEntities);
                    _db.QTTS01_Permissions.RemoveRange(permissionEntities);
                    _db.QTTS01_Banks.Remove(bankEntity);

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