using MobilePhonesWebsite.ViewModels.SharedVM;
using static MobilePhonesWebsite.Enumerators.SharedEnum;

namespace MobilePhonesWebsite.ViewModels.PhoneCaseVM
{
    public class CreatePhoneCaseVM : CreateAccessoryVM
    {
        public Colour Colour { get; set; }
        public string FitFor { get; set; }
    }
}
