using MobilePhonesWebsite.Models;
using System.Linq.Expressions;

namespace MobilePhonesWebsite.ViewModels.MobilePhoneVM
{
    public class FilterMobilePhoneVM
    {
        public List<string> Brand { get; set; }
        public List<int> StorageSpace { get; set; }
        public List<int> OperatingMemory { get; set; }
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }

        public Expression<Func<MobilePhone, bool>> GetFilter()
        {

            var brand1 = Brand?.ElementAtOrDefault(0)?.ToLower();
            var brand2 = Brand?.ElementAtOrDefault(1)?.ToLower();
            var brand3 = Brand?.ElementAtOrDefault(2)?.ToLower();
            var brand4 = Brand?.ElementAtOrDefault(3)?.ToLower();

            var storageSpace1 = StorageSpace?.ElementAtOrDefault(0) ?? 0;
            var storageSpace2 = StorageSpace?.ElementAtOrDefault(1) ?? 0;
            var storageSpace3 = StorageSpace?.ElementAtOrDefault(2) ?? 0;
            var storageSpace4 = StorageSpace?.ElementAtOrDefault(3) ?? 0;

            var operatingMemory1 = OperatingMemory?.ElementAtOrDefault(0) ?? 0;
            var operatingMemory2 = OperatingMemory?.ElementAtOrDefault(1) ?? 0;
            var operatingMemory3 = OperatingMemory?.ElementAtOrDefault(2) ?? 0;
            var operatingMemory4 = OperatingMemory?.ElementAtOrDefault(3) ?? 0;

            return i => (string.IsNullOrEmpty(brand1) || i.Brand.ToLower().Contains(brand1)) &&
                        (string.IsNullOrEmpty(brand2) || i.Brand.ToLower().Contains(brand2)) &&
                        (string.IsNullOrEmpty(brand3) || i.Brand.ToLower().Contains(brand3)) &&
                        (string.IsNullOrEmpty(brand4) || i.Brand.ToLower().Contains(brand4)) &&

                        (storageSpace1 == 0 || i.StorageSpace == storageSpace1) &&
                        (storageSpace2 == 0 || i.StorageSpace == storageSpace2) &&
                        (storageSpace3 == 0 || i.StorageSpace == storageSpace3) &&
                        (storageSpace4 == 0 || i.StorageSpace == storageSpace4) &&

                        (operatingMemory1 == 0 || i.OperatingMemory == operatingMemory1) &&
                        (operatingMemory2 == 0 || i.OperatingMemory == operatingMemory2) &&
                        (operatingMemory3 == 0 || i.OperatingMemory == operatingMemory3) &&
                        (operatingMemory4 == 0 || i.OperatingMemory == operatingMemory4) &&

                        (MinPrice == 0 || i.Price >= MinPrice) &&
                        (MaxPrice == 0 || i.Price <= MaxPrice);
        }
    }
}
