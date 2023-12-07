using Behapy.Presentation.Models;

namespace Behapy.Presentation.Extensions;

public static class OrderExtension
{
    public static string Orderer(this Order? order) =>
        order?.Customer != null
            ? order.Customer.User.FullName
            : order?.Distributor != null
                ? order.Distributor.FullName
                : "";
}