using AutoMapper;
using Repository.CustomModel;
using Repository.Model;
using UserManagement.Models.Main;

namespace UserManagement.Config
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<QTTS01_User, UserResponse>().ReverseMap();
            CreateMap<UserRequest, QTTS01_User>().ReverseMap();
            CreateMap<UserUpdateRequest, QTTS01_User>().ReverseMap();
            CreateMap<QTTS01_User, UserCustomModel>().ReverseMap();
            CreateMap<QTTS01_User, UserCustomResponse>().ReverseMap();

            CreateMap<ModuleRequest, QTTS01_Module>().ReverseMap();
            CreateMap<QTTS01_Module, ModuleResponse>().ReverseMap();
            CreateMap<ListResult<QTTS01_Module>, ListResult<ModuleResponse>>().ReverseMap();

            CreateMap<ProfileRequest, QTTS01_Profile>().ReverseMap();
            CreateMap<QTTS01_Profile, ProfileResponse>().ReverseMap();
            CreateMap<ListResult<QTTS01_Profile>, ListResult<ProfileResponse>>().ReverseMap();

            CreateMap<PermissionObjectRequest, QTTS01_PermissionObject>().ReverseMap();
            CreateMap<QTTS01_PermissionObject, PermissionObjectResponse>().ReverseMap();
            CreateMap<ListResult<QTTS01_PermissionObject>, ListResult<PermissionObjectResponse>>().ReverseMap();
            CreateMap<PermissionRequest, QTTS01_Permission>().ReverseMap();
            CreateMap<QTTS01_Permission, PermissionResponse>().ReverseMap();
            CreateMap<ListResult<QTTS01_Permission>, ListResult<PermissionResponse>>().ReverseMap();

            CreateMap<QTTS01_Bank, BankResponse>().ReverseMap();
            CreateMap<ListResult<QTTS01_Bank>, ListResult<BankResponse>>().ReverseMap();


        }
    }
}
