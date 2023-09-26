using Common.Commons;
using Common.Params.Base;
using Repository.CustomModel;
using System.Threading.Tasks;
using UserManagement.Models.Main;

namespace UserManagement.Services.Implement.UserSetting
{
    public interface IUserService
    {
        Task<ResponseService<bool>> ChangePassword(UpdatePasswordRequest request);
        Task<ResponseService<UserCustomResponse>> Create(UserRequest obj);
        Task<ResponseService<bool>> Delete(string username);
        Task<ResponseService<ListResult<UserCustomResponse>>> GetAll(PagingParam param);
        Task<ResponseService<UserCustomResponse>> GetById(string username);
        Task<ResponseService<UserCustomResponse>> Update(UserUpdateRequest obj);
        Task<ResponseService<UserCustomResponse>> UpdateInformation(UpdateInformation request);
    }
}