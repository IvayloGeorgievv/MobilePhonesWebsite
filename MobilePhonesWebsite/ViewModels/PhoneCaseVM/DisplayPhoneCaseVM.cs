using MobilePhonesWebsite.Models;
using MobilePhonesWebsite.ViewModels.SharedVM;

namespace MobilePhonesWebsite.ViewModels.PhoneCaseVM
{
    public class DisplayPhoneCaseVM
    {
        public List<PhoneCase> PhoneCases { get; set; }
        public FilterPhoneCaseVM Filter { get; set; }
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
    }
}
