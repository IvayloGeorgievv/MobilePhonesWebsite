using MobilePhonesWebsite.Models;
using System.Data.Entity;

namespace MobilePhonesWebsite.Data
{
	public class ApplicationDbContext : DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<MobilePhone> MobilePhones { get; set; }
		public DbSet<MobilePhoneProcessor> MobilePhoneProcessors { get; set; }
		public DbSet<MobilePhoneDisplay> MobilePhoneDisplays { get; set; }
		public DbSet<MobilePhoneCamera> MobilePhoneCameras { get; set; }
		public DbSet<MobilePhoneAdditionalFunction> MobilePhoneAdditionalFunctions { get; set; }
		public DbSet<PhoneCase> PhoneCases { get; set; }
		public DbSet<PhoneProtector> PhoneProtectors { get; set; }
		public DbSet<WiredHeadphones> WiredHeadphones { get; set; }
		public DbSet<WirelessHeadphones> WirelessHeadphones { get; set; }
		public DbSet<Smartwatch> Smartwatches { get; set; }
		public DbSet<Cart> Cart { get; set; }
		public DbSet<LikedProduct> LikedProducts { get; set; }
		public DbSet<Order> Orders { get; set; }
		public ApplicationDbContext() : base("Data Source=SQL8005.site4now.net;Initial Catalog=db_a984be_mobilephones;User Id=db_a984be_mobilephones_admin;Password=MobilePhones1")

        {
			Users = this.Set<User>();
			MobilePhones = this.Set<MobilePhone>();
			MobilePhoneCameras = this.Set<MobilePhoneCamera>();
			MobilePhoneAdditionalFunctions = this.Set<MobilePhoneAdditionalFunction>();
			PhoneCases = this.Set<PhoneCase>();
			PhoneProtectors = this.Set<PhoneProtector>();
			WiredHeadphones = this.Set<WiredHeadphones>();
			WirelessHeadphones= this.Set<WirelessHeadphones>();
			Smartwatches = this.Set<Smartwatch>();
			Cart = this.Set<Cart>();
			LikedProducts = this.Set<LikedProduct>();
			Orders = this.Set<Order>();
		}
	}
}
