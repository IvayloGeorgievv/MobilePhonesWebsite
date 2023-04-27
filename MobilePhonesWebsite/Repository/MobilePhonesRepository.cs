using MobilePhonesWebsite.Data;
using MobilePhonesWebsite.Models;
using MobilePhonesWebsite.ViewModels.MobilePhoneVM;
using System.Data.Entity;
using System.Data.SqlTypes;
using System.Linq.Expressions;

namespace MobilePhonesWebsite.Repository
{
    public class MobilePhonesRepository
    {
        static readonly ApplicationDbContext applicationDbContext;
        public static List<MobilePhone> Items = new List<MobilePhone>();

        static MobilePhonesRepository()
        {
            applicationDbContext = new ApplicationDbContext();
            Items = applicationDbContext.MobilePhones.ToList();
        }

        public bool MobilePhoneExisting(int id)
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

        public void AddMobilePhone(MobilePhone item)
        {
            MobilePhone mobilePhone = new MobilePhone();

            mobilePhone.Brand = item.Brand;
            mobilePhone.Model = item.Model;
            mobilePhone.Size = item.Size;
            mobilePhone.BatteryLife = item.BatteryLife;
            mobilePhone.Weight = item.Weight;
            mobilePhone.SIMCard = item.SIMCard;
            mobilePhone.MobilePhoneCameraId = item.MobilePhoneCameraId;
            mobilePhone.MobilePhoneDisplayId = item.MobilePhoneDisplayId;
            mobilePhone.MobilePhoneProcessorId = item.MobilePhoneProcessorId;
            mobilePhone.AdditionalFunctions = item.AdditionalFunctions;
            mobilePhone.OperatingMemory = item.OperatingMemory;
            mobilePhone.StorageSpace= item.StorageSpace;
            mobilePhone.Price = item.Price;
            mobilePhone.Image1= item.Image1;
            mobilePhone.Image2= item.Image2;
            mobilePhone.DateAdded = DateTime.Now;

            applicationDbContext.MobilePhones.Add(mobilePhone);
            applicationDbContext.SaveChanges();
        }

        public void RemoveMobilePhone(int id)
        {
            MobilePhone mobilePhone = applicationDbContext.MobilePhones.Find(id);

            applicationDbContext.MobilePhones.Remove(mobilePhone);
            applicationDbContext.SaveChanges();
        }
        
        public void UpdateMobilePhone(EditMobilePhoneVM item)
        {
            MobilePhone mobilePhone = applicationDbContext.MobilePhones.Find(item.MobilePhone.Id);

            if (mobilePhone != null)
            {
                mobilePhone.Brand = item.MobilePhone.Brand;
                mobilePhone.Model = item.MobilePhone.Model;
                mobilePhone.Size = item.PhoneHeight + " x " + item.PhoneWidth + " x " + item.PhoneThickness + " мм.";
                mobilePhone.BatteryLife = item.MobilePhone.BatteryLife;
                mobilePhone.Weight = item.MobilePhone.Weight;
                mobilePhone.SIMCard = item.MobilePhone.SIMCard;
                mobilePhone.Colour = item.MobilePhone.Colour;
                mobilePhone.OperatingMemory = item.MobilePhone.OperatingMemory;
                mobilePhone.StorageSpace = item.MobilePhone.StorageSpace;
                mobilePhone.Price = item.MobilePhone.Price;


                applicationDbContext.Entry(mobilePhone).State = EntityState.Modified;
                applicationDbContext.SaveChanges();
            }
        }

        public List<MobilePhone> GetAllMobilePhones(Expression<Func<MobilePhone, bool>> filter = null)
        {
            IQueryable<MobilePhone> query = applicationDbContext.MobilePhones;

            if (filter != null)
            {
                query = query.Where(filter);

            }
            return query.OrderBy(x => x.Id).ToList();

        }

        public Task<List<MobilePhone>> GetAllMobilePhonesWithoutFilter(int page = 1, int pageSize = int.MaxValue)
        {
            IQueryable<MobilePhone> query = applicationDbContext.MobilePhones;

            return   query.OrderBy(x => x.Id)
                              .Skip((page - 1) * pageSize)
                              .Take(pageSize)
                              .ToListAsync();
        }

        public int MobilePhonesCount(Expression<Func<MobilePhone, bool>> filter = null)
        {
            IQueryable<MobilePhone> query = applicationDbContext.MobilePhones;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.Count();
        }

        public double GetMobilePhonePrice(int id)
        {
            MobilePhone mobilePhone = applicationDbContext.MobilePhones.Find(id);

            return mobilePhone.Price;
        }

        public string GetMobilePhoneName(int id)
        {
            MobilePhone mobilePhone = applicationDbContext.MobilePhones.Find(id);

            return string.Concat(mobilePhone.Brand, " " ,mobilePhone.Model);
        }

        public string GetMobilePhoneImageLink(int id)
        {
            MobilePhone mobilePhone = applicationDbContext.MobilePhones.Find(id);
            return mobilePhone.Image1;
        }

        public List<dynamic> MobilePhoneSearch(string search)
        {
            List<dynamic> Searched = new List<dynamic>();
            IQueryable<MobilePhone> query = applicationDbContext.MobilePhones;
            foreach (var item in query)
            {
                string name = string.Concat("Телефон ",item.Brand, " ", item.Model).ToLower();
                if (name.Contains(search.ToLower()))
                {
                    Searched.Add(item);
                }
            }

            return Searched;
        }

        public MobilePhone GetNewestMobilePhone()
        {
            var newestPhone = applicationDbContext.MobilePhones.OrderByDescending(x => x.DateAdded).FirstOrDefault();

            return newestPhone;
        }
    }
}
