using MobilePhonesWebsite.Data;
using MobilePhonesWebsite.Models;
using MobilePhonesWebsite.ViewModels.HeadphonesVM;
using System.Data.Entity;
using System.Linq.Expressions;

namespace MobilePhonesWebsite.Repository
{
    public class WiredHeadphonesRepository
    {
        static readonly ApplicationDbContext applicationDbContext;
        public static List<WiredHeadphones> Items = new List<WiredHeadphones>();

        static WiredHeadphonesRepository()
        {
            applicationDbContext = new ApplicationDbContext();
            Items = applicationDbContext.WiredHeadphones.ToList();
        }

        public bool HeadphonesExisting(int id)
        {
            foreach(var item in Items)
            {
                if(item.Id == id)
                {
                    return true;
                }
            }
            return false;
        }

        public void AddWiredHeadphones(WiredHeadphones item)
        {
            WiredHeadphones headphones = new WiredHeadphones();
            headphones.Brand = item.Brand;
            headphones.Model = item.Model;
            headphones.Connectivity = item.Connectivity;
            headphones.Type = item.Type;
            headphones.Colour = item.Colour;
            headphones.Image = item.Image;
            headphones.Price = item.Price;
            headphones.DateAdded = DateTime.Now;

            applicationDbContext.WiredHeadphones.Add(headphones);
            applicationDbContext.SaveChanges();
        }
        
        public void RemoveWiredHeadphones(int id)
        {
            WiredHeadphones headphones = applicationDbContext.WiredHeadphones.Find(id);

            applicationDbContext.WiredHeadphones.Remove(headphones);
            applicationDbContext.SaveChanges();
        }

        public void UpdateWiredHeadphones(EditWiredHeadphonesVM item)
        {
            WiredHeadphones headphones = applicationDbContext.WiredHeadphones.Find(item.Id);

            headphones.Id = item.Id;
            headphones.Brand = item.Brand;
            headphones.Model = item.Model;
            headphones.Type = item.Type;
            headphones.Colour = item.Colour;
            headphones.Price = item.Price;

            headphones.DateAdded = DateTime.Now;

            applicationDbContext.Entry(headphones).State = EntityState.Modified;
            applicationDbContext.SaveChanges();
        }

        public List<WiredHeadphones> GetAllWiredHeadphones(Expression<Func<WiredHeadphones, bool>> filter = null)
        {
            IQueryable<WiredHeadphones> query = applicationDbContext.WiredHeadphones;

            if(filter != null)
            {
                query = query.Where(filter);
            }

            return query.OrderBy(x => x.Id).ToList();
        }

        public int WiredHeadphonesCount(Expression<Func<WiredHeadphones,bool>> filter = null)
        {
            IQueryable<WiredHeadphones> query = applicationDbContext.WiredHeadphones;

            if(filter != null)
            {
                query = query.Where(filter);
            }

            return query.Count();
        }

        public double GetWiredHeadphonesPrice(int id)
        {
            WiredHeadphones wiredHeadphones = applicationDbContext.WiredHeadphones.Find(id);

            return wiredHeadphones.Price;
        }

        public string GetWiredHeadphonesName(int id)
        {
            WiredHeadphones wiredHeadphones = applicationDbContext.WiredHeadphones.Find(id);

            return string.Concat(wiredHeadphones.Brand, " ", wiredHeadphones.Model);
        }
        public string GetWiredHeadphonesImageLink(int id)
        {
            WiredHeadphones wiredHeadphones = applicationDbContext.WiredHeadphones.Find(id);
            return wiredHeadphones.Image;
        }

        public List<dynamic> WiredHeadphonesSearch(string search)
        {
            List<dynamic> Searched = new List<dynamic>();
            IQueryable<WiredHeadphones> query = applicationDbContext.WiredHeadphones;
            foreach (var item in query)
            {
                string name = string.Concat("Слушалки ",item.Brand, " ", item.Model).ToLower();
                if (name.Contains(search.ToLower()))
                {
                    Searched.Add(item);
                }
            }

            return Searched;
        }

        public WiredHeadphones GetNewestWiredHeadphones()
        {
            var newestHeadphones = applicationDbContext.WiredHeadphones.OrderByDescending(x => x.DateAdded).FirstOrDefault();

            return newestHeadphones;
        }
    }
}
