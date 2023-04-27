using MobilePhonesWebsite.Models;
using MobilePhonesWebsite.ViewModels.SharedVM;

namespace MobilePhonesWebsite.ViewModels.HeadphonesVM
{
    public class DisplayHeadphonesVM
    {
        public List<WiredHeadphones> WiredHeadphones { get; set; }
        public List<WirelessHeadphones> WirelessHeadphones { get; set; }
        public FilterHeadphonesVM Filter { get; set; }
        public bool Brand1 { get; set; }
        public bool Brand2 { get; set; }
        public bool Brand3 { get; set; }
        public bool Brand4 { get; set; }
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
    }
}
