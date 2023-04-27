using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static MobilePhonesWebsite.Enumerators.SharedEnum;

namespace MobilePhonesWebsite.Models
{
    [Table("Case")]
    public class PhoneCase : Accessory
    {
        [Required]
        public Colour Colour { get; set; }
        public string FitFor { get; set; }
    }
}
