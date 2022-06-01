using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.Data.Migration;
using OrchardCore.Commerce.Indexes;
using YesSql.Sql;
using OrchardCore.Recipes.Services;
using System.Threading.Tasks;

namespace OrchardCore.Commerce.Migrations
{
    /// <summary>
    /// Adds the product part to the list of available parts.
    /// </summary>
    public class ProductMigrations : DataMigration
    {
        IContentDefinitionManager _contentDefinitionManager;
        private IRecipeMigrator _rm;

        public ProductMigrations(IContentDefinitionManager contentDefinitionManager, IRecipeMigrator recipeMigrator)
        {
            _contentDefinitionManager = contentDefinitionManager;
            _rm = recipeMigrator;
        }

        public async Task<int> CreateAsync()
        {
            _contentDefinitionManager.AlterPartDefinition("ProductPart", builder => builder
                .Attachable()
                .WithDescription("Makes a content item into a product."));

            SchemaBuilder.CreateMapIndexTable<ProductPartIndex>(
                table => table
                    .Column<string>("Sku", col => col.WithLength(128))
                    .Column<string>("ContentItemId", c => c.WithLength(26))
            );

            SchemaBuilder.AlterTable(nameof(ProductPartIndex), table => table
                .CreateIndex("IDX_ProductPartIndex_Sku", "Sku")
            );

            await _rm.ExecuteAsync("Product.recipe.json", this);
            return 1;
        }
    }
}