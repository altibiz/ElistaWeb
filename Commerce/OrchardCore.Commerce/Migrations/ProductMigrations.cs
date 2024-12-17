using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.Data.Migration;
using OrchardCore.Commerce.Indexes;
using YesSql.Sql;
using OrchardCore.Recipes.Services;
using System.Threading.Tasks;
using OrchardCore.Lists.Models;

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
            await _contentDefinitionManager.AlterPartDefinitionAsync("ProductPart", builder => builder
                .Attachable()
                .WithDescription("Makes a content item into a product."));

            await SchemaBuilder.CreateMapIndexTableAsync<ProductPartIndex>(
                table => table
                    .Column<string>("Sku", col => col.WithLength(128))
                    .Column<string>("ContentItemId", c => c.WithLength(26))
            );

            await SchemaBuilder.AlterTableAsync(nameof(ProductPartIndex), table => table
                .CreateIndex("IDX_ProductPartIndex_Sku", "Sku")
            );

            await _rm.ExecuteAsync("Product.recipe.json", this);
            return 2;
        }

        public int UpdateFrom1Async()
        {
            return 2;
        }

        public async Task<int> UpdateFrom2Async()
        {
            await _contentDefinitionManager.AlterTypeDefinitionAsync("ProductList", type => type
                .DisplayedAs("Product List")
                .Creatable()
                .Listable()
                .Securable()
                .WithPart("ProductList", part => part
                    .WithPosition("1")
                )
                .WithPart("AutoroutePart", part => part
                    .WithPosition("2")
                )
                .WithPart("ListPart", part => part
                    .WithPosition("3")
                    .WithSettings(new ListPartSettings
                    {
                        PageSize = 30,
                        ContainedContentTypes = new[] { "Product" },
                    })
                )
                .WithPart("TitlePart", part => part
                    .WithPosition("0")
                )
            );
            await _rm.ExecuteAsync("productList.recipe.json", this);

            return 3;
        }
    }
}