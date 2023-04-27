using static MobilePhonesWebsite.Enumerators.OrderEnum;

namespace MobilePhonesWebsite.ViewModels.OrderVM
{
    public class ChoosePaymentAndShippingMethodVM
    {
        public PaymentMethod PaymentMethod { get; set; }
        public ShippingMethod ShippingMethod { get; set; }
    }
}
