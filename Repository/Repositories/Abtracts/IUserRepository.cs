using Common.Params.Base;
using Repository.CustomModel;
using Repository.Model;

namespace Repository.Repositories
{
    public interface IUserRepository : IBaseRepositorySql<QTTS01_User>
    {
        Task ChangePasswordAndCreateChangePasswordLog(QTTS01_User userEntity, QTTS01_ChangePasswordLog log);
        Task<bool> CheckDuplicateOldPassword(string newPassword, string username, Guid tenantId, int numberHistoryPasswordValidate);
        Task<bool> CheckExistsProfile(Guid profileId, Guid tenantId);
        Task<bool> CheckInformation(string username, string password, Guid tenantId);
        Task CreateAndMapUserToProfile(QTTS01_User userEntity);
        Task<ListResult<UserCustomResponse>> GetListUser(PagingParam param, string currentUser);
        Task RemoveUserData(QTTS01_User userEntity);
    }
}