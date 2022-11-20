using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WorkOrderManagerServer.Application.Services;
using WorkOrderManagerServer.Identity.Configurations;
using WorkOrderManagerServer.Identity.Data;
using WorkOrderManagerServer.Identity.Services;
using WorkOrderManagerServer.Repo;
using WorkOrderManagerServer.Services;

namespace WorkOrderManagerServer.App.IoC
{
    public static class NativeInjectorConfig
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<WorkOrderDbContext>(opt => opt.UseInMemoryDatabase("StdDb"));
            services.AddDbContext<IdentityDataContext>(opt => opt.UseInMemoryDatabase("StdDb"));

            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<IdentityDataContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IWorkOrder, WorkOrderRepo>();
        }
    }
}
