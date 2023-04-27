using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MobilePhonesWebsite.Models
{
    [Table("Protector")]
    public class PhoneProtector : Accessory
    {
        [Required]
        public string FitFor { get; set; }
    }
}
