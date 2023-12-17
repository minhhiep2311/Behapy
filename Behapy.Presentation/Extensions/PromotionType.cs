namespace Behapy.Presentation.Extensions;

public static class PromotionTypeExtension
{
    public static string GetName(this Enums.PromotionType unit)
    {
        var map = new Dictionary<Enums.PromotionType, string>
        {
            { Enums.PromotionType.Order, "Đơn hàng" },
            { Enums.PromotionType.Product, "Sản phẩm" }
        };

        return map[unit];
    }
}