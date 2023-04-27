using MobilePhonesWebsite.Models;
using MobilePhonesWebsite.ViewModels.SharedVM;

namespace MobilePhonesWebsite.ViewModels.SmartwatchVM
{
    public class DisplaySmartwatchVM
    {
        public List<Smartwatch> Smartwatches { get; set; }
        public FilterSmartwatchVM Filter { get; set; }
        public bool Brand1 { get; set; }
        public bool Brand2 { get; set; }
        public bool Brand3 { get; set; }
        public bool Brand4 { get; set; }
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
    }
}
