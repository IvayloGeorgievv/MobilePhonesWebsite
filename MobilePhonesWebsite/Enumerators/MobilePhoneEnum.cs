namespace MobilePhonesWebsite.Enumerators
{
    public class MobilePhoneEnum
    {
        public enum OperatingSystem
        {
            Непознат = 0,
            Android_11 = 1,
            Android = 2,
            iOS = 3,
            iOS_14 = 4,
            iOS_15 = 5
        }

        public enum DisplayTechnology
        {
            Непознат = 0,
            IPS = 1,
            OLED = 2,
            AMOLED = 3,
            LiquidRetinaHD_LCD = 4,
            DynamicAMOLED2X = 5,
            LCD = 6,
            IPS_LCD = 7,
            PLS_TFT_LCD = 8,
            PLS_LCD = 9
        }
        public enum AdditionalFunctions
        {
            НеПоддържа = 0,
            Поддържа = 1
        }
        public enum USBType
        {
            Непознат = 0,
            MicroUSB = 1,
            USBTypeC = 2,
            USBTypeA = 3,
            Lightning = 4
        }
        public enum SIMCardType
        {
            MiniSIM = 1,
            MicroSIM = 2,
            NanoSIM = 3,
            ChipSIM = 4,
            eSIM = 5
        }
        public enum SecondSIMCardType
        {
            НеПоддържа = 0,
            MiniSIM = 1,
            MicroSIM = 2,
            NanoSIM = 3,
            ChipSIM = 4,
            eSIM = 5
        }

        public enum CPUType
        {
            Непознат = 0,
            Двуядрен = 1,
            Четириядрен = 2,
            Шестоядрен = 3,
            Осемядрен = 4
        }

    }
}
