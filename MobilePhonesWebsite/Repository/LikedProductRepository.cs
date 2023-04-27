using MobilePhonesWebsite.Data;
using MobilePhonesWebsite.Models;
using System.Data.Entity;

namespace MobilePhonesWebsite.Repository
{
	public class LikedProductRepository
	{
		static readonly ApplicationDbContext applicationDbContext;
		public static List<LikedProduct> Items = new List<LikedProduct>();

		static LikedProductRepository()
		{
			applicationDbContext = new ApplicationDbContext();
			Items = applicationDbContext.LikedProducts.ToList();
		}

		public LikedProduct CheckForExisting(int userId, int id, string type)
		{
			foreach (var item in Items)
			{
				if (item.ProductId == id && item.UserId == userId && item.ProductType == type)
				{
					return item;
				}
			}
			return null;
		}

		public void AddToLikedProducts(LikedProduct item)
		{
			LikedProduct liked = new LikedProduct();
			liked.ProductId = item.ProductId;
			liked.ProductType = item.ProductType;
			liked.UserId = item.UserId;
			liked.ProductName = item.ProductName;
			liked.Image = item.Image;
			liked.Price = item.Price;

			applicationDbContext.LikedProducts.Add(liked);
			applicationDbContext.SaveChanges();
		}

        public async Task RemoveFromLikedProducts(int id)
        {
            LikedProduct likedProduct = await applicationDbContext.LikedProducts.FindAsync(id);

            if (likedProduct != null)
            {
                applicationDbContext.LikedProducts.Remove(likedProduct);
                await applicationDbContext.SaveChangesAsync();
            }
        }

        public async Task<List<LikedProduct>> GetByUserIdAsync(int userId)
        {
            List<LikedProduct> AllLikedItems = await applicationDbContext.LikedProducts.ToListAsync();
            List<LikedProduct> liked = new List<LikedProduct>();
            foreach (var item in AllLikedItems)
            {
                if (item.UserId == userId)
                {
                    liked.Add(item);
                }
            }
            return liked;
        }
    }
}
