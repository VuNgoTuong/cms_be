using Common.Commons;
using Common.Params.Base;
using Repository.CustomModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Models.Main;

namespace UserManagement.Services.Implement.GeneralSetting
{
    public interface IProfileService
    {
        Task<ResponseService<ProfileResponse>> Create(ProfileRequest request);
        Task<ResponseService<bool>> Delete(Guid id);
        Task<ResponseService<bool>> DeleteUserInProfile(DeleteUserInProfile request);
        Task<ResponseService<ListResult<ProfileResponse>>> GetAll(PagingParam param);
        Task<ResponseService<ProfileResponse>> GetById(Guid id);
        Task<ResponseService<ListResult<UserModel>>> GetListUserByProfile(PagingParam param);
        Task<ResponseService<ProfileResponse>> Update(ProfileRequest request);
        Task<ResponseService<bool>> UpdatePermissionInProfile(List<UpdatePermissonRequest> request);
    }
}