namespace Behapy.Presentation.Constants;

public abstract class OrderStatusConstant
{
    public const string NeedToConfirm = "Cần xác nhận";
    public const string Confirmed = "Đã xác nhận";
    public const string Denied = "Đã từ chối";
    public const string Delivering = "Đang vận chuyển";
    public const string DeliverFailed = "Giao hàng thất bại";
    public const string Delivered = "Đã nhận hàng";

    private static List<string> AllStatuses => new()
        { NeedToConfirm, Confirmed, Denied, Delivering, DeliverFailed, Delivered };

    public static bool ValidStatus(string status) => AllStatuses.Contains(status);
}