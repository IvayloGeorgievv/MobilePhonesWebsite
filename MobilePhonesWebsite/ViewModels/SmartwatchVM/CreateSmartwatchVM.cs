using MobilePhonesWebsite.ViewModels.SharedVM;
using static MobilePhonesWebsite.Enumerators.SharedEnum;

namespace MobilePhonesWebsite.ViewModels.SmartwatchVM
{
    public class CreateSmartwatchVM : CreateAccessoryVM
    {
        public string Model { get; set; }
        public Colour Colour { get; set; }
        public double DisplaySize { get; set; }
        public string DisplayTechnology { get; set; }
        public double Weight { get; set; }
        public double BatteryLife { get; set; }
    }
}
