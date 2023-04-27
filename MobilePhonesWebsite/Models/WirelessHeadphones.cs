using System.ComponentModel.DataAnnotations.Schema;
using static MobilePhonesWebsite.Enumerators.HeadphonesEnum;
using static MobilePhonesWebsite.Enumerators.SharedEnum;

namespace MobilePhonesWebsite.Models
{
    [Table("WirelessHeadphones")]
    public class WirelessHeadphones : Accessory
    {
        public string Model { get; set; }
        public string Connectivity { get; set; }
        public HeadphonesType Type { get; set; }
        public double BatteryLife { get; set; }
        public double BatteryLifeWithCase { get; set; }
        public Colour Colour { get; set; }
    }
}
