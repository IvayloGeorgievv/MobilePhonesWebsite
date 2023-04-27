using MobilePhonesWebsite.Models;
using static MobilePhonesWebsite.Enumerators.MobilePhoneEnum;

namespace MobilePhonesWebsite.ViewModels.MobilePhoneVM
{
    public class CreateMobilePhoneVM
    {
        public MobilePhone MobilePhone { get; set; }
        public double PhoneHeight { get; set; }
        public double PhoneWidth { get; set; }
        public double PhoneThickness { get; set; }
        public MobilePhoneCamera MobilePhoneCamera { get; set; }
        public MobilePhoneDisplay MobilePhoneDisplay { get; set; }
        public MobilePhoneProcessor MobilePhoneProcessor { get; set; }
        public MobilePhoneAdditionalFunction MobilePhoneAdditionalFunction { get; set; }
    }
}
