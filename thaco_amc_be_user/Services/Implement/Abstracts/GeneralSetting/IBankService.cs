using Common.Commons;
using Common.Params.Base;
using Repository.CustomModel;
using System;
using System.Threading.Tasks;
using UserManagement.Models.Main;

namespace UserManagement.Services.Implement.GeneralSetting
{
    public interface IBankService
    {
        Task<ResponseService<ListResult<BankResponse>>> GetAll(PagingParam param);
        Task<ResponseService<BankResponse>> GetById(Guid id);
    }
}