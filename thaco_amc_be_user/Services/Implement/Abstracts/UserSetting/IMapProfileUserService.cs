using Common.Commons;
using System.Threading.Tasks;
using UserManagement.Models.Main;

namespace UserManagement.Services.Implement.UserSetting
{
    public interface IMapProfileUserService
    {
        Task<ResponseService<bool>> Create(AddUserToProfile request);
    }
}