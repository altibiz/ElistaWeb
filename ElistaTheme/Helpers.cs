using OrchardCore.ContentManagement;

namespace ElistaTheme;

public static class Helpers
{
    private static string[] colors = new[] { "DFFEDF", "F8C1BE", "C3E3E0", "F5D9D8", "DBEDF7", "F7EFCB", "E3E4F9", "FCFAEB", "CADCF2" };

    private static int ndx = 0;
    public static string Pop()
    {
        return "#"+colors[ndx++ % colors.Length];
    }

    public static string GetText(this ContentItem contentItem, string field)
    {
        if (contentItem.ContentType == null) return null;
        var part = contentItem.Content[contentItem.ContentType];
        if (part != null)
        {
            var fieldElement = part[field];
            if (fieldElement != null)
            {
                return fieldElement.Text;
            }
        }
        return null;
    }

}
