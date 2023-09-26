using Repository.CustomModel;
using Repository.Model;

namespace Repository.Repositories
{
    public interface IPermissionRepository : IBaseRepositorySql<QTTS01_Permission>
    {
        Task<bool> CheckExistsPermissionObject(Guid tenantId, Guid permissionObjectId);
        Task<bool> CheckExistsProfile(Guid tenantId, Guid profileId);
        Task<List<PermissionResShort>> GetListPermissionByUser(string username, bool isRoot, bool isAdmin);
        Task<PermissionResShort> GetPermissionAObjectByUser(PermissionAObjectRequest request);
        Task<bool> GetStatusPermissionByTypeAndName(GetPermissionByTypeAndName request);
    }
}