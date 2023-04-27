using MobilePhonesWebsite.Data;
using MobilePhonesWebsite.Models;
using MobilePhonesWebsite.ViewModels.SmartwatchVM;
using System.Data.Entity;
using System.Linq.Expressions;

namespace MobilePhonesWebsite.Repository
{
    public class SmartwatchRepository
    {
        static readonly ApplicationDbContext applicationDbContext;
        public static List<Smartwatch> Items = new List<Smartwatch>();

        static SmartwatchRepository()
        {
            applicationDbContext = new ApplicationDbContext();
            Items = applicationDbContext.Smartwatches.ToList();
        }

        public bool SmartwatchExisting(int id)
        {
            foreach(var item in Items)
            {
                if(item.Id == id)
                {
                    return true;
                }
            }
            return  false;
        }

        public void AddSmartwatch(Smartwatch item)
        {
            Smartwatch smartwatch = new Smartwatch();
            smartwatch.Brand = item.Brand;
            smartwatch.Model = item.Model;
            smartwatch.BatteryLife = item.BatteryLife;
            smartwatch.DisplaySize = item.DisplaySize;
            smartwatch.DisplayTechnology = item.DisplayTechnology;
            smartwatch.Colour = item.Colour;
            smartwatch.Weight = item.Weight;
            smartwatch.Image = item.Image;
            smartwatch.DateAdded = DateTime.Now;
            smartwatch.Price = item.Price;
            
            applicationDbContext.Smartwatches.Add(smartwatch);
            applicationDbContext.SaveChanges();
        }

        public void RemoveSmartwatch(int id)
        {
            Smartwatch smartwatch = applicationDbContext.Smartwatches.Find(id);
            
            applicationDbContext.Smartwatches.Remove(smartwatch);
            applicationDbContext.SaveChanges();
        }

        public void UpdateSmartwatch(EditSmartwatchVM item)
        {
            Smartwatch smartwatch = applicationDbContext.Smartwatches.Find(item.Id);

            if (smartwatch != null)
            {
                smartwatch.Brand = item.Brand;
                smartwatch.Model = item.Model;
                smartwatch.BatteryLife = item.BatteryLife;
                smartwatch.DisplaySize = item.DisplaySize;
                smartwatch.DisplayTechnology = item.DisplayTechnology;
                smartwatch.Weight = item.Weight;
                smartwatch.Colour = item.Colour;
                smartwatch.Price = item.Price;

                smartwatch.DateAdded = DateTime.Now;

                applicationDbContext.Entry(smartwatch).State = EntityState.Modified;
                applicationDbContext.SaveChanges();
            }
        }

        public List<Smartwatch> GetAllSmartwatches(Expression<Func<Smartwatch,bool>> filter = null)
        {
            IQueryable<Smartwatch> query = applicationDbContext.Smartwatches;

            if(filter != null)
            {
                query = query.Where(filter);
            }       

            return query.OrderBy(x => x.Id).ToList();
        }

        public int SmartwatchesCount(Expression<Func<Smartwatch, bool>> filter = null)
        {
            IQueryable<Smartwatch> query = applicationDbContext.Smartwatches;
            if(filter != null)
            {
                query = query.Where(filter); 
            }

            return query.Count();
        }

        public double GetSmartwatchPrice(int id)
        {
            Smartwatch smartwatch = applicationDbContext.Smartwatches.Find(id);

            return smartwatch.Price;
        }

        public string GetSmartwatchName(int id)
        {
            Smartwatch smartwatch = applicationDbContext.Smartwatches.Find(id);

            return string.Concat(smartwatch.Brand, " ", smartwatch.Model);
        }

        public string GetSmartwatchImageLink(int id)
        {
            Smartwatch smartwatch = applicationDbContext.Smartwatches.Find(id);
            return smartwatch.Image;
        }

        public List<dynamic> SmartwatchSearch(string search)
        {
            List<dynamic> Searched = new List<dynamic>();
            IQueryable<Smartwatch> query = applicationDbContext.Smartwatches;
            foreach (var item in query)
            {
                string name = string.Concat("Смартчасовник ",item.Brand, " ", item.Model).ToLower();
                if (name.Contains(search.ToLower()))
                {
                    Searched.Add(item);
                }
            }

            return Searched;
        }

        public Smartwatch GetNewestSmartwatch()
        {
            var newestSmartwatch = applicationDbContext.Smartwatches.OrderByDescending(x => x.DateAdded).FirstOrDefault();

            return newestSmartwatch;
        }
    }
}
