using Common.Commons;
using Common.Params.Base;
using Repository.CustomModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Models.Main;

namespace UserManagement.Services.Implement.GeneralSetting
{
    public interface IPermissionService
    {
        Task<ResponseService<PermissionResponse>> Create(PermissionRequest request);
        Task<ResponseService<bool>> Delete(Guid id);
        Task<ResponseService<ListResult<PermissionResponse>>> GetAll(PagingParam param);
        Task<ResponseService<PermissionResponse>> GetById(Guid id);
        Task<ResponseService<List<PermissionResShort>>> GetListPermissionByUser(string username);
        Task<PermissionResShort> GetPermissionAObjectByUser(PermissionAObjectRequest request);
        Task<ResponseService<ListResult<PermissionResponse>>> GetPermissionListByProfile(PagingRequest<PermissonSearchRequest> param);
        Task<ResponseService<bool>> GetStatusPermissionByTypeAndName(GetPermissionByTypeAndName request);
        Task<ResponseService<PermissionResponse>> Update(PermissionRequest request);
    }
}