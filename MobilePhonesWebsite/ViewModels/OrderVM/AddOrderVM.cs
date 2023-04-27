using static MobilePhonesWebsite.Enumerators.OrderEnum;

namespace MobilePhonesWebsite.ViewModels.OrderVM
{
    public class AddOrderVM
    {
        public int UserId { get; set; }
        public string FirstAndLastName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOrdered { get; set; }
        public string ProductsId { get; set; }
        public string ProductsTypes { get; set; }
        public string ProductsQuantities { get; set; }
        public OrderStatusEnum OrderStatus { get; set; }
        public string ShippingCity { get; set; }
        public string ShippingAddress { get; set; }
        public ShippingMethod ShippingMethod { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public double OrderTotalPrice { get; set; }
    }
}
