using Common.Commons;
using Repository.CustomModel;
using System.Threading.Tasks;
using UserManagement.Models.Main;

namespace UserManagement.Services.Implement.GeneralSetting
{
    public interface IAuthenticationService
    {
        Task<ResponseService<TokenResponse>> CheckAuthentication();
        Task<ResponseService<LoginResponse>> Login(LoginRequest request);
        Task<ResponseService<bool>> Logout();
    }
}