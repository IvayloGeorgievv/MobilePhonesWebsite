using Microsoft.AspNetCore.Mvc;
using MobilePhonesWebsite.ViewModels.SharedVM;
using static MobilePhonesWebsite.Enumerators.HeadphonesEnum;
using static MobilePhonesWebsite.Enumerators.SharedEnum;

namespace MobilePhonesWebsite.ViewModels.HeadphonesVM
{
    public class CreateWirelessHeadphonesVM : CreateAccessoryVM
    {
        public string Model { get; set; }
        public HeadphonesType Type { get; set; }
        public Colour Colour { get; set; }
        public double BatteryLife { get; set; }
        public double BatteryLifeWithCase { get; set; }
    }
}
