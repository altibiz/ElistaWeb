using OrchardCore.ContentFields.Settings;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Data.Migration;
using System.Threading.Tasks;

namespace ElistaTheme;
internal class Migrations : DataMigration
{
    private readonly IContentDefinitionManager _contentDefinitionManager;

    public Migrations(IContentDefinitionManager contentDefinitionManager)
    {
        _contentDefinitionManager = contentDefinitionManager;
    }
    public async Task<int> CreateAsync()
    {
        await _contentDefinitionManager.AlterTypeDefinitionAsync("GeneralSettings", type => type
            .DisplayedAs("General Settings")
            .Stereotype("CustomSettings")
            .WithPart("GeneralSettings", part => part
                .WithPosition("0")
            )
        );

        await _contentDefinitionManager.AlterPartDefinitionAsync("GeneralSettings", part => part
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
                         .WithSettings(new NumericFieldSettings { Scale = 2 })
            )
        );
        return 1;
    }
}
