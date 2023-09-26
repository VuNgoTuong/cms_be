using Common.Commons;
using Repository.CustomModel;
using System;
using System.Threading.Tasks;
using UserManagement.Models.Main;

namespace UserManagement.Services.Implement.GeneralSetting
{
    public interface IPermissionObjectService
    {
        Task<ResponseService<PermissionObjectResponse>> Create(PermissionObjectRequest request);
        Task<ResponseService<bool>> Delete(Guid id);
        Task<ResponseService<ListResult<PermissionObjectCustom>>> GetAll(PagingRequest<PermissionObjectSearchRequest> param);
        Task<ResponseService<PermissionObjectResponse>> GetById(Guid id);
        Task<ResponseService<PermissionObjectResponse>> Update(PermissionObjectRequest request);
    }
}