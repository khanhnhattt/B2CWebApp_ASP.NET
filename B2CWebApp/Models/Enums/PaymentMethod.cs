namespace B2CWebApp.Models.Enums
{
    public enum PaymentMethod
    {
        VISA,
        INTERNET_BANKING,
        COD
    }

    public enum OrderStatus
    {
        UNCONFIRMED,
        IN_PROCESS,
        DELIVERY,
        DONE,
        CANCELLED
    }

    public enum PaymentStatus
    {
        PAID,
        UNPAID
    }

    public enum ShippingStatus
    {
        PROCESSING,
        DELIVERING,
        DELIVERED,
        CANCELLED
    }
}
