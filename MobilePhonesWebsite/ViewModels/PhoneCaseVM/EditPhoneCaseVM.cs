using MobilePhonesWebsite.ViewModels.SharedVM;
using static MobilePhonesWebsite.Enumerators.SharedEnum;

namespace MobilePhonesWebsite.ViewModels.PhoneCaseVM
{
    public class EditPhoneCaseVM : EditAccessoryVM
    {
        public string FitFor { get; set; }
        public Colour Colour { get; set; }
    }
}
