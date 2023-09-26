using Common.Commons;
using Microsoft.EntityFrameworkCore;
using Repository.CustomModel;
using Repository.Model;

namespace Repository.Repositories
{
    public class PermissionRepository : BaseRepositorySql<QTTS01_Permission>, IPermissionRepository
    {
        public async Task<bool> CheckExistsPermissionObject(Guid tenantId, Guid permissionObjectId)
        {
            return await _db.QTTS01_PermissionObjects.AnyAsync(x => x.id == permissionObjectId && x.tenant_id == tenantId);
        }

        public async Task<bool> CheckExistsProfile(Guid tenantId, Guid profileId)
        {
            return await _db.QTTS01_Profiles.AnyAsync(x => x.id == profileId && x.tenant_id == tenantId);
        }

        // lấy danh sách quyền truy cập của một user.
        public async Task<List<PermissionResShort>> GetListPermissionByUser(string username, bool isRoot, bool isAdmin)
        {
            bool flag = true;

            List<PermissionResShort> response = new List<PermissionResShort>();
            List<PermissionModel> permissionsResult = new List<PermissionModel>();
            var listProfileid = await (from p in _db.QTTS01_Profiles
                                       join m in _db.QTTS01_MapProfileUsers on p.id equals m.profile_id
                                       where m.username.Equals(username) && p.is_active
                                       select new
                                       {
                                           profile_id = p.id
                                       }).ToListAsync();


            if (isAdmin || isRoot)
            {
                foreach (var item in listProfileid)
                {
                    var listPermisson = await _db.Set<QTTS01_Permission>().Include(po => po.QTTS01_PermissionObject)
                                                                .Include(mp => mp.QTTS01_Profile.QTTS01_MapProfileUser)
                                                                .Include(mo => mo.QTTS01_PermissionObject.QTTS01_Module)
                                                                    .Where(x => x.profile_id == item.profile_id)
                                                                    .Select(x => new PermissionModel()
                                                                    {
                                                                        id = x.id,
                                                                        permissionobject_id = x.permissionobject_id,
                                                                        object_name = x.QTTS01_PermissionObject.object_name,
                                                                        module_name = x.QTTS01_PermissionObject.QTTS01_Module.module_name,
                                                                        is_show = x.is_show,
                                                                        is_allow_access = x.is_allow_access,
                                                                        is_allow_create = x.is_allow_create,
                                                                        is_allow_delete = x.is_allow_delete,
                                                                        is_allow_edit = x.is_allow_edit,
                                                                        is_active = x.is_active
                                                                    })
                                                                    .ToListAsync();
                    if (flag)
                    {
                        permissionsResult = listPermisson;
                        flag = false;
                    }
                    else
                    {
                        foreach (var permissionR in permissionsResult)
                        {
                            foreach (var permission in listPermisson)
                            {
                                if (permissionR.permissionobject_id.Equals(permission.permissionobject_id))
                                {
                                    if (!permissionR.is_show)
                                        permissionR.is_show = permission.is_show;
                                    if (!permissionR.is_allow_access)
                                        permissionR.is_allow_access = permission.is_allow_access;
                                    if (!permissionR.is_allow_create)
                                        permissionR.is_allow_create = permission.is_allow_create;
                                    if (!permissionR.is_allow_delete)
                                        permissionR.is_allow_delete = permission.is_allow_delete;
                                    if (!permissionR.is_allow_edit)
                                        permissionR.is_allow_edit = permission.is_allow_edit;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (var item in listProfileid)
                {
                    var listPermisson = await _db.Set<QTTS01_Permission>().Include(po => po.QTTS01_PermissionObject)
                                                                .Include(mp => mp.QTTS01_Profile.QTTS01_MapProfileUser)
                                                                .Include(mo => mo.QTTS01_PermissionObject.QTTS01_Module)
                                                                    .Where(x => x.profile_id == item.profile_id && x.QTTS01_PermissionObject.QTTS01_Module.is_active)
                                                                    .Select(x => new PermissionModel()
                                                                    {
                                                                        id = x.id,
                                                                        permissionobject_id = x.permissionobject_id,
                                                                        object_name = x.QTTS01_PermissionObject.object_name,
                                                                        module_name = x.QTTS01_PermissionObject.QTTS01_Module.module_name,
                                                                        is_show = x.is_show,
                                                                        is_allow_access = x.is_allow_access,
                                                                        is_allow_create = x.is_allow_create,
                                                                        is_allow_delete = x.is_allow_delete,
                                                                        is_allow_edit = x.is_allow_edit,
                                                                        is_active = x.is_active
                                                                    })
                                                                    .ToListAsync();
                    if (flag)
                    {
                        permissionsResult = listPermisson;
                        flag = false;
                    }
                    else
                    {
                        foreach (var permissionR in permissionsResult)
                        {
                            foreach (var permission in listPermisson)
                            {
                                if (permissionR.permissionobject_id.Equals(permission.permissionobject_id))
                                {
                                    if (!permissionR.is_show)
                                        permissionR.is_show = permission.is_show;
                                    if (!permissionR.is_allow_access)
                                        permissionR.is_allow_access = permission.is_allow_access;
                                    if (!permissionR.is_allow_create)
                                        permissionR.is_allow_create = permission.is_allow_create;
                                    if (!permissionR.is_allow_delete)
                                        permissionR.is_allow_delete = permission.is_allow_delete;
                                    if (!permissionR.is_allow_edit)
                                        permissionR.is_allow_edit = permission.is_allow_edit;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            response.AddRange(CommonFuncMain.ToObjectList<PermissionResShort>(permissionsResult));
            return response;
        }

        // lấy quyền của người dùng ở một đối tượng phân quyền.
        public virtual async Task<PermissionResShort> GetPermissionAObjectByUser(PermissionAObjectRequest request)
        {
            PermissionResShort response = null;

            var listProfileId = await _db.Set<QTTS01_MapProfileUser>().Where(x => x.username == request.username).Select(x => x.profile_id).ToListAsync();

            foreach (var item in listProfileId)
            {
                var permisson = await _db.QTTS01_Permissions.Include(po => po.QTTS01_PermissionObject)
                                                            .Where(x => x.profile_id == item && x.permissionobject_id == request.permissionobject_id)
                                                            .Select(x => new PermissionResShort()
                                                            {
                                                                id = x.id,
                                                                object_name = x.QTTS01_PermissionObject.object_name,
                                                                is_show = x.is_show,
                                                                is_allow_access = x.is_allow_access,
                                                                is_allow_create = x.is_allow_create,
                                                                is_allow_delete = x.is_allow_delete,
                                                                is_allow_edit = x.is_allow_edit,
                                                                is_active = x.is_active
                                                            }).FirstOrDefaultAsync();
                if (permisson != null) response = permisson;
            }
            return response;
        }

        public async Task<bool> GetStatusPermissionByTypeAndName(GetPermissionByTypeAndName request)
        {

            // Lấy danh sách profile mà user được thêm vào
            List<Guid> profileByUser = await _db.Set<QTTS01_MapProfileUser>().Where(x => x.username == request.username && x.QTTS01_Profile.is_active).Select(x => x.profile_id).ToListAsync();

            switch (request.permission_type?.Trim().ToLower())
            {
                case "access":
                    return await _db.QTTS01_Permissions.AnyAsync(x => x.is_allow_access && x.object_name == request.permission_name && profileByUser.Contains(x.profile_id));
                case "create":
                    return await _db.QTTS01_Permissions.AnyAsync(x => x.is_allow_create && x.object_name == request.permission_name && profileByUser.Contains(x.profile_id));
                case "edit":
                    return await _db.QTTS01_Permissions.AnyAsync(x => x.is_allow_edit && x.object_name == request.permission_name && profileByUser.Contains(x.profile_id));
                case "delete":
                    return await _db.QTTS01_Permissions.AnyAsync(x => x.is_allow_delete && x.object_name == request.permission_name && profileByUser.Contains(x.profile_id));
            }

            return false;
        }
    }
}
