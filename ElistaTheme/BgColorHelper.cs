namespace ElistaTheme;

public static class BgColorHelper
{
    private static string[] colors = new[] { "DFFEDF", "F8C1BE", "C3E3E0", "F5D9D8", "DBEDF7", "F7EFCB", "E3E4F9", "FCFAEB", "CADCF2" };

    private static int ndx = 0;
    public static string Pop()
    {
        return "#"+colors[ndx++ % colors.Length];
    }

}
