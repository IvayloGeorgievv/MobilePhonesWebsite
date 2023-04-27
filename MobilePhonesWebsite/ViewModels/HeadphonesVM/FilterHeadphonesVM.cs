using MobilePhonesWebsite.Models;
using System.Linq.Expressions;

namespace MobilePhonesWebsite.ViewModels.HeadphonesVM
{
    public class FilterHeadphonesVM
    {
        public List<string> Brand { get; set; }
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }

        public Expression<Func<WiredHeadphones, bool>> GetFilter()
        {
            var brand1 = Brand?.ElementAtOrDefault(0)?.ToLower();
            var brand2 = Brand?.ElementAtOrDefault(1)?.ToLower();
            var brand3 = Brand?.ElementAtOrDefault(2)?.ToLower();
            var brand4 = Brand?.ElementAtOrDefault(3)?.ToLower();

            return i => (string.IsNullOrEmpty(brand1) || i.Brand.ToLower().Contains(brand1)) &&
                        (string.IsNullOrEmpty(brand2) || i.Brand.ToLower().Contains(brand2)) &&
                        (string.IsNullOrEmpty(brand3) || i.Brand.ToLower().Contains(brand3)) &&
                        (string.IsNullOrEmpty(brand4) || i.Brand.ToLower().Contains(brand4)) &&
                        (MinPrice == 0 || i.Price >= MinPrice) &&
                        (MaxPrice == 0 || i.Price <= MaxPrice);
        }
        public Expression<Func<WirelessHeadphones, bool>> GetFilterForWireless()
        {
            var brand1 = Brand?.ElementAtOrDefault(0)?.ToLower();
            var brand2 = Brand?.ElementAtOrDefault(1)?.ToLower();
            var brand3 = Brand?.ElementAtOrDefault(2)?.ToLower();
            var brand4 = Brand?.ElementAtOrDefault(3)?.ToLower();

            return i => (string.IsNullOrEmpty(brand1) || i.Brand.ToLower().Contains(brand1)) &&
                        (string.IsNullOrEmpty(brand2) || i.Brand.ToLower().Contains(brand2)) &&
                        (string.IsNullOrEmpty(brand3) || i.Brand.ToLower().Contains(brand3)) &&
                        (string.IsNullOrEmpty(brand4) || i.Brand.ToLower().Contains(brand4)) &&
                        (MinPrice == 0 || i.Price >= MinPrice) &&
                        (MaxPrice == 0 || i.Price <= MaxPrice);
        }
    }
}
