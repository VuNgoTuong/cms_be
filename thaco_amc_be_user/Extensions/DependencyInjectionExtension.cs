using UserManagement.Services.Implement;
using Microsoft.Extensions.DependencyInjection;
using Repository.Repositories;
using UserManagement.Services.Implement.GeneralSetting;
using UserManagement.Services.Implement.UserSetting;

namespace UserManagement.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static void AddDependency(this IServiceCollection services)
        {
            //Inject Service
            services.AddTransient<IStoreProcedureExecute, StoreProcedureExecute>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IMapProfileUserService, MapProfileUserService>();
            services.AddTransient<IProfileService, ProfileService>();
            services.AddTransient<IModuleService, ModuleService>();
            services.AddTransient<IPermissionService, PermissionService>();
            services.AddTransient<IPermissionObjectService, PermissionObjectService>();
            services.AddTransient<IChangePasswordLogService, ChangePasswordLogService>();
            services.AddTransient<IBankService, BankService>();


            //Repository
            services.AddTransient(typeof(IBaseRepositorySql<>), typeof(BaseRepositorySql<>));
            services.AddTransient<IAuthenticationRepository, AuthenticationRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IMapProfileUserRepository, MapProfileUserRepository>();
            services.AddTransient<IProfileRepository, ProfileRepository>();
            services.AddTransient<IModuleRepository, ModuleRepository>();
            services.AddTransient<IPermissionRepository, PermissionRepository>();
            services.AddTransient<IPermissionObjectRepository, PermissionObjectRepository>();
            services.AddTransient<IChangePasswordLogRepository, ChangePasswordLogRepository>();
            services.AddTransient<IBankRepository, BankRepository>();

        }
    }
}
