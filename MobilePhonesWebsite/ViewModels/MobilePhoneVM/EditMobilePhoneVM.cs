using MobilePhonesWebsite.Models;
using MobilePhonesWebsite.ViewModels;
namespace MobilePhonesWebsite.ViewModels.MobilePhoneVM
{
    public class EditMobilePhoneVM
    {
        public MobilePhone MobilePhone { get; set; }
        public double PhoneHeight { get; set; }
        public double PhoneWidth { get; set; }
        public double PhoneThickness { get; set; }
        public MobilePhoneCamera MobilePhoneCamera { get; set; }
        public MobilePhoneDisplay MobilePhoneDisplay { get; set; }
        public MobilePhoneProcessor MobilePhoneProcessor { get; set; }
        public MobilePhoneAdditionalFunction MobilePhoneAdditionalFunctions { get; set; }
    }
}
