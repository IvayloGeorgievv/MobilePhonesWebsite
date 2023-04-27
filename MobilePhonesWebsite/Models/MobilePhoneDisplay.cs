using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MobilePhonesWebsite.Models
{
    [Table("MobilePhoneDisplay")]
    public class MobilePhoneDisplay
    {
        [Key]
        public int Id { get; set; }
        public double DisplaySize { get; set; }
        public string Technology { get; set; }
        public string Resolution { get; set; }
    }
}
