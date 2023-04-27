using System.ComponentModel.DataAnnotations.Schema;

namespace MobilePhonesWebsite.Models
{
	[Table("Liked")]
	public class LikedProduct
	{
		public int Id { get; set; }
		public string ProductType { get; set; }
		public int ProductId { get; set; }
		public int UserId { get; set; }
		public string ProductName { get; set; }
		public string Image { get; set; }
		public double Price { get; set; }
	}
}
