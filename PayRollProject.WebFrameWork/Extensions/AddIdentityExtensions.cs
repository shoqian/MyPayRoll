namespace PayRollProject.WebFrameWork.Extensions
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using PayRollProject.DataModel;
    using PayRollProject.Entities.Entities;

    public static class AddIdentityExtensions
    {
        public static IServiceCollection AddIdentityService(this IServiceCollection service)
        {
            service.AddIdentity<ApplicationUsers, ApplicationRoles>((IdentityOptions options) =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;

                }).AddEntityFrameworkStores<PayRollDbContext>();

            return service;
        }
    }
}