namespace Behapy.Presentation.Extensions;

public static class PromotionUnitExtension
{
    public static string GetName(this Enums.PromotionUnit unit)
    {
        var map = new Dictionary<Enums.PromotionUnit, string>
        {
            { Enums.PromotionUnit.Percentage, "%" },
            { Enums.PromotionUnit.Value, "VNĐ" }
        };

        return map[unit];
    }
}