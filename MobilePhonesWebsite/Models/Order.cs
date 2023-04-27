using static MobilePhonesWebsite.Enumerators.OrderEnum;

namespace MobilePhonesWebsite.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int OrderNum { get; set; }
        public string FirstAndLastName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOrdered { get; set; }
        public string ProductsId { get; set; }
        public string ProductsTypes { get; set; }
        public string ProductsQuantities { get; set; }
        public OrderStatusEnum OrderStatus { get; set; }
        public string ShippingAddress { get; set; }
        public ShippingMethod ShippingMethod { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public double OrderTotalPrice { get; set; }
    }
}
