using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MobilePhonesWebsite.Models
{
    [Table("Users")]
    public class User
	{
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
		public bool IsAdmin { get; set; }
	}
}
