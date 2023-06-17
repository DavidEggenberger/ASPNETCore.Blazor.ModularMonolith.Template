using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Modules.LandingPages.Web.Server;
using Modules.LandingPages.Web.Server.MyFeature.Pages;
using Modules.TenantIdentity.DomainFeatures;
using Modules.TenantIdentity.Web.Server;
using Modules.Subscription.Server;
using Shared.DomainFeatures;
using Shared.Infrastructure;
using System.Reflection;

namespace Web.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .RegisterTenantIdentityModuleControllers()
                .RegisterSubscriptionModuleControllers(Configuration);

            services.AddRazorPages()
                .RegisterLandingPagesModulePages();

            services.RegisterSharedDomainFeaturesServices();

            services.RegisterSharedInfrastructure(new Assembly[]
            {
                typeof(Modules.TenantIdentity.Web.Server.Registrator).Assembly,
                typeof(Modules.Subscription.Server.Registrator).Assembly,
            });

            services.RegisterTenantIdentityModule(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();

                endpoints.RegisterLandingPagesModuleFallbackPage();
            });
        }
    }
}
