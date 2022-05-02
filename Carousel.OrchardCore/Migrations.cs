using OrchardCore.ContentFields.Settings;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Data.Migration;
using OrchardCore.Flows.Models;
using OrchardCore.Media.Settings;
using OrchardCore.Recipes.Services;
using System.Threading.Tasks;

namespace Carousel.OrchardCore {
    public class Migrations : DataMigration {

        private readonly IContentDefinitionManager _contentDefinitionManager;
        private readonly IRecipeMigrator _recipeMigrator;

        public Migrations(IContentDefinitionManager contentDefinitionManager, IRecipeMigrator recipeMigrator) 
        {

            _contentDefinitionManager = contentDefinitionManager;
            _recipeMigrator = recipeMigrator;
        }

        public int Create() {
            _contentDefinitionManager.AlterPartDefinition("Slide", cfg => cfg
                .WithDescription("Contains the fields for the current type")
                .WithField("Image",
                    fieldBuilder => fieldBuilder
                        .OfType("MediaField")
                        .WithDisplayName("Image")
                        .WithSettings(new MediaFieldSettings { Required = true, Multiple = false}))
                .WithField("SubTitle",
                    fieldBuilder => fieldBuilder
                        .OfType("TextField")
                        .WithDisplayName("SubTitle (up)"))
                .WithField("Title",
                    fieldBuilder => fieldBuilder
                        .OfType("TextField")
                        .WithDisplayName("Title"))
                .WithField("ShortDescription",
                    fieldBuilder => fieldBuilder
                        .OfType("TextField")
                        .WithDisplayName("Short Description"))
                .WithField("ImageClass",
                    fieldBuilder => fieldBuilder
                        .OfType("TextField")
                        .WithDisplayName("Image Class"))
                .WithField("ImageAltText",
                    fieldBuilder => fieldBuilder
                        .OfType("TextField")
                        .WithDisplayName("Image Alt Text"))
                .WithField("BtnText",
                    fieldBuilder => fieldBuilder
                        .OfType("TextField")
                        .WithDisplayName("Button display text"))
                .WithField("BtnUrl",
                    fieldBuilder => fieldBuilder
                        .OfType("TextField")
                        .WithDisplayName("Button click destination"))
            );

            _contentDefinitionManager.AlterTypeDefinition("Slide", type => type
                .WithPart("Slide"));

            _contentDefinitionManager.AlterPartDefinition("Carousel", cfg => cfg
                .WithDescription("Contains the fields for the current type")
                .WithField("Interval",
                    fieldBuilder => fieldBuilder
                        .OfType("NumericField")
                        .WithDisplayName("Interval")
                        .WithSettings(new NumericFieldSettings { Required = true, DefaultValue = "5000", Hint = "Delay between slides (ms)" }))
                .WithField("Pause",
                    fieldBuilder => fieldBuilder
                        .OfType("BooleanField")
                        .WithDisplayName("Pause on hover/touch"))
            );

            _contentDefinitionManager.AlterTypeDefinition("Carousel", type => type
                .WithPart("Carousel")
                .WithPart("Slides", "BagPart", cfg => cfg
                    .WithDisplayName("Slides")
                    .WithDescription("Slides to display in the carousel.")
                    .WithSettings(new BagPartSettings { ContainedContentTypes = new[] { "Slide" }, DisplayType = "Detail" }))
                .Stereotype("Widget"));

            return 1;
        }

        //public async Task<int> UpdateFrom1Async()
        //{
        //    await _recipeMigrator.ExecuteAsync("county.recipe.json", this);

        //    return 2;
        //}
    }
}