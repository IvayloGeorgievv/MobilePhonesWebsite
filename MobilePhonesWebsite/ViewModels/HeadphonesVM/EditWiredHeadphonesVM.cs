using MobilePhonesWebsite.ViewModels.SharedVM;
using static MobilePhonesWebsite.Enumerators.HeadphonesEnum;
using static MobilePhonesWebsite.Enumerators.SharedEnum;

namespace MobilePhonesWebsite.ViewModels.HeadphonesVM
{
    public class EditWiredHeadphonesVM : EditAccessoryVM
    {
        public string Model { get; set; }
        public HeadphonesType Type { get; set; }
        public Colour Colour { get; set; }
    }
}
