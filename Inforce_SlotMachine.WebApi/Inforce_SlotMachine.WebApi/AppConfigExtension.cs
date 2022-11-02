using Inforce_SlotMachine.BLL.Abstract;
using Inforce_SlotMachine.BLL.Implementations;
using Inforce_SlotMachine.BLL.Profiles;
using Inforce_SlotMachine.Common.Options;
using Inforce_SlotMachine.DAL;
using Inforce_SlotMachine.WebApi.Middlewares;
using MongoDB.Driver;

namespace Inforce_SlotMachine.WebApi
{
    public static class AppConfigExtension
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {

            services
                .SetUpDatabase(configuration)
                .AddEndpointsApiExplorer()
                .AddSwaggerGen()
                .RegisterMappingProfiles()
                .AddControllers();

            services
                .AddTransient<IUserService, UserService>()
                .AddTransient<ISpinService, SpinService>();
        }

        public static void RegisterMiddlewares(this WebApplication app)
        {
            app.UseMiddleware<ExceptionHandler>(app.Logger);
        }

        private static IServiceCollection SetUpDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var options = new SlotMachineDbOptions();
            configuration.GetSection(nameof(SlotMachineDbOptions)).Bind(options);

            var mongoClient = new MongoClient(configuration.GetConnectionString(nameof(MongoClient)));

            var slotMachineDb = new SlotMachineDb(mongoClient, options);

            services.AddSingleton(slotMachineDb);

            return services;
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
