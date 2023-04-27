using MobilePhonesWebsite.Data;
using MobilePhonesWebsite.Models;
using MobilePhonesWebsite.ViewModels.MobilePhoneVM;
using System.Data.Entity;
using System.Linq.Expressions;

namespace MobilePhonesWebsite.Repository
{
    public class MobilePhoneCameraRepository
    {
        static readonly ApplicationDbContext applicationDbContext;
        public static List<MobilePhoneCamera> Items = new List<MobilePhoneCamera>();

        static MobilePhoneCameraRepository()
        {
            applicationDbContext = new ApplicationDbContext();
            Items = applicationDbContext.MobilePhoneCameras.ToList();
        }

        public bool MobilePhoneCameraExisting(int id)
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

        public void AddMobilePhoneCamera(MobilePhoneCamera item)
        {
            MobilePhoneCamera camera = new MobilePhoneCamera();

            camera.MainRearCamera = item.MainRearCamera;
            camera.RearCamera = item.RearCamera;
            camera.FrontCamera = item.FrontCamera;

            applicationDbContext.MobilePhoneCameras.Add(camera);
            applicationDbContext.SaveChanges();
        }

        public void RemoveMobilePhoneCamera(int id)
        {
            MobilePhoneCamera camera = applicationDbContext.MobilePhoneCameras.Find(id);

            applicationDbContext.MobilePhoneCameras.Remove(camera);
            applicationDbContext.SaveChanges();
        }

        public void UpdateMobilePhoneCamera(MobilePhoneCamera item)
        {
            MobilePhoneCamera camera = applicationDbContext.MobilePhoneCameras.Find(item.Id);

            if (camera != null)
            {
                camera.MainRearCamera = item.MainRearCamera;
                camera.RearCamera = item.RearCamera;
                camera.FrontCamera = item.FrontCamera;

                applicationDbContext.Entry(camera).State = EntityState.Modified;
                applicationDbContext.SaveChanges();
            }
        }

        public List<MobilePhoneCamera> GetAllMobilePhoneCameras(Expression<Func<MobilePhoneCamera, bool>> filter = null, int page = 1, int pageSize = int.MaxValue)
        {
            IQueryable<MobilePhoneCamera> query = applicationDbContext.MobilePhoneCameras;

            if (filter != null)
            {
                query = query.Where(filter);

            }
            return query.OrderBy(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();

        }

        public int MobilePhoneCamerasCount(Expression<Func<MobilePhoneCamera, bool>> filter = null)
        {
            IQueryable<MobilePhoneCamera> query = applicationDbContext.MobilePhoneCameras;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.Count();
        }
        public int GetLastAddedMobilePhoneCameraId()
        {
            try
            {
                var lastCamera = applicationDbContext.MobilePhoneCameras.OrderByDescending(x => x.Id).FirstOrDefault();

                return lastCamera != null ? lastCamera.Id : 0;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
