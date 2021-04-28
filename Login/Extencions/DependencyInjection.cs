using Login.Business;
using Login.Business.Interface;
using Login.Infrrastructure;
using Login.Infrrastructure.Interface;
using Login.Repository;
using Login.Repository.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace Login.Extencions
{
    public static class DependencyInjection
    {
        public static void DependencyInsection(this IServiceCollection services)
        {
            DependencyInjectionBusiness(services);
            DependencyInjectionRepository(services);
            DependencyInjectionInfrastructure(services);
        }

        public static void DependencyInjectionBusiness(IServiceCollection services)
        {
            services.AddTransient<ILoginBusiness, LoginBusiness>();
            services.AddTransient<IUserBusiness, UserBusiness>();
        }
        public static void DependencyInjectionInfrastructure(IServiceCollection services)
        {
            services.AddSingleton<IRecoverPasswordInfrastructure, RecoverPasswordInfrastructure>();           
        }

        public static void DependencyInjectionRepository(IServiceCollection services)
        {
            services.AddSingleton<IUserLoginRepository, UserLoginRepository>();
            services.AddSingleton<ILoginRepository, LoginRepository>();
        }
    }
}
