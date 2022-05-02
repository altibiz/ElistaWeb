using System;

namespace Carousel.OrchardCore.Base.CodeGeneration
{
    public class ContentTypeMigrationsViewModel
    {
        internal Lazy<string> MigrationCodeLazy { get; set; }
        public string MigrationCode => MigrationCodeLazy.Value;
        internal Lazy<string> ModelCodeLazy { get; set; }
        public string ModelCode => ModelCodeLazy.Value;
    }
}
