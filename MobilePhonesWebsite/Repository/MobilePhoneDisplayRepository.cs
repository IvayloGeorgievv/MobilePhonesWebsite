using MobilePhonesWebsite.Data;
using MobilePhonesWebsite.Models;
using MobilePhonesWebsite.ViewModels.MobilePhoneVM;
using System.Data.Entity;
using System.Linq.Expressions;

namespace MobilePhonesWebsite.Repository
{
    public class MobilePhoneDisplayRepository
    {
        static readonly ApplicationDbContext applicationDbContext;
        public static List<MobilePhoneDisplay> Items = new List<MobilePhoneDisplay>();

        static MobilePhoneDisplayRepository()
        {
            applicationDbContext = new ApplicationDbContext();
            Items = applicationDbContext.MobilePhoneDisplays.ToList();
        }

        public bool MobilePhoneDisplayExisting(int id)
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

        public void AddMobilePhoneDisplay(MobilePhoneDisplay item)
        {
            MobilePhoneDisplay display = new MobilePhoneDisplay();
            display.DisplaySize = item.DisplaySize;
            display.Technology = item.Technology;
            display.Resolution = item.Resolution;

            applicationDbContext.MobilePhoneDisplays.Add(display);
            applicationDbContext.SaveChanges();
        }

        public void RemoveMobilePhoneDisplay(int id)
        {
            MobilePhoneDisplay display = applicationDbContext.MobilePhoneDisplays.Find(id);

            applicationDbContext.MobilePhoneDisplays.Remove(display);
            applicationDbContext.SaveChanges();
        }

        public void UpdateMobilePhoneDisplay(MobilePhoneDisplay item)
        {
            MobilePhoneDisplay display = applicationDbContext.MobilePhoneDisplays.Find(item.Id);

            if (display != null)
            {
                display.DisplaySize = item.DisplaySize;
                display.Technology = item.Technology;
                display.Resolution = item.Resolution;

                applicationDbContext.Entry(display).State = EntityState.Modified;
                applicationDbContext.SaveChanges();
            }
        }

        public List<MobilePhoneDisplay> GetAllMobilePhoneDisplays(Expression<Func<MobilePhoneDisplay, bool>> filter = null, int page = 1, int pageSize = int.MaxValue)
        {
            IQueryable<MobilePhoneDisplay> query = applicationDbContext.MobilePhoneDisplays;

            if (filter != null)
            {
                query = query.Where(filter);

            }
            return query.OrderBy(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();

        }

        public int MobilePhoneDisplaysCount(Expression<Func<MobilePhoneDisplay, bool>> filter = null)
        {
            IQueryable<MobilePhoneDisplay> query = applicationDbContext.MobilePhoneDisplays;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.Count();
        }
        public int GetLastAddedMobilePhoneDisplayId()
        {
            try
            {
                var lastDisplay = applicationDbContext.MobilePhoneDisplays.OrderByDescending(x => x.Id).FirstOrDefault();

                return lastDisplay != null ? lastDisplay.Id : 0;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
