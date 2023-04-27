using MobilePhonesWebsite.Models;

namespace MobilePhonesWebsite.ViewModels.UserVM
{
    public class CartVM
    {
        public List<Cart> Cart {get;set;}
        public int TotalProducts { get;set;}
        public double TotalPrice { get; set; }
    }
}
