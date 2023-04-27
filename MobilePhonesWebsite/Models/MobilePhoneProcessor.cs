using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static MobilePhonesWebsite.Enumerators.MobilePhoneEnum;

namespace MobilePhonesWebsite.Models
{
    [Table("MobilePhoneProcessor")]
    public class MobilePhoneProcessor
    {
        [Key]
        public int Id { get; set; }
        public CPUType ProcessorType { get; set; }
        public string ProcessorFrequency { get; set; }
    }
}
