using MobilePhonesWebsite.Data;
using MobilePhonesWebsite.Models;
using MobilePhonesWebsite.ViewModels.MobilePhoneVM;
using System.Data.Entity;
using System.Linq.Expressions;

namespace MobilePhonesWebsite.Repository
{
    public class MobilePhoneAdditionalFunctionsRepository
    {
        static readonly ApplicationDbContext applicationDbContext;
        public static List<MobilePhoneAdditionalFunction> Items = new List<MobilePhoneAdditionalFunction>();

        static MobilePhoneAdditionalFunctionsRepository()
        {
            applicationDbContext = new ApplicationDbContext();
            Items = applicationDbContext.MobilePhoneAdditionalFunctions.ToList();
        }

        public bool AdditionalFunctionExisting(int id)
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

        public void AddAdditionalFunction(MobilePhoneAdditionalFunction item)
        {
            MobilePhoneAdditionalFunction additionalFunction = new MobilePhoneAdditionalFunction();

            additionalFunction.SecondSIM = item.SecondSIM;
            additionalFunction.HeadphonesJack = item.HeadphonesJack;
            additionalFunction.MemoryCard = item.MemoryCard;
            additionalFunction.FingerPrintReader = item.FingerPrintReader;
            additionalFunction.FacialRecognition = item.FacialRecognition;
            additionalFunction.NFC = item.NFC;

            applicationDbContext.MobilePhoneAdditionalFunctions.Add(additionalFunction);
            applicationDbContext.SaveChanges();
        }

        public void RemoveAdditionalFunction(int id)
        {
            MobilePhoneAdditionalFunction additionalFunction = applicationDbContext.MobilePhoneAdditionalFunctions.Find(id);

            applicationDbContext.MobilePhoneAdditionalFunctions.Remove(additionalFunction);
            applicationDbContext.SaveChanges();
        }

        public void UpdateAdditionalFunction(MobilePhoneAdditionalFunction item)
        {
            MobilePhoneAdditionalFunction additionalFunction = applicationDbContext.MobilePhoneAdditionalFunctions.Find(item.Id);

            if (additionalFunction != null)
            {
                additionalFunction.SecondSIM = item.SecondSIM;
                additionalFunction.HeadphonesJack = item.HeadphonesJack;
                additionalFunction.MemoryCard = item.MemoryCard;
                additionalFunction.FingerPrintReader = item.FingerPrintReader;
                additionalFunction.FacialRecognition = item.FacialRecognition;
                additionalFunction.NFC = item.NFC;

                applicationDbContext.Entry(additionalFunction).State = EntityState.Modified;
                applicationDbContext.SaveChanges();
            }
        }

        public List<MobilePhoneAdditionalFunction> GetAllAdditionalFunctions(Expression<Func<MobilePhoneAdditionalFunction, bool>> filter = null, int page = 1, int pageSize = int.MaxValue)
        {
            IQueryable<MobilePhoneAdditionalFunction> query = applicationDbContext.MobilePhoneAdditionalFunctions;

            if (filter != null)
            {
                query = query.Where(filter);

            }
            return query.OrderBy(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();

        }

        public int MobileAdditionalFunctionCount(Expression<Func<MobilePhoneAdditionalFunction, bool>> filter = null)
        {
            IQueryable<MobilePhoneAdditionalFunction> query = applicationDbContext.MobilePhoneAdditionalFunctions;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.Count();
        }
        public int GetLastAddedAdditionalFunctionId()
        {
            try
            {
                var lastAdditionalFunction = applicationDbContext.MobilePhoneAdditionalFunctions.OrderByDescending(x => x.Id).FirstOrDefault();

                return lastAdditionalFunction != null ? lastAdditionalFunction.Id : 0;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
