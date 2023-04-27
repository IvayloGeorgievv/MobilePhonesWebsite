using static MobilePhonesWebsite.Enumerators.SharedEnum;

namespace MobilePhonesWebsite.ViewModels.PhoneCaseVM
{
    public class SinglePhoneCaseVM
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string FitFor { get; set; }
		public Colour Colour { get; set; }
		public double Price { get; set; }
    }
}
