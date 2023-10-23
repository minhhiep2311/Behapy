using System.Globalization;

namespace Behapy.Presentation.Extensions;

public static class DecimalExtension
{
    public static string ToMoney(this decimal number) =>
        number.ToString("C", CultureInfo.CreateSpecificCulture("vi-VN"));
}