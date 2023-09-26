using Common.Params.Base;
using Repository.CustomModel;
using Repository.Model;

namespace Repository.Repositories
{
    public interface IProfileRepository : IBaseRepositorySql<QTTS01_Profile>
    {
        Task<QTTS01_MapProfileUser> CheckExistsDataRemove(Guid tenantId, string username, Guid profileId);
        Task<bool> CheckUserRemoveIsAdmin(Guid tenantId, string username);
        Task CreateCustom(QTTS01_Profile entity, List<QTTS01_Permission> permissionEntities);
        Task DeleteProfile(QTTS01_Profile profileEntity);
        Task DeleteUserInProfile(QTTS01_MapProfileUser dataRemove);
        Task<List<QTTS01_Permission>> GetListPermissionInProfile(List<Guid> permissionIdList, Guid profileId, Guid tenantId);
        Task<List<QTTS01_PermissionObject>> GetPermissionObjectListByTenant(Guid tenantId);
        Task<ListResult<UserModel>> GetUsersByProfile(PagingParam param);
        Task UpdateListPermissionInProfile(List<QTTS01_Permission> permisionEntities);
    }
}