namespace Behapy.Presentation.Extensions;

public static class StringExtension
{
    public static string ToTitle(this string str) => char.ToUpper(str[0]) + str[1..].ToLower();
}