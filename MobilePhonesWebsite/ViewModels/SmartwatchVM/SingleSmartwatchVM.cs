using static MobilePhonesWebsite.Enumerators.SharedEnum;

namespace MobilePhonesWebsite.ViewModels.SmartwatchVM
{
    public class SingleSmartwatchVM
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public double DisplaySize { get; set; }
        public string DisplayTechnology { get; set; }
        public double Weight { get; set; }
        public double BatteryLife { get; set; }
        public double BatteryLifeWithCase { get; set; }
        public Colour Colour { get; set; }
        public double Price { get; set; }
    }
}
