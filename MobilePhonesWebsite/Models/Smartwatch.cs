using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static MobilePhonesWebsite.Enumerators.SharedEnum;

namespace MobilePhonesWebsite.Models
{
    [Table("Smartwatch")]
    public class Smartwatch : Accessory
    {
        public string Model { get; set; }
        public Colour Colour { get; set; }
        public double DisplaySize { get; set; }
        public string DisplayTechnology { get; set; }
        public double BatteryLife { get; set; }
        public double Weight { get; set; }
    }
}
