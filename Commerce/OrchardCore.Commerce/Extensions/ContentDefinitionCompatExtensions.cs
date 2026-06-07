using System.Reflection;

namespace OrchardCore.ContentManagement.Metadata.Models
{
    /// <summary>
    /// Compatibility shim for the OrchardCore 1.x <c>ContentPartFieldDefinition.PopulateSettings&lt;T&gt;()</c>
    /// API, which was removed in OrchardCore 2.x in favour of <c>GetSettings&lt;T&gt;()</c> (which returns a new
    /// instance rather than populating an existing one). This vendored Commerce fork populates shape view models
    /// in place, so we re-create the in-place behaviour on top of <c>GetSettings&lt;T&gt;()</c>.
    /// </summary>
    public static class ContentDefinitionCompatExtensions
    {
        public static void PopulateSettings<TSettings>(this ContentPartFieldDefinition definition, TSettings target)
            where TSettings : class, new()
        {
            var source = definition.GetSettings<TSettings>();

            foreach (var property in typeof(TSettings).GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (property.CanRead && property.CanWrite)
                {
                    property.SetValue(target, property.GetValue(source));
                }
            }
        }
    }
}
