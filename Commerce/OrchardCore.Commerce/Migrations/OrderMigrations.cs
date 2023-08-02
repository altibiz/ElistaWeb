using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.Data.Migration;
using OrchardCore.Commerce.Fields;
using OrchardCore.Commerce.Settings;
using OrchardCore.ContentFields.Settings;
using OrchardCore.Title.Models;
using OrchardCore.Taxonomies.Settings;
using OrchardCore.Recipes.Services;
using System.Threading.Tasks;

namespace OrchardCore.Commerce.Migrations
{
    /// <summary>
    /// Adds the order part to the list of available parts and the address field to the list of available fields.
    /// </summary>
    public class OrderMigrations : DataMigration
    {
        IContentDefinitionManager _contentDefinitionManager;
        private IRecipeMigrator _recipeMigrator;

        public OrderMigrations(IContentDefinitionManager contentDefinitionManager, IRecipeMigrator recipeMigrator)
        {
            _contentDefinitionManager = contentDefinitionManager;
            _recipeMigrator = recipeMigrator;
        }

        public int Create()
        {
            _contentDefinitionManager.AlterPartDefinition("OrderPart", builder => builder
                .Attachable()
                .WithDescription("Makes a content item into an order."));

            _contentDefinitionManager.MigrateFieldSettings<AddressField, AddressPartFieldSettings>();
            _contentDefinitionManager.AlterTypeDefinition("Order", type => type
                .DisplayedAs("Order")
                .Creatable()
                .Listable()
                .Draftable()
                .Versionable()
                .Securable()
                .WithPart("TitlePart", part => part
                    .WithPosition("0")
                    .WithSettings(new TitlePartSettings
                    {
                        Options = TitlePartOptions.GeneratedDisabled,
                        Pattern = "{{ ContentItem.Content.Order.BillingAddress.Address.Name  }} | {{ ContentItem.Content.Order.OrderId.Text }}",
                    })
                )
                .WithPart("Annotations", part => part
                    .WithPosition("1")
                    .WithEditor("Wysiwyg")
                )
                .WithPart("Order", part => part
                    .WithPosition("2")
                )
                .WithPart("OrderPart", part => part
                    .WithPosition("3")
                )
            );

            _contentDefinitionManager.AlterPartDefinition("Order", part => part
                .WithField("OrderId", field => field
                    .OfType("TextField")
                    .WithDisplayName("Order Id")
                    .WithPosition("0")
                )
                .WithField("ShippingAddress", field => field
                    .OfType("AddressField")
                    .WithDisplayName("Shipping Address")
                    .WithPosition("4")
                    .WithSettings(new AddressPartFieldSettings
                    {
                        Hint = "The address where the order should be shipped.",
                    })
                )
                .WithField("BillingAddress", field => field
                    .OfType("AddressField")
                    .WithDisplayName("Billing Address")
                    .WithPosition("3")
                    .WithSettings(new AddressPartFieldSettings
                    {
                        Hint = "The address of the party that should be billed for this order.",
                    })
                )
                .WithField("Email", field => field
                    .OfType("TextField")
                    .WithDisplayName("Email")
                    .WithEditor("Email")
                    .WithPosition("1")
                    .WithSettings(new TextFieldSettings
                    {
                        Required = true,
                    })
                )
                .WithField("Phone", field => field
                    .OfType("TextField")
                    .WithDisplayName("Phone")
                    .WithEditor("Tel")
                    .WithPosition("2")
                )
            );
            return 1;
        }

        public async Task<int> UpdateFrom1()
        {
            _contentDefinitionManager.AlterPartDefinition("Order", part => part
                    .WithField("Status", field => field
                            .OfType("TaxonomyField")
                            .WithDisplayName("Status")
                            .WithEditor("Tags")
                            .WithDisplayMode("Tags")
                            .WithPosition("1")
                            .WithSettings(new TaxonomyFieldSettings
                            {
                                TaxonomyContentItemId = "4jv2vjnd353k94wdw3y3c6w3m0",
                                Unique = true,
                            })
                            .WithSettings(new TaxonomyFieldTagsEditorSettings
                            {
                                Open = false,
                            })
                        )
                    );
            await _recipeMigrator.ExecuteAsync("order-status.recipe.json", this);
            return 4; //skip next 2 since it's just add and delete
        }

        public int UpdateFrom2()
        {
            _contentDefinitionManager.AlterPartDefinition("Order", part => part
                .WithField("NoShipping", field => field
                    .OfType("BooleanField")
                    .WithDisplayName("No Shipping")
                    .WithEditor("Switch")
                    .WithPosition("6")
                )

            );
            return 3;
        }

        public int UpdateFrom3()
        {
            _contentDefinitionManager.AlterPartDefinition("Order", part => part
                .RemoveField("NoShipping"));
            return 4;
        }

    }
}