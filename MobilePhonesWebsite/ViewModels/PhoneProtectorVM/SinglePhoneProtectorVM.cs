using static MobilePhonesWebsite.Enumerators.SharedEnum;

namespace MobilePhonesWebsite.ViewModels.PhoneProtectorVM
{
    public class SinglePhoneProtectorVM
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string FitFor { get; set; }
		public double Price { get; set; }
    }
}
