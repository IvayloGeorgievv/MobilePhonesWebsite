using MobilePhonesWebsite.Models;
using System.Linq.Expressions;

namespace MobilePhonesWebsite.ViewModels.PhoneCaseVM
{
    public class FilterPhoneCaseVM
    {
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }

        public Expression<Func<PhoneCase, bool>> GetFilter()
        {
            return i => (MinPrice == 0 || i.Price >= MinPrice) &&
                        (MaxPrice == 0 || i.Price <= MaxPrice);
        }
    }
}
