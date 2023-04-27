using MobilePhonesWebsite.Models;
using MobilePhonesWebsite.ViewModels.PhoneCaseVM;
using MobilePhonesWebsite.ViewModels.SharedVM;

namespace MobilePhonesWebsite.ViewModels.PhoneProtectorVM
{
    public class DisplayPhoneProtectorVM
    {
        public List<PhoneProtector> PhoneProtectors { get; set; }
        public FilterPhoneProtectorVM Filter { get; set; }
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
    }
}
