using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static MobilePhonesWebsite.Enumerators.MobilePhoneEnum;
using static MobilePhonesWebsite.Enumerators.SharedEnum;

namespace MobilePhonesWebsite.Models
{
    [Table("MobilePhones")]
    public class MobilePhone
    {
        [Key]
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Size { get; set; }
        public int BatteryLife { get; set; }
        public double Weight  { get; set; }
        public Colour Colour { get; set; }
        public SIMCardType SIMCard { get; set; }
        public int MobilePhoneCameraId { get; set; }
        public int MobilePhoneDisplayId { get; set; }
        public int MobilePhoneProcessorId { get; set; }
        public int AdditionalFunctions { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public int OperatingMemory { get; set; }
        public double StorageSpace { get; set; }
        public DateTime DateAdded { get; set; }
        public double Price { get; set; }
    }
}
