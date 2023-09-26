using Common.Commons;
using Common.Params.Base;
using Repository.CustomModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Models.Main;

namespace UserManagement.Services.Implement.GeneralSetting
{
    public interface IModuleService
    {
        Task<ResponseService<ModuleResponse>> Create(ModuleRequest obj);
        Task<ResponseService<bool>> Delete(Guid id);
        Task<ResponseService<ListResult<ModuleResponse>>> GetAll(PagingParam param);
        Task<ResponseService<List<ModuleCustomResponse>>> GetAllWithIsShow();
        Task<ResponseService<ModuleResponse>> GetById(Guid id);
        Task<ResponseService<ModuleResponse>> Update(ModuleRequest obj);
    }
}