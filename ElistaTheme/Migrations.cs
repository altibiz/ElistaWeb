using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Data.Migration;

namespace ElistaTheme;
internal class Migrations : DataMigration
{
    private readonly IContentDefinitionManager _contentDefinitionManager;

    public Migrations(IContentDefinitionManager contentDefinitionManager)
    {
        _contentDefinitionManager = contentDefinitionManager;
    }
    public int Create()
    {
        _contentDefinitionManager.AlterTypeDefinition("GeneralSettings", type => type
            .DisplayedAs("General Settings")
            .Stereotype("CustomSettings")
            .WithPart("GeneralSettings", part => part
                .WithPosition("0")
            )
        );

        _contentDefinitionManager.AlterPartDefinition("GeneralSettings", part => part
            .WithField("Email", field => field
                .OfType("TextField")
                .WithDisplayName("Email")
                .WithEditor("Email")
                .WithPosition("0")
            )
            .WithField("Phone", field => field
                .OfType("TextField")
                .WithDisplayName("Phone")
                .WithEditor("Tel")
                .WithPosition("1")
            )
            .WithField("Phone", field => field
                .OfType("TextField")
                .WithDisplayName("Phone")
                .WithEditor("Tel")
                .WithPosition("1")
            )
            .WithField("ExchangeRate", field => field.OfType("NumericField")
                         .WithDisplayName("Conversion Kn")
            )
        );
        return 1;
    }
}
