using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ElistaWeb
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOrchardCms()
#if DEBUG
                .AddSetupFeatures("OrchardCore.AutoSetup")
#else
                .AddAzureShellsConfiguration() //put shells info into blob
#endif
                ;
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
                app.UseDeveloperExceptionPage();
            //}

            app.UseRouting();
            app.UseOrchardCore();
        }
    }
}
