using MobilePhonesWebsite.Data;
using MobilePhonesWebsite.Models;
using MobilePhonesWebsite.ViewModels.MobilePhoneVM;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq.Expressions;

namespace MobilePhonesWebsite.Repository
{
    public class MobilePhoneProcessorRepository
    {
        static readonly ApplicationDbContext applicationDbContext;
        public static List<MobilePhoneProcessor> Items = new List<MobilePhoneProcessor>();

        static MobilePhoneProcessorRepository()
        {
            applicationDbContext = new ApplicationDbContext();
            Items = applicationDbContext.MobilePhoneProcessors.ToList();
        }

        public bool MobilePhoneProcessorExisting(int id)
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

        public void AddMobilePhoneProcesor(MobilePhoneProcessor item)
        {
            MobilePhoneProcessor processor = new MobilePhoneProcessor();
            processor.ProcessorType = item.ProcessorType;
            processor.ProcessorFrequency = item.ProcessorFrequency;

            applicationDbContext.MobilePhoneProcessors.Add(processor);
            applicationDbContext.SaveChanges();
        }

        public void RemoveMobilePhoneProcesor(int id)
        {
            MobilePhoneProcessor processor = applicationDbContext.MobilePhoneProcessors.Find(id);

            applicationDbContext.MobilePhoneProcessors.Remove(processor);
            applicationDbContext.SaveChanges();
        }

        public void UpdateMobilePhoneProcessor(MobilePhoneProcessor item)
        {
            MobilePhoneProcessor processor = applicationDbContext.MobilePhoneProcessors.Find(item.Id);

            if (processor != null)
            {
                processor.ProcessorType = item.ProcessorType;
                processor.ProcessorFrequency = item.ProcessorFrequency;

                applicationDbContext.Entry(processor).State = EntityState.Modified;
                applicationDbContext.SaveChanges();
            }
        }

        public List<MobilePhoneProcessor> GetAllMobilePhoneProcessors(Expression<Func<MobilePhoneProcessor, bool>> filter = null, int page = 1, int pageSize = int.MaxValue)
        {
            IQueryable<MobilePhoneProcessor> query = applicationDbContext.MobilePhoneProcessors;

            if (filter != null)
            {
                query = query.Where(filter);

            }
            return query.OrderBy(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();

        }

        public int MobilePhoneProcessorCount(Expression<Func<MobilePhoneProcessor, bool>> filter = null)
        {
            IQueryable<MobilePhoneProcessor> query = applicationDbContext.MobilePhoneProcessors;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.Count();
        }

        public int GetLastAddedMobilePhoneProcessorId()
        {
            try
            {
                var lastProcessor = applicationDbContext.MobilePhoneProcessors.OrderByDescending(x => x.Id).FirstOrDefault();

                return lastProcessor != null ? lastProcessor.Id : 0;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
