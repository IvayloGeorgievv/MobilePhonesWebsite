using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static MobilePhonesWebsite.Enumerators.HeadphonesEnum;
using static MobilePhonesWebsite.Enumerators.SharedEnum;

namespace MobilePhonesWebsite.Models
{
    [Table("WiredHeadphones")]
    public class WiredHeadphones : Accessory
    {
        public string Model { get; set; }
        public string Connectivity { get; set; }
        public HeadphonesType Type { get; set; }
        public Colour Colour { get; set; }
    }
}
