using Common;
using Common.Commons;
using Common.Params.Base;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Repository.CustomModel;
using Repository.Model;
using Repository.Queries;
using System.Data;

namespace Repository.Repositories
{
    public class UserRepository : BaseRepositorySql<QTTS01_User>, IUserRepository
    {
        private Query<UserCustomResponse> _query;
        private readonly IStoreProcedureExecute _storeProcedure;
        public UserRepository(IStoreProcedureExecute storeProcedure) : base()
        {
            _storeProcedure = storeProcedure;
            _query = new Query<UserCustomResponse>(_db);
        }

        #region implement
        public async Task<ListResult<UserCustomResponse>> GetListUser(PagingParam param, string currentUser)
        {
            //ListResult<UserCustomResponse> resultList = new ListResult<UserCustomResponse>();
            //// get list user report_to
            //var userList = await GetListUserRole(new UsernameRequest() { username = currentUser, tenant_id = param.tenant_id });      
            //return resultList;
            ListResult<UserCustomResponse> result = new ListResult<UserCustomResponse>();
            DynamicParameters parameters = new DynamicParameters(
                       new
                       {
                           username = currentUser,
                           tenant_id = param.tenant_id
                       });

            result.items = await Task.FromResult(_storeProcedure.ExecuteReturnList<UserCustomResponse>("usp_User_Get_User_Information", parameters).Result.ToList());
            result.total = result.items.Count;

            return result;
        }

        #endregion      

        public async Task<bool> CheckExistsProfile(Guid profileId, Guid tenantId)
        {
            return await _db.QTTS01_Profiles.AnyAsync(x => x.tenant_id == tenantId && x.id == profileId);
        }

        public async Task CreateAndMapUserToProfile(QTTS01_User userEntity)
        {
            using (IDbContextTransaction transaction = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    // Default khi tạo sẽ add user vào profile có ít quyền nhất
                    Guid profileToMap = await GetProfileAtLeastPermission(userEntity.tenant_id);
                    QTTS01_MapProfileUser mapProfileEntity = new QTTS01_MapProfileUser();
                    mapProfileEntity.id = Guid.NewGuid();
                    mapProfileEntity.username = userEntity.username;
                    mapProfileEntity.profile_id = profileToMap;
                    mapProfileEntity.description = "System";
                    mapProfileEntity.is_active = true;
                    mapProfileEntity.create_time = userEntity.create_time;
                    mapProfileEntity.create_by = userEntity.create_by;
                    mapProfileEntity.modify_time = userEntity.modify_time;
                    mapProfileEntity.modify_by = userEntity.modify_by;
                    mapProfileEntity.tenant_id = userEntity.tenant_id;

                    await _db.QTTS01_MapProfileUsers.AddAsync(mapProfileEntity);
                    await _db.QTTS01_Users.AddAsync(userEntity);

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

        public async Task ChangePasswordAndCreateChangePasswordLog(QTTS01_User userEntity, QTTS01_ChangePasswordLog log)
        {
            using (IDbContextTransaction transaction = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    _db.QTTS01_Users.Update(userEntity);
                    await _db.QTTS01_ChangePasswordLogs.AddAsync(log);

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

        public async Task<bool> CheckDuplicateOldPassword(string newPassword, string username, Guid tenantId, int numberHistoryPasswordValidate)
        {
            var getLastestPassword = await _db.QTTS01_ChangePasswordLogs.AsNoTracking().Where(x => x.username == username && x.tenant_id == tenantId).OrderByDescending(x => x.create_time).Take(numberHistoryPasswordValidate).ToListAsync();
            if (getLastestPassword != null)
            {
                foreach (var item in getLastestPassword)
                {
                    if (item.old_password == newPassword)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public async Task<bool> CheckInformation(string username, string password, Guid tenantId)
        {
            bool checkUsernameAndPassword = await _db.QTTS01_Users.AnyAsync(x => x.username == username && x.password == password && x.tenant_id == tenantId);
            if (checkUsernameAndPassword)
            {
                return true;
            }

            return false;
        }

        public async Task RemoveUserData(QTTS01_User userEntity)
        {
            using (IDbContextTransaction transaction = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    List<QTTS01_MapProfileUser> mapProfileUserEntities = await _db.QTTS01_MapProfileUsers.Where(x => x.username == userEntity.username && x.tenant_id == userEntity.tenant_id).ToListAsync();

                    // Remove data by user 

                    _db.QTTS01_MapProfileUsers.RemoveRange(mapProfileUserEntities);
                    _db.QTTS01_Users.Remove(userEntity);

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

        #region private func
        // Lấy profile có ít permission nhất
        private async Task<Guid> GetProfileAtLeastPermission(Guid tenantId)
        {
            Dictionary<Guid, int> profileAtLeasePermission = new Dictionary<Guid, int>();
            var profileByTenantList = await _db.QTTS01_Profiles.Where(x => x.is_active && x.tenant_id == tenantId).Select(x => x.id).Distinct().ToListAsync();
            var permissionByTenantList = await _db.QTTS01_Permissions.Where(x => x.tenant_id == tenantId)
                .Select(x => new { x.profile_id, x.is_allow_access, x.is_allow_create, x.is_allow_delete, x.is_allow_edit, x.is_show }).ToListAsync();

            foreach (var item in profileByTenantList)
            {
                var countPermissionInProfile = permissionByTenantList.Where(x => x.profile_id == item)
                                               .Sum(x =>
                                               Convert.ToInt32(x.is_allow_access) +
                                               Convert.ToInt32(x.is_allow_create) +
                                               Convert.ToInt32(x.is_allow_delete) +
                                               Convert.ToInt32(x.is_allow_edit) +
                                               Convert.ToInt32(x.is_show));

                profileAtLeasePermission.Add(item, countPermissionInProfile);
            }

            var result = profileAtLeasePermission.Aggregate((x, y) => x.Value < y.Value ? x : y).Key;
            return result;
        }
        #endregion
    }
}
