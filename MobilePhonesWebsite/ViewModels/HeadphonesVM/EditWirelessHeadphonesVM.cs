using MobilePhonesWebsite.ViewModels.SharedVM;
using static MobilePhonesWebsite.Enumerators.HeadphonesEnum;
using static MobilePhonesWebsite.Enumerators.SharedEnum;

namespace MobilePhonesWebsite.ViewModels.HeadphonesVM
{
    public class EditWirelessHeadphonesVM : EditAccessoryVM
    {
        public string Model { get; set; }
        public HeadphonesType Type { get; set; }
        public double BatteryLife { get; set; }
        public double BatteryLifeWithCase { get; set; }
        public Colour Colour { get; set; }
    }
}
