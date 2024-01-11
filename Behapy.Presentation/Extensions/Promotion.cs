using Behapy.Presentation.Enums;
using Behapy.Presentation.Models;

namespace Behapy.Presentation.Extensions;

public static class PromotionExtension
{
    public static decimal CalculateValue(this Promotion promotion, decimal total, List<CartItem> items)
    {
        var promotionValue = (promotion.Type, promotion.Unit) switch
        {
            (PromotionType.Order, PromotionUnit.Percentage) => Math.Min(
                promotion.MaxDiscount ?? decimal.MaxValue,
                total.Percentage(promotion.Value)),
            (PromotionType.Order, PromotionUnit.Value) => promotion.Value,
            (PromotionType.Product, PromotionUnit.Percentage) => Math.Min(
                promotion.MaxDiscount ?? decimal.MaxValue,
                items.Where(ci => promotion.ProductPromotions
                        .Select(pp => pp.ProductId)
                        .Contains(ci.ProductId))
                    .Sum(ci => ci.Amount * ci.Product.Price)
                    .Percentage(promotion.Value)),
            (PromotionType.Product, PromotionUnit.Value) => Math.Min(
                promotion.MaxDiscount ?? decimal.MaxValue,
                items.Where(ci => promotion.ProductPromotions
                        .Select(pp => pp.ProductId)
                        .Contains(ci.ProductId))
                    .Sum(ci => (decimal)ci.Amount) * promotion.Value),
            _ => 0
        };

        return promotionValue;
    }
}