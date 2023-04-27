using MobilePhonesWebsite.Models;
using System.Linq.Expressions;

namespace MobilePhonesWebsite.ViewModels.PhoneProtectorVM
{
    public class FilterPhoneProtectorVM
    {
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }

        public Expression<Func<PhoneProtector, bool>> GetFilter()
        {
            return i => (MinPrice == 0 || i.Price >= MinPrice) &&
                        (MaxPrice == 0 || i.Price <= MaxPrice);
        }
    }
}
