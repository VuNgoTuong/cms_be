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
        Task<ResponseService<BankResponse>> Create(BankRequest obj);
        Task<ResponseService<bool>> Delete(Guid id);
        Task<ResponseService<ListResult<BankResponse>>> GetAll(PagingParam param);
        Task<ResponseService<BankResponse>> GetById(Guid id);
        Task<ResponseService<BankResponse>> Update(BankRequest obj);
    }
}