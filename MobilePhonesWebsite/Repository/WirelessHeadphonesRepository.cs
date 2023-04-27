using MobilePhonesWebsite.Data;
using MobilePhonesWebsite.Models;
using MobilePhonesWebsite.ViewModels.HeadphonesVM;
using System.Data.Entity;
using System.Linq.Expressions;

namespace MobilePhonesWebsite.Repository
{
    public class WirelessHeadphonesRepository
    {
        static readonly ApplicationDbContext applicationDbContext;
        public static List<WirelessHeadphones> Items = new List<WirelessHeadphones>();

        static WirelessHeadphonesRepository()
        {
            applicationDbContext = new ApplicationDbContext();
            Items = applicationDbContext.WirelessHeadphones.ToList();
        }

        public bool HeadphonesExisting(int id)
        {
            foreach (var item in Items)
            {
                if (item.Id == id)
                {
                    return true;
                }
            }
            return false;
        }

        public void AddWirelessHeadphones(WirelessHeadphones item)
        {
            WirelessHeadphones headphones = new WirelessHeadphones();
            headphones.Brand = item.Brand;
            headphones.Model = item.Model;
            headphones.Connectivity = item.Connectivity;
            headphones.Type = item.Type;
            headphones.BatteryLife = item.BatteryLife;
            headphones.BatteryLifeWithCase = item.BatteryLifeWithCase;
            headphones.Colour = item.Colour;
            headphones.Image = item.Image;
            headphones.Price = item.Price;

            headphones.DateAdded = DateTime.Now;

            applicationDbContext.WirelessHeadphones.Add(headphones);
            applicationDbContext.SaveChanges();
        }

        public void RemoveWirelessHeadphones(int id)
        {
            WirelessHeadphones headphones = applicationDbContext.WirelessHeadphones.Find(id);

            applicationDbContext.WirelessHeadphones.Remove(headphones);
            applicationDbContext.SaveChanges();
        }

        public void UpdateWirelessHeadphones(EditWirelessHeadphonesVM item)
        {
            WirelessHeadphones headphones = applicationDbContext.WirelessHeadphones.Find(item.Id);

            headphones.Id = item.Id;
            headphones.Brand = item.Brand;
            headphones.Model = item.Model;
            headphones.Type = item.Type;
            headphones.BatteryLife = item.BatteryLife;
            headphones.BatteryLifeWithCase = item.BatteryLifeWithCase;
            headphones.Colour = item.Colour;
            headphones.Price = item.Price;

            headphones.DateAdded = DateTime.Now;

            applicationDbContext.Entry(headphones).State = EntityState.Modified;
            applicationDbContext.SaveChanges();
        }

        public List<WirelessHeadphones> GetAllWirelessHeadphones(Expression<Func<WirelessHeadphones, bool>> filter = null)
        {
            IQueryable<WirelessHeadphones> query = applicationDbContext.WirelessHeadphones;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.OrderBy(x => x.Id).ToList();
        }

        public int WirelessHeadphonesCount(Expression<Func<WirelessHeadphones, bool>> filter = null)
        {
            IQueryable<WirelessHeadphones> query = applicationDbContext.WirelessHeadphones;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.Count();
        }

        public double GetWirelessHeadphonesPrice(int id)
        {
            WirelessHeadphones wirelessHeadphones = applicationDbContext.WirelessHeadphones.Find(id);

            return wirelessHeadphones.Price;
        }

        public string GetWirelessHeadphonesName(int id)
        {
            WirelessHeadphones wirelessHeadphones = applicationDbContext.WirelessHeadphones.Find(id);

            return string.Concat(wirelessHeadphones.Brand, " ", wirelessHeadphones.Model);
        }

        public string GetWirelessHeadphonesImageLink(int id)
        {
            WirelessHeadphones wirelessHeadphones = applicationDbContext.WirelessHeadphones.Find(id);
            return wirelessHeadphones.Image;
        }

        public List<dynamic> WirelessHeadphonesSearch(string search)
        {
            List<dynamic> Searched = new List<dynamic>();
            IQueryable<WirelessHeadphones> query = applicationDbContext.WirelessHeadphones;
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

        public WirelessHeadphones GetNewestWirelessHeadphones()
        {
            var newestHeadphones = applicationDbContext.WirelessHeadphones.OrderByDescending(x => x.DateAdded).FirstOrDefault();

            return newestHeadphones;
        }
    }
}
