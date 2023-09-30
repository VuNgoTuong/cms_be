using Common.Commons;
using Common.Params.Base;
using Repository.CustomModel;
using System;
using System.Threading.Tasks;
using UserManagement.Models.Main;

namespace UserManagement.Services.Implement.GeneralSetting
{
    public interface ITenantService
    {
        Task<ResponseService<TenantResponse>> Create(TenantRequest obj);
        Task<ResponseService<bool>> Delete(Guid id);
        Task<ResponseService<ListResult<TenantResponse>>> GetAllCustom(PagingParamCustom param);
        Task<ResponseService<TenantResponse>> GetById(Guid id);
        Task<ResponseService<TenantResponse>> Update(TenantRequest obj);
    }
}