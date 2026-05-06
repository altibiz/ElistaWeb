using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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

            // Redirect bare and www apex of formadria.com to the canonical Jesmonite host.
            // Runs before UseOrchardCore so the request never reaches tenant routing.
            app.Use(async (context, next) =>
            {
                var host = context.Request.Host.Host;
                if (string.Equals(host, "formadria.com", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(host, "www.formadria.com", StringComparison.OrdinalIgnoreCase))
                {
                    var target = $"https://jesmonite.formadria.com{context.Request.Path}{context.Request.QueryString}";
                    context.Response.Redirect(target, permanent: true);
                    return;
                }
                await next();
            });

            app.UseOrchardCore();
        }
    }
}
