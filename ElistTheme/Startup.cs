using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OrchardCore.ResourceManagement;
using OrchardCore.DisplayManagement.Implementation;
using OrchardCore.ContentTypes.Editors;
using Microsoft.Extensions.Hosting;
using OrchardCore.Modules;
using Microsoft.AspNetCore.Hosting;

namespace ElistTheme
{
    public class Startup : OrchardCore.Modules.StartupBase
    {
        public override void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IConfigureOptions<ResourceManagementOptions>, ResourceManagementOptionsConfiguration>();

        }
    }
}
