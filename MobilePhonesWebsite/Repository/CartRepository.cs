using MobilePhonesWebsite.Data;
using MobilePhonesWebsite.Models;
using System.Data.Entity;

namespace MobilePhonesWebsite.Repository
{
    public class CartRepository
    {
        static readonly ApplicationDbContext applicationDbContext;
        public static List<Cart> Items = new List<Cart>();

        static CartRepository()
        {
            applicationDbContext = new ApplicationDbContext();
            Items = applicationDbContext.Cart.ToList();
        }

        public Cart CheckForExisting(int userId, int id, string type)
        {
            foreach (var item in Items)
            {
                if (item.ProductId == id && item.UserId == userId && item.ProductType == type)
                {
                    return  item;
                }
            }
            return null;
        }

        public void AddToCart(Cart item)
        {
            Cart cart = new Cart();
            cart.ProductId = item.ProductId;
            cart.ProductType = item.ProductType;
            cart.UserId = item.UserId;
            cart.ProductName = item.ProductName;
            cart.Quantity = item.Quantity;
            cart.Image = item.Image;
            cart.PriceForOne = item.PriceForOne;
            cart.Price = item.Price;

            applicationDbContext.Cart.Add(cart);
            applicationDbContext.SaveChanges();
        }

        public async Task RemoveFromCart(int id)
        {
            var shoppingCartItem = await applicationDbContext.Cart.FirstOrDefaultAsync(x => x.Id == id);

            if (shoppingCartItem != null)
            {
                applicationDbContext.Cart.Remove(shoppingCartItem);
                await applicationDbContext.SaveChangesAsync();
            }
        }

        public async Task RemoveAllFromCartByUserId(int userId)
        {
            List<Cart> cartItems = await applicationDbContext.Cart.Where(x => x.UserId == userId).ToListAsync();

            if (cartItems != null && cartItems.Any())
            {
                applicationDbContext.Cart.RemoveRange(cartItems);
                await applicationDbContext.SaveChangesAsync();
            }
        }

        public async Task<List<Cart>> GetByUserIdAsync(int userId)
        {
            List<Cart> AllCartItems = await applicationDbContext.Cart.ToListAsync();
            List<Cart> cart = new List<Cart>();
            foreach (var item in AllCartItems)
            {
                if (item.UserId == userId)
                {
                    cart.Add(item);
                }
            }
            return cart;
        }

        public async Task<Cart> GetCartItemByProductIdAndTypeAsync(int productId, string productType)
        {
            return await applicationDbContext.Cart.SingleOrDefaultAsync(c => c.ProductId == productId && c.ProductType == productType);
        }

        public async Task UpdateCartItemAsync(Cart item)
        {
            Cart cartItem = await applicationDbContext.Cart.FindAsync(item.Id);

            if (cartItem != null)
            {
                cartItem.Quantity = item.Quantity;
                cartItem.Price = cartItem.PriceForOne * item.Quantity;

                applicationDbContext.Entry(cartItem).State = EntityState.Modified;
                await applicationDbContext.SaveChangesAsync();
            }
        }

    }
}
