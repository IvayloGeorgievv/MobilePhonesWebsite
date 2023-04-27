using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static MobilePhonesWebsite.Enumerators.MobilePhoneEnum;

namespace MobilePhonesWebsite.Models
{
    [Table("AdditionalFunctions")]
    public class MobilePhoneAdditionalFunction
    {
        [Key]
        public int Id { get; set; }
        public SecondSIMCardType SecondSIM { get; set; }
        public AdditionalFunctions HeadphonesJack { get; set; }
        public AdditionalFunctions MemoryCard { get; set; }
        public AdditionalFunctions FingerPrintReader { get; set; }
        public AdditionalFunctions FacialRecognition { get; set; }
        public AdditionalFunctions NFC { get; set; }
    }
}
