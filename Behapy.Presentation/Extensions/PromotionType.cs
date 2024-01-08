namespace Behapy.Presentation.Extensions;

public static class PromotionTypeExtension
{
    public static string GetName(this Enums.PromotionType unit)
    {
        return unit.GetName(false);
    }

    public static string GetName(this Enums.PromotionType unit, bool lowercase)
    {
        var map = new Dictionary<Enums.PromotionType, string>
        {
            { Enums.PromotionType.Order, "Đơn hàng" },
            { Enums.PromotionType.Product, "Sản phẩm" },
            { Enums.PromotionType.Category, "Danh mục" }
        };

        return lowercase ? map[unit].ToLower() : map[unit];
    }
}