using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WorkOrderManagerServer.Application.Services;
using WorkOrderManagerServer.Data.Repo;
using WorkOrderManagerServer.Data.Services;
using WorkOrderManagerServer.Identity.Data;
using WorkOrderManagerServer.Identity.Services;

namespace WorkOrderManagerServer.App.IoC
{
    public static class NativeInjectorConfig
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<WorkOrderDbContext>(opt => opt.UseInMemoryDatabase("StdDb"));
            services.AddDbContext<IdentityDbContext>(opt => opt.UseInMemoryDatabase("StdDb"));

            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<IdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IWorkOrderService, WorkOrderService>();
        }
    }
}
