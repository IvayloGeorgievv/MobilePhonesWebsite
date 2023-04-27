using System.ComponentModel;
using System.Reflection;

namespace MobilePhonesWebsite.Enumerators
{
    public class OrderEnum
    {
        public enum OrderStatusEnum
        {
            Предстояща = 0,
            Изпратена = 1,
            Доставена = 2,
            Отменена = 3
        }
        public enum ShippingMethod
        {
            Стандартна_доставка = 0,
            Експресна_доставка,
        }
        public enum PaymentMethod
        {
            Наложен_платеж = 0,
            Чрез_Кредитна_карта = 1
        }
    }
}
