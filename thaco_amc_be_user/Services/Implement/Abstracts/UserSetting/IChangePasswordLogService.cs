using Common.Commons;
using Common.Params.Base;
using Repository.CustomModel;
using Repository.Model;
using System.Threading.Tasks;

namespace UserManagement.Services.Implement.UserSetting
{
    public interface IChangePasswordLogService
    {
        Task<ResponseService<ListResult<QTTS01_ChangePasswordLog>>> GetAll(PagingParam param);
    }
}