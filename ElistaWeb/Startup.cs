using ElistaWeb.Base.CodeGeneration;
using Lombiq.HelpfulExtensions.Extensions.CodeGeneration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrchardCore.ContentTypes.Editors;
using OrchardCore.DisplayManagement.Implementation;

namespace ElistaWeb
{
    public class Startup
    {
        public IWebHostEnvironment CurrentEnvironment { get; }

        public Startup(IWebHostEnvironment env)
        {
            CurrentEnvironment = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOrchardCms()
#if DEBUG
                //.AddSetupFeatures("OrchardCore.AutoSetup")
#else
                //.AddAzureShellsConfiguration() //put shells info into blob
#endif
                ;


            if (CurrentEnvironment.IsDevelopment())
            {
                services.AddScoped<IShapeDisplayEvents, ShapeTracingShapeEvents>();
                services.AddScoped<IContentTypeDefinitionDisplayDriver, CodeGenerationDisplayDriver>();
            }


        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseOrchardCore();
        }
    }
}
