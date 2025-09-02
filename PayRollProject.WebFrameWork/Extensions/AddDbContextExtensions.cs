namespace PayRollProject.WebFrameWork.Extensions
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using PayRollProject.DataModel;

    public static class AddDbContextExtensions
    {
        public static IServiceCollection AddDbContextService(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<PayRollDbContext>(options =>
                {
                    options.UseSqlServer(configuration.GetConnectionString("PayRollConnectionString"));
                });

            return services;
        }

    }
}