using Carousel.OrchardCore.Base.CodeGeneration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.ContentTypes.Editors;
using OrchardCore.Data.Migration;
using OrchardCore.DisplayManagement.Implementation;
using StartupBase = OrchardCore.Modules.StartupBase;

namespace Carousel.OrchardCore
{
    public class Startup : StartupBase 
    {
        public IWebHostEnvironment CurrentEnvironment { get; }

        public Startup(IWebHostEnvironment env)
        {
            CurrentEnvironment = env;
        }
        public override void ConfigureServices(IServiceCollection services) {
            services.AddScoped<IDataMigration, Migrations>();

                services.AddScoped<IShapeDisplayEvents, ShapeTracingShapeEvents>();
                services.AddScoped<IContentTypeDefinitionDisplayDriver, CodeGenerationDisplayDriver>();
        }
    }
}