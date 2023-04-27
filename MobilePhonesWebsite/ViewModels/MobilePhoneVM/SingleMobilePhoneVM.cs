using MobilePhonesWebsite.Models;

namespace MobilePhonesWebsite.ViewModels.MobilePhoneVM
{
    public class SingleMobilePhoneVM
    {
        public MobilePhone phone { get; set; }
        public MobilePhoneCamera camera { get; set; }
        public MobilePhoneDisplay display { get; set; }
        public MobilePhoneProcessor processor { get; set; }
    }
}
