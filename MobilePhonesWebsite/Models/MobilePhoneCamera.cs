using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MobilePhonesWebsite.Models
{
    [Table("MobilePhoneCamera")]
    public class MobilePhoneCamera
    {
        [Key]
        public int Id { get; set; }
        public string MainRearCamera { get; set; }
        public string RearCamera { get; set; }
        public string FrontCamera { get; set; }
    }
}
