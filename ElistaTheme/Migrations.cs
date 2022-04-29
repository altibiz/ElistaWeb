using OrchardCore.ContentManagement.Metadata;
using OrchardCore.Data.Migration;
using OrchardCore.Recipes.Services;
using System.Threading.Tasks;

namespace OrchardCore.Themes.ElistaTheme
{
    public class Migrations : DataMigration
    {
        private readonly IRecipeMigrator _recipeMigrator;
        private readonly IContentDefinitionManager _contentDefinitionManager;
        public Migrations(IRecipeMigrator recipeMigrator, IContentDefinitionManager cdf)
        {
            _recipeMigrator = recipeMigrator;
            _contentDefinitionManager = cdf;
        }


    }
}