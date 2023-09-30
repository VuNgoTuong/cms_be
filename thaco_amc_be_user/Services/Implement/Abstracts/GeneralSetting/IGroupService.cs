using Common.Commons;
using Common.Params.Base;
using Repository.CustomModel;
using System;
using System.Threading.Tasks;
using UserManagement.Models.Main;

namespace UserManagement.Services.Implement.GeneralSetting
{
    public interface IGroupService
    {
        Task<ResponseService<GroupResponse>> Create(GroupRequest obj);
        Task<ResponseService<bool>> Delete(Guid id);
        Task<ResponseService<ListResult<GroupResponse>>> GetAll(PagingParamCustom param);
        Task<ResponseService<GroupResponse>> GetById(Guid id);
        Task<ResponseService<GroupResponse>> Update(GroupRequest obj);
    }
}