using MobilePhonesWebsite.Models;
using static MobilePhonesWebsite.Enumerators.OrderEnum;

namespace MobilePhonesWebsite.ViewModels.OrderVM
{
    public class CheckOrderVM
    {
        public List<Cart> OrderItems { get; set; }
        public int OrderId { get; set; }
        public int OrderNumber { get; set; }
        public OrderStatusEnum OrderStatus { get; set; }
        public double OrderPrice { get; set; }
    }
}
