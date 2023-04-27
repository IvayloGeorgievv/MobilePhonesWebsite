using static MobilePhonesWebsite.Enumerators.OrderEnum;

namespace MobilePhonesWebsite.ViewModels.OrderVM
{
    public class EditOrderVM
    {
        public int Id { get; set; }
        public OrderStatusEnum OrderStatus { get; set; }
    }
}
