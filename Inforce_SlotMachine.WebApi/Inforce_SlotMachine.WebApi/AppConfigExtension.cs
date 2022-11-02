using Inforce_SlotMachine.BLL.Abstract;
using Inforce_SlotMachine.BLL.Implementations;
using Inforce_SlotMachine.BLL.Profiles;
using Inforce_SlotMachine.DAL;
using Inforce_SlotMachine.WebApi.Middlewares;

namespace Inforce_SlotMachine.WebApi
{
    public static class AppConfigExtension
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services
                .AddEndpointsApiExplorer()
                .AddSwaggerGen()
                .RegisterMappingProfiles()
                .AddControllers();

            services
                .AddSingleton<SlotMachineDb>()

                .AddTransient<IUserService, UserService>()
                .AddTransient<ISpinService, SpinService>();
        }

        public static void RegisterMiddlewares(this WebApplication app)
        {
            app.UseMiddleware<ExceptionHandler>(app.Logger);
        }

        private static IServiceCollection RegisterMappingProfiles(this IServiceCollection services)
        {
            services.AddAutoMapper(opt => 
            {
                opt.AddProfile<UserProfile>();
            });

            return services;
        }
    }
}
