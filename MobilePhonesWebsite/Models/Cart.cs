using System.ComponentModel.DataAnnotations.Schema;

namespace MobilePhonesWebsite.Models
{

    [Table("Cart")]
    public class Cart
    {
        public int Id { get; set; }
        public string ProductType { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public string Image { get; set; }
        public double PriceForOne { get; set; }
        public double Price { get; set; }
    }
}
