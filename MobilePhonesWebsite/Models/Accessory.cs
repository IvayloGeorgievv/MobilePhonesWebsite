using System.ComponentModel.DataAnnotations;
using static MobilePhonesWebsite.Enumerators.SharedEnum;

namespace MobilePhonesWebsite.Models
{
    public class Accessory
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Brand { get; set; }
        public string Image { get; set; }
        public DateTime DateAdded { get; set; }
        public double Price { get; set; }
    }
}
