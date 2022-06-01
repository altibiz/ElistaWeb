using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OrchardCore.ResourceManagement;
using OrchardCore.DisplayManagement.Descriptors;

namespace ElistaTheme
{
    public class Startup : OrchardCore.Modules.StartupBase
    {
        public override void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IConfigureOptions<ResourceManagementOptions>, ResourceManagementOptionsConfiguration>();
        }
    }
}
