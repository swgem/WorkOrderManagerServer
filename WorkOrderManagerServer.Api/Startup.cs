using WorkOrderManagerServer.App.Extensions;
using WorkOrderManagerServer.App.IoC;

namespace WorkOrderManagerServer.App
{
    public class Startup : Interfaces.IStartup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;

        void Interfaces.IStartup.ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers();
            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddSwagger();
            services.AddAuthentication(Configuration);
            //services.AddAuthorizationPolicies();
            services.RegisterServices(Configuration);
        }

        void Interfaces.IStartup.Configure(WebApplication app, IWebHostEnvironment env)
        {
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors(builder => builder
                .SetIsOriginAllowed(orign => true)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
            app.MapControllers();
        }
    }
}