using MobilePhonesWebsite.Data;
using MobilePhonesWebsite.Models;
using MobilePhonesWebsite.ViewModels.PhoneCaseVM;
using System.Data.Entity;
using System.Linq.Expressions;

namespace MobilePhonesWebsite.Repository
{
    public class PhoneCaseRepository
    {
        static readonly ApplicationDbContext applicationDbContext = new ApplicationDbContext();
        public static List<PhoneCase> Items = new List<PhoneCase>();

        static PhoneCaseRepository()
        {
            applicationDbContext = new ApplicationDbContext();
            Items = applicationDbContext.PhoneCases.ToList();
        }

        public bool PhoneCaseExisting(int id)
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

        public void AddPhoneCase(PhoneCase item)
        {
            PhoneCase phoneCase = new PhoneCase();
            phoneCase.Brand = item.Brand;
            phoneCase.FitFor = item.FitFor;
            phoneCase.Colour = item.Colour;
            phoneCase.Image = item.Image;
            phoneCase.Price = item.Price;
            phoneCase.DateAdded = DateTime.Now;

            applicationDbContext.PhoneCases.Add(phoneCase);
            applicationDbContext.SaveChanges();
        }

        public void RemovePhoneCase(int id)
        {
            PhoneCase phoneCase = applicationDbContext.PhoneCases.Find(id);

            applicationDbContext.PhoneCases.Remove(phoneCase);
            applicationDbContext.SaveChanges();

        }

        public void UpdatePhoneCase(EditPhoneCaseVM item)
        {
            PhoneCase phoneCase = applicationDbContext.PhoneCases.Find(item.Id);

            if(phoneCase!= null)
            {
                phoneCase.Brand = item.Brand;
                phoneCase.FitFor = item.FitFor;
                phoneCase.Colour = item.Colour;
                phoneCase.Price = item.Price;

                applicationDbContext.Entry(phoneCase).State = EntityState.Modified;
                applicationDbContext.SaveChanges();
            }
        }

        public List<PhoneCase> GetAllPhoneCases(Expression<Func<PhoneCase,bool>> filter = null)
        {
            IQueryable<PhoneCase> query = applicationDbContext.PhoneCases;

            if(filter != null)
            {
                query = query.Where(filter);
            }

            return query.OrderBy(x => x.Id).ToList();
        }

        public int PhoneCasesCount(Expression<Func<PhoneCase, bool>> filter = null)
        {
            IQueryable<PhoneCase> query = applicationDbContext.PhoneCases;

            if(filter != null)
            {
                query = query.Where(filter);
            }

            return query.Count();
        }

        public double GetPhoneCasePrice(int id)
        {
            PhoneCase phoneCase = applicationDbContext.PhoneCases.Find(id);

            return phoneCase.Price;
        }

        public string GetPhoneCaseName(int id)
        {
            PhoneCase phoneCase = applicationDbContext.PhoneCases.Find(id);

            return string.Concat(phoneCase.Brand, " за ", phoneCase.FitFor);
        }

        public string GetPhoneCaseImageLink(int id)
        {
            PhoneCase phoneCase = applicationDbContext.PhoneCases.Find(id);
            return phoneCase.Image;
        }

        public List<dynamic> PhoneCaseSearch(string search)
        {
            List<dynamic> Searched = new List<dynamic>();
            IQueryable<PhoneCase> query = applicationDbContext.PhoneCases;
            string name;
            foreach (var item in query)
            {
                name = string.Concat("Калъф ",item.Brand, " ", item.FitFor).ToLower();
                if (name.Contains(search.ToLower()))
                {
                    Searched.Add(item);
                }
            }

            return Searched;
        }
    }
}
