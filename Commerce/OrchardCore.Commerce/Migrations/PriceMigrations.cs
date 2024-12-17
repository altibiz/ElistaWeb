using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.Data.Migration;
using System.Threading.Tasks;

namespace OrchardCore.Commerce.Migrations
{
    /// <summary>
    /// Adds the price part to the list of available parts.
    /// </summary>
    public class PriceMigrations : DataMigration
    {
        IContentDefinitionManager _contentDefinitionManager;

        public PriceMigrations(IContentDefinitionManager contentDefinitionManager)
        {
            _contentDefinitionManager = contentDefinitionManager;
        }

        public async Task<int> CreateAsync()
        {
            await _contentDefinitionManager.AlterPartDefinitionAsync("PricePart", builder => builder
                .Attachable()
                .Reusable()
                .WithDescription("Adds a simple price to a product."));
            return 1;
        }
    }
}
