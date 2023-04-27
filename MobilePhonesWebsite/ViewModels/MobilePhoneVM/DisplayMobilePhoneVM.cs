using MobilePhonesWebsite.Models;
using MobilePhonesWebsite.ViewModels.SharedVM;

namespace MobilePhonesWebsite.ViewModels.MobilePhoneVM
{
    public class DisplayMobilePhoneVM
    {
        public List<MobilePhone> MobilePhones { get; set; }
        public FilterMobilePhoneVM Filter { get; set; }
        public bool Brand1 { get; set; }
        public bool Brand2 { get; set;}
        public bool Brand3 { get; set;}
        public bool Brand4 { get; set;}
        public bool StorageSpace1 { get; set; }
        public bool StorageSpace2 { get; set; }
        public bool StorageSpace3 { get; set; }
        public bool StorageSpace4 { get; set; }
        public bool OperatingMemory1 { get; set; }
        public bool OperatingMemory2 { get; set; }
        public bool OperatingMemory3 { get; set; }
        public bool OperatingMemory4 { get; set; }
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
    }
}
