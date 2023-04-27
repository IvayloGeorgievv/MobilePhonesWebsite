using MobilePhonesWebsite.Data;
using MobilePhonesWebsite.Models;
using MobilePhonesWebsite.ViewModels.PhoneProtectorVM;
using System.Data.Entity;
using System.Linq.Expressions;

namespace MobilePhonesWebsite.Repository
{
    public class PhoneProtectorRepository
    {
        static readonly ApplicationDbContext applicationDbContext;
        public static List<PhoneProtector> Items = new List<PhoneProtector>();
        
        static PhoneProtectorRepository()
        {
            applicationDbContext = new ApplicationDbContext();
            Items = applicationDbContext.PhoneProtectors.ToList();
        }

        public bool PhoneProtectorExisting(int id)
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

        public void AddPhoneProtector(PhoneProtector item)
        {
            PhoneProtector phoneProtector = new PhoneProtector();
            phoneProtector.Brand = item.Brand;
            phoneProtector.FitFor = item.FitFor;
            phoneProtector.Image = item.Image;
            phoneProtector.Price = item.Price;
            phoneProtector.DateAdded = DateTime.Now;

            applicationDbContext.PhoneProtectors.Add(phoneProtector);
            applicationDbContext.SaveChanges();
        }

        public void RemovePhoneProtector(int id)
        {
            PhoneProtector phoneProtector = applicationDbContext.PhoneProtectors.Find(id);

            applicationDbContext.PhoneProtectors.Remove(phoneProtector);
            applicationDbContext.SaveChanges();
        }

        public void UpdatePhoneProtector(EditPhoneProtectorVM item)
        {
            PhoneProtector phoneProtector = applicationDbContext.PhoneProtectors.Find(item.Id);

            if (phoneProtector != null)
            {
                phoneProtector.Brand = item.Brand;
                phoneProtector.FitFor = item.FitFor;
                phoneProtector.Price = item.Price;

                phoneProtector.DateAdded = DateTime.Now;

                applicationDbContext.Entry(phoneProtector).State = EntityState.Modified;
                applicationDbContext.SaveChanges();
            }
        }

        public List<PhoneProtector> GetAllPhoneProtectors(Expression<Func<PhoneProtector, bool>> filter = null)
        {
            IQueryable<PhoneProtector> query = applicationDbContext.PhoneProtectors;

            if(filter != null)
            {
                query = query.Where(filter);
            }

            return query.OrderBy(x => x.Id).ToList();
        }

        public int PhoneProtectorsCount(Expression<Func<PhoneProtector,bool>> filter = null)
        {
            IQueryable<PhoneProtector> query = applicationDbContext.PhoneProtectors;

            if(filter != null)
            {
                query = query.Where(filter);
            }

            return query.Count();
        }

        public double GetPhoneProtectorPrice(int id)
        {
            PhoneProtector phoneProtector = applicationDbContext.PhoneProtectors.Find(id);

            return phoneProtector.Price;
        }

        public string GetPhoneProtectorName(int id)
        {
            PhoneProtector phoneProtector = applicationDbContext.PhoneProtectors.Find(id);

            return string.Concat(phoneProtector.Brand, " за ", phoneProtector.FitFor);
        }

        public string GetPhoneProtectorImageLink(int id)
        {
            PhoneProtector phoneProtector = applicationDbContext.PhoneProtectors.Find(id);
            return phoneProtector.Image;
        }

        public List<dynamic> PhoneProtectorSearch(string search)
        {
            List<dynamic> Searched = new List<dynamic>();
            IQueryable<PhoneProtector> query = applicationDbContext.PhoneProtectors;
            string name;
            foreach (var item in query)
            {
                name = string.Concat("Протектор ",item.Brand, " ", item.FitFor).ToLower();
                if (name.Contains(search.ToLower()))
                {
                    Searched.Add(item);
                }
            }

            return Searched;
        }
    }
}
