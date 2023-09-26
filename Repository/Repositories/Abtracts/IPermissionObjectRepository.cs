using Common.Params.Base;
using Repository.CustomModel;
using Repository.Model;

namespace Repository.Repositories
{
    public interface IPermissionObjectRepository: IBaseRepositorySql<QTTS01_PermissionObject>
    {
        Task<bool> CheckExistsModule(Guid tenantId, Guid moduleId);
        Task CreateCustom(QTTS01_PermissionObject permissionObjectEntity, List<QTTS01_Permission> permissionEntities);
        Task DeleteCustom(QTTS01_PermissionObject permissionObjectEntity, List<QTTS01_Permission> permissionEntities);
        Task<ListResult<PermissionObjectCustom>> GetAllPermiss(PagingParam param);
        Task<List<QTTS01_Permission>> GetListPermissionByPermissionObject(Guid permissionObjectId, Guid tenantId);
        Task<List<Guid>> GetListProfileByTenant(Guid tenantId);
        Task UpdateCustom(QTTS01_PermissionObject permissionObjectEntity, List<QTTS01_Permission> permissionEntities);
    }
}