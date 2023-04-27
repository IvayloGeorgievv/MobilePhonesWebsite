using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MobilePhonesWebsite.Data;
using MobilePhonesWebsite.Models;
using MobilePhonesWebsite.Repository;
using MobilePhonesWebsite.ViewModels.HeadphonesVM;
using MobilePhonesWebsite.ViewModels.MobilePhoneVM;
using MobilePhonesWebsite.ViewModels.PhoneCaseVM;
using MobilePhonesWebsite.ViewModels.PhoneProtectorVM;
using MobilePhonesWebsite.ViewModels.SharedVM;
using MobilePhonesWebsite.ViewModels.SmartwatchVM;
using MobilePhonesWebsite.ViewModels.UserVM;
using Scrypt;
using System.Security.Claims;
using static MobilePhonesWebsite.Enumerators.UserEnum;

namespace MobilePhonesWebsite.Controllers
{
    public class AdminController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private ApplicationDbContext applicationDbContext;
        private ScryptEncoder scryptEncoder;

        public AdminController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            applicationDbContext = new ApplicationDbContext();
            scryptEncoder = new ScryptEncoder();
        }

        public IActionResult UserList(IndexUserVM model)
        {
            if (!User.Identity.IsAuthenticated || User.IsInRole("User"))
                return RedirectToAction("Index", "Home");
            else
            {

                UserRepository userRepository = new UserRepository();
                model.Items = userRepository.GetAllUsers();
              

                return View(model);
            }
        }
        public IActionResult DeleteUser(int id)
        {
            UserRepository userRepository = new UserRepository();
            userRepository.DeleteUser(id);

            return RedirectToAction("UserList", "Admin");
        }

        [HttpGet]
        public IActionResult UpdateUser(int id)
        {
            ApplicationDbContext applicationDbContext = new ApplicationDbContext();
            User user = applicationDbContext.Users.Find(id);
            EditUserVM item = new EditUserVM();

            item.Id = user.Id;
            item.Username = user.Username;
            item.Email = user.Email;
            item.Password = user.Password;
            item.IsAdmin = user.IsAdmin;
        
            

            return View(item);
        }

        [HttpPost]
        public IActionResult UpdateUser(EditUserVM item)
        {
            UserRepository userRepository = new UserRepository();
            if (!ModelState.IsValid)
            {
                return RedirectToAction("UserList", "Admin");
            }

            userRepository.UpdateUser(item);

            return RedirectToAction("UserList", "Admin");
        }

        public IActionResult MobilePhonesList(DisplayMobilePhoneVM model)
        {
            model.Filter ??= new FilterMobilePhoneVM();
            var filter = model.Filter.GetFilter();

            MobilePhonesRepository mobilePhonesRepository = new MobilePhonesRepository();
            model.MobilePhones = mobilePhonesRepository.GetAllMobilePhones(filter);
           


            return View(model);
        }
        [HttpGet]
        public IActionResult CreateMobilePhone()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateMobilePhone(CreateMobilePhoneVM item, IFormFile Image1, IFormFile Image2)
        {

            MobilePhonesRepository mobilePhonesRepository = new MobilePhonesRepository();
            MobilePhoneCameraRepository mobilePhoneCameraRepository = new MobilePhoneCameraRepository();
            MobilePhoneDisplayRepository mobilePhoneDisplayRepository = new MobilePhoneDisplayRepository();
            MobilePhoneProcessorRepository mobilePhoneProcessorRepository = new MobilePhoneProcessorRepository();
            MobilePhoneAdditionalFunctionsRepository additionalFunctionsRepository = new MobilePhoneAdditionalFunctionsRepository();

            MobilePhone mobilePhone = new MobilePhone();
            MobilePhoneDisplay mobilePhoneDisplay = new MobilePhoneDisplay();
            MobilePhoneProcessor mobilePhoneProcessor = new MobilePhoneProcessor();
            MobilePhoneCamera mobilePhoneCamera = new MobilePhoneCamera();
            MobilePhoneAdditionalFunction additionalFunction = new MobilePhoneAdditionalFunction();

            if (Image1 != null)
            {
                mobilePhone.Image1 = Image1.FileName.Substring(0, Image1.FileName.Length - 4);

                var directoryPath = Path.Combine(_webHostEnvironment.WebRootPath + "\\img\\mobilephones");
                var path = Path.Combine(_webHostEnvironment.WebRootPath + "\\img\\mobilephones", Image1.FileName);

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                using var fileStream = new FileStream(path, FileMode.Create);
                Image1.CopyTo(fileStream);
            }
            if (Image2 != null)
            {
                mobilePhone.Image2 = Image2.FileName.Substring(0, Image2.FileName.Length - 4);

                var directoryPath = Path.Combine(_webHostEnvironment.WebRootPath + "\\img\\mobilephones");
                var path = Path.Combine(_webHostEnvironment.WebRootPath + "\\img\\mobilephones", Image2.FileName);

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                using var fileStream = new FileStream(path, FileMode.Create);
                Image2.CopyTo(fileStream);
            }
            mobilePhoneDisplay.DisplaySize = item.MobilePhoneDisplay.DisplaySize;
            mobilePhoneDisplay.Technology = item.MobilePhoneDisplay.Technology;
            mobilePhoneDisplay.Resolution = item.MobilePhoneDisplay.Resolution;

            mobilePhoneDisplayRepository.AddMobilePhoneDisplay(mobilePhoneDisplay);

            int displayId = mobilePhoneDisplayRepository.GetLastAddedMobilePhoneDisplayId();

            mobilePhoneProcessor.ProcessorType = item.MobilePhoneProcessor.ProcessorType;
            mobilePhoneProcessor.ProcessorFrequency = item.MobilePhoneProcessor.ProcessorFrequency;

            mobilePhoneProcessorRepository.AddMobilePhoneProcesor(mobilePhoneProcessor);

            int processorId = mobilePhoneProcessorRepository.GetLastAddedMobilePhoneProcessorId();

            mobilePhoneCamera.MainRearCamera = item.MobilePhoneCamera.MainRearCamera;
            mobilePhoneCamera.RearCamera = item.MobilePhoneCamera.RearCamera;
            mobilePhoneCamera.FrontCamera = item.MobilePhoneCamera.FrontCamera;

            mobilePhoneCameraRepository.AddMobilePhoneCamera(mobilePhoneCamera);
            
            int cameraId = mobilePhoneCameraRepository.GetLastAddedMobilePhoneCameraId();

            additionalFunction.SecondSIM = item.MobilePhoneAdditionalFunction.SecondSIM;
            additionalFunction.HeadphonesJack = item.MobilePhoneAdditionalFunction.HeadphonesJack;
            additionalFunction.MemoryCard = item.MobilePhoneAdditionalFunction.MemoryCard;
            additionalFunction.FingerPrintReader = item.MobilePhoneAdditionalFunction.FingerPrintReader;
            additionalFunction.FacialRecognition = item.MobilePhoneAdditionalFunction.FacialRecognition;
            additionalFunction.NFC = item.MobilePhoneAdditionalFunction.NFC;

            additionalFunctionsRepository.AddAdditionalFunction(additionalFunction);
            int additionalFunctionId = additionalFunctionsRepository.GetLastAddedAdditionalFunctionId();

            mobilePhone.Brand = item.MobilePhone.Brand;
            mobilePhone.Model = item.MobilePhone.Model;
            mobilePhone.Size = item.PhoneHeight + " x " + item.PhoneWidth + " x " + item.PhoneThickness + " мм.";
            mobilePhone.Colour = item.MobilePhone.Colour;
            mobilePhone.BatteryLife = item.MobilePhone.BatteryLife;
            mobilePhone.Weight = item.MobilePhone.Weight;
            mobilePhone.MobilePhoneProcessorId = processorId;
            mobilePhone.MobilePhoneDisplayId = displayId;
            mobilePhone.MobilePhoneCameraId = cameraId;
            mobilePhone.AdditionalFunctions = additionalFunctionId;
            mobilePhone.OperatingMemory = item.MobilePhone.OperatingMemory;
            mobilePhone.StorageSpace = item.MobilePhone.StorageSpace;
            mobilePhone.Price = item.MobilePhone.Price;

            mobilePhonesRepository.AddMobilePhone(mobilePhone);

            return RedirectToAction("MobilePhonesList", "Admin");

        }

        public IActionResult DeleteMobilePhone(int id)
        {
            MobilePhonesRepository mobilePhonesRepository = new MobilePhonesRepository();
            MobilePhoneCameraRepository mobilePhoneCameraRepository = new MobilePhoneCameraRepository();
            MobilePhoneProcessorRepository mobilePhoneProcessorRepository = new MobilePhoneProcessorRepository();
            MobilePhoneDisplayRepository displayRepository = new MobilePhoneDisplayRepository();

            MobilePhone mobilePhone = applicationDbContext.MobilePhones.Find(id);

            mobilePhoneCameraRepository.RemoveMobilePhoneCamera(mobilePhone.MobilePhoneCameraId);
            mobilePhoneProcessorRepository.RemoveMobilePhoneProcesor(mobilePhone.MobilePhoneProcessorId);
            displayRepository.RemoveMobilePhoneDisplay(mobilePhone.MobilePhoneDisplayId);


            mobilePhonesRepository.RemoveMobilePhone(id);
            applicationDbContext.SaveChanges();

            return RedirectToAction("MobilePhonesList", "Admin");
        }

        [HttpGet]
        public IActionResult UpdateMobilePhone(int id)
        {
            ApplicationDbContext applicationDbContext = new ApplicationDbContext();
            MobilePhone mobilePhone = applicationDbContext.MobilePhones.Find(id);
            MobilePhoneCamera mobilePhoneCamera = applicationDbContext.MobilePhoneCameras.Find(mobilePhone.MobilePhoneCameraId);
            MobilePhoneDisplay mobilePhoneDisplay = applicationDbContext.MobilePhoneDisplays.Find(mobilePhone.MobilePhoneDisplayId);
            MobilePhoneProcessor mobilePhoneProcessor = applicationDbContext.MobilePhoneProcessors.Find(mobilePhone.MobilePhoneProcessorId);
            MobilePhoneAdditionalFunction mobilePhoneAdditionalFunction = applicationDbContext.MobilePhoneAdditionalFunctions.Find(mobilePhone.AdditionalFunctions);
            EditMobilePhoneVM item = new EditMobilePhoneVM();

            item.MobilePhoneDisplay = mobilePhoneDisplay;

            item.MobilePhoneCamera = mobilePhoneCamera;

            item.MobilePhoneProcessor = mobilePhoneProcessor;

            item.MobilePhoneAdditionalFunctions = mobilePhoneAdditionalFunction;

            item.MobilePhone = mobilePhone;

            string originalString = mobilePhone.Size;
            string[] splitValues = originalString.Split(new char[] { 'x' }, StringSplitOptions.RemoveEmptyEntries);

            string value1 = splitValues[0].Trim();
            string value2 = splitValues[1].Trim();

            string[] splitValue3 = splitValues[2].Split(' ');
            string value3 = splitValue3[1].Trim();


            item.PhoneHeight = double.Parse(value1);
            item.PhoneWidth = double.Parse(value2);
            item.PhoneThickness = double.Parse(value3);

      

            return View(item);
        }
        [HttpPost]
        public IActionResult UpdateMobilePhone(EditMobilePhoneVM item)
        {
            MobilePhonesRepository mobilePhonesRepository = new MobilePhonesRepository();
            MobilePhoneCameraRepository cameraRepository = new MobilePhoneCameraRepository();
            MobilePhoneDisplayRepository displayRepository = new MobilePhoneDisplayRepository();
            MobilePhoneProcessorRepository processorRepository = new MobilePhoneProcessorRepository();
            MobilePhoneAdditionalFunctionsRepository additionalFunctionRepository = new MobilePhoneAdditionalFunctionsRepository();

            MobilePhoneDisplay display = item.MobilePhoneDisplay;
            MobilePhoneCamera camera = item.MobilePhoneCamera;
            MobilePhoneProcessor processor = item.MobilePhoneProcessor;
            MobilePhoneAdditionalFunction additionalFunction = item.MobilePhoneAdditionalFunctions;
                  
          
            displayRepository.UpdateMobilePhoneDisplay(display);
            cameraRepository.UpdateMobilePhoneCamera(camera);
            processorRepository.UpdateMobilePhoneProcessor(processor);
            additionalFunctionRepository.UpdateAdditionalFunction(additionalFunction);
            mobilePhonesRepository.UpdateMobilePhone(item);

            return RedirectToAction("MobilePhonesList", "Admin");
        }

        public IActionResult HeadphonesList(DisplayHeadphonesVM model)
        {
            model.Filter ??= new FilterHeadphonesVM();

            var filter = model.Filter.GetFilter();
            var filterTwo = model.Filter.GetFilterForWireless();

            WiredHeadphonesRepository wiredHeadphonesRepository = new WiredHeadphonesRepository();
            WirelessHeadphonesRepository wirelessHeadphonesRepository = new WirelessHeadphonesRepository();
            model.WiredHeadphones = wiredHeadphonesRepository.GetAllWiredHeadphones(filter);
            model.WirelessHeadphones = wirelessHeadphonesRepository.GetAllWirelessHeadphones(filterTwo);

            return View(model);
        }

        [HttpGet]
        public IActionResult CreateWiredHeadphones()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateWiredHeadphones(CreateWiredHeadphonesVM item, IFormFile Image)
        {

            WiredHeadphonesRepository headphonesRepository = new WiredHeadphonesRepository();
            WiredHeadphones headphones = new WiredHeadphones();

            if (Image != null)
            {
                headphones.Image = Image.FileName.Substring(0, Image.FileName.Length - 4);

                var directoryPath = Path.Combine(_webHostEnvironment.WebRootPath + "\\img\\headphones\\wired");
          

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                var path = Path.Combine(_webHostEnvironment.WebRootPath + "\\img\\headphones\\wired", Image.FileName);

                using var fileStream = new FileStream(path, FileMode.Create);
                Image.CopyTo(fileStream);
            }
            headphones.Brand = item.Brand;
            headphones.Model = item.Model;
            headphones.Type = item.Type;
            headphones.Connectivity = "Жични";
            headphones.Colour = item.Colour;
            headphones.Price = item.Price;

            headphonesRepository.AddWiredHeadphones(headphones);

            return RedirectToAction("HeadphonesList", "Admin");

        }

        [HttpGet]
        public IActionResult CreateWirelessHeadphones()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateWirelessHeadphones(CreateWirelessHeadphonesVM item, IFormFile Image)
        {

            WirelessHeadphonesRepository headphonesRepository = new WirelessHeadphonesRepository();
            WirelessHeadphones headphones = new WirelessHeadphones();
            if (Image != null)
            {
                headphones.Image = Image.FileName.Substring(0, Image.FileName.Length - 4);

                var directoryPath = Path.Combine(_webHostEnvironment.WebRootPath + "\\img\\headphones\\wireless");

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                var path = Path.Combine(_webHostEnvironment.WebRootPath + "\\img\\headphones\\wireless", Image.FileName);

                using var fileStream = new FileStream(path, FileMode.Create);
                Image.CopyTo(fileStream);
            }
          
            headphones.Brand = item.Brand;
            headphones.Model = item.Model;
            headphones.Connectivity = "Безжични";
            headphones.Type = item.Type;
            headphones.Colour = item.Colour;
            headphones.BatteryLife = item.BatteryLife;
            headphones.BatteryLifeWithCase = item.BatteryLifeWithCase;
            headphones.Price = item.Price;

            headphonesRepository.AddWirelessHeadphones(headphones);

            return RedirectToAction("HeadphonesList", "Admin");

        }

        public IActionResult DeleteWiredHeadphones(int id)
        {
            WiredHeadphonesRepository headphonesRepository = new WiredHeadphonesRepository();

            headphonesRepository.RemoveWiredHeadphones(id);

            return RedirectToAction("HeadphonesList", "Admin");
        }

        public IActionResult DeleteWirelessHeadphones(int id)
        {
            WirelessHeadphonesRepository headphonesRepository = new WirelessHeadphonesRepository();

            headphonesRepository.RemoveWirelessHeadphones(id);
            applicationDbContext.SaveChanges();

            return RedirectToAction("HeadphonesList", "Admin");
        }

        [HttpGet]
        public IActionResult UpdateWiredHeadphones(int id)
        {
            ApplicationDbContext applicationDbContext = new ApplicationDbContext();
            WiredHeadphones headphones = applicationDbContext.WiredHeadphones.Find(id);
            EditWiredHeadphonesVM item = new EditWiredHeadphonesVM();

            item.Id = headphones.Id;
            item.Brand = headphones.Brand;
            item.Model = headphones.Model;
            item.Type = headphones.Type;
            item.Colour = headphones.Colour;
            item.Price = headphones.Price;

            return View(item);
        }
        [HttpPost]
        public IActionResult UpdateWiredHeadphones(EditWiredHeadphonesVM item)
        {
            WiredHeadphonesRepository headphonesRepository = new WiredHeadphonesRepository();
            if (!ModelState.IsValid)
            {
                return RedirectToAction("HeadphonesList", "Admin");
            }

            headphonesRepository.UpdateWiredHeadphones(item);

            return RedirectToAction("HeadphonesList", "Admin");
        }

        [HttpGet]
        public IActionResult UpdateWirelessHeadphones(int id)
        {
            ApplicationDbContext applicationDbContext = new ApplicationDbContext();
            WirelessHeadphones headphones = applicationDbContext.WirelessHeadphones.Find(id);
            EditWirelessHeadphonesVM item = new EditWirelessHeadphonesVM();

            item.Id = headphones.Id;
            item.Brand = headphones.Brand;
            item.Model = headphones.Model;
            item.Type = headphones.Type;
            item.Colour = headphones.Colour;
            item.BatteryLife = headphones.BatteryLife;
            item.BatteryLifeWithCase = headphones.BatteryLifeWithCase;
            item.Price = headphones.Price;

            return View(item);
        }
        [HttpPost]
        public IActionResult UpdateWirelessHeadphones(EditWirelessHeadphonesVM item)
        {
            WirelessHeadphonesRepository headphonesRepository = new WirelessHeadphonesRepository();
            if (!ModelState.IsValid)
            {
                return RedirectToAction("HeadphonesList", "Admin");
            }

            headphonesRepository.UpdateWirelessHeadphones(item);

            return RedirectToAction("HeadphonesList", "Admin");
        }

        public IActionResult SmartwatchList(DisplaySmartwatchVM model)
        {
            model.Filter ??= new FilterSmartwatchVM();
          
            var filter = model.Filter.GetFilter();

            SmartwatchRepository smartwatchRepository = new SmartwatchRepository();
            model.Smartwatches = smartwatchRepository.GetAllSmartwatches(filter);
       
            return View(model);
        }


        [HttpGet]
        public IActionResult CreateSmartwatch()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateSmartwatch(CreateSmartwatchVM item, IFormFile Image)
        {
            SmartwatchRepository smartwatchRepository = new SmartwatchRepository();
            Smartwatch smartwatch = new Smartwatch();

            if (Image != null)
            {
                smartwatch.Image = Image.FileName.Substring(0, Image.FileName.Length - 4);

                var directoryPath = Path.Combine(_webHostEnvironment.WebRootPath + "\\img\\smartwatches");
                var path = Path.Combine(_webHostEnvironment.WebRootPath + "\\img\\smartwatches", Image.FileName);

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                using var fileStream = new FileStream(path, FileMode.Create);
                Image.CopyTo(fileStream);
            }
            smartwatch.Brand = item.Brand;
            smartwatch.Model = item.Model;
            smartwatch.Weight = item.Weight;
            smartwatch.Colour = item.Colour;
            smartwatch.BatteryLife = item.BatteryLife;
            smartwatch.DisplaySize = item.DisplaySize;
            smartwatch.DisplayTechnology = item.DisplayTechnology;
            smartwatch.Weight = item.Weight;
            smartwatch.Price = item.Price;

            smartwatchRepository.AddSmartwatch(smartwatch);

            return RedirectToAction("SmartwatchList", "Admin");
        }

        public IActionResult DeleteSmartwatch(int id)
        {
            SmartwatchRepository smartwatchRepository = new SmartwatchRepository();

            smartwatchRepository.RemoveSmartwatch(id);

            return RedirectToAction("SmartwatchList", "Admin");

        }

        [HttpGet]
        public IActionResult UpdateSmartwatch(int id)
        {
            ApplicationDbContext applicationDbContext = new ApplicationDbContext();
            Smartwatch smartwatch = applicationDbContext.Smartwatches.Find(id);
            EditSmartwatchVM item = new EditSmartwatchVM();

            item.Id = smartwatch.Id;
            item.Brand = smartwatch.Brand;
            item.Model = smartwatch.Model;
            item.BatteryLife = smartwatch.BatteryLife;
            item.DisplaySize = smartwatch.DisplaySize;
            item.DisplayTechnology = smartwatch.DisplayTechnology;
            item.Weight = smartwatch.Weight;
            item.Colour = smartwatch.Colour;
            item.Price = smartwatch.Price;

            return View(item);
        }
        [HttpPost]
        public IActionResult UpdateSmartwatch(EditSmartwatchVM item)
        {
            SmartwatchRepository smartwatchRepository = new SmartwatchRepository();
            if (!ModelState.IsValid)
            {
                return RedirectToAction("SmartwatchList", "Admin");
            }

            smartwatchRepository.UpdateSmartwatch(item);

            return RedirectToAction("SmartwatchList", "Admin");
        }
        public IActionResult PhoneCaseList(DisplayPhoneCaseVM model)
        {
            model.Filter ??= new FilterPhoneCaseVM();

            var filter = model.Filter.GetFilter();

            PhoneCaseRepository phoneCaseRepository = new PhoneCaseRepository();
            model.PhoneCases = phoneCaseRepository.GetAllPhoneCases(filter);
         
            return View(model);
        }

        [HttpGet]
        public IActionResult CreatePhoneCase()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreatePhoneCase(CreatePhoneCaseVM item, IFormFile Image)
        {
            PhoneCaseRepository phoneCaseRepository = new PhoneCaseRepository();
            PhoneCase phoneCase = new PhoneCase();

            if (Image != null)
            {
                phoneCase.Image = Image.FileName.Substring(0, Image.FileName.Length - 4);

                var directoryPath = Path.Combine(_webHostEnvironment.WebRootPath + "\\img\\phonecases");
                var path = Path.Combine(_webHostEnvironment.WebRootPath + "\\img\\phonecases", Image.FileName);

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                using var fileStream = new FileStream(path, FileMode.Create);
                Image.CopyTo(fileStream);
            }

            phoneCase.Colour = item.Colour;
            phoneCase.Brand = item.Brand;
            phoneCase.FitFor = item.FitFor;
            phoneCase.Price = item.Price;

            phoneCaseRepository.AddPhoneCase(phoneCase);

            return RedirectToAction("PhoneCaseList", "Admin");
        }

        public IActionResult DeletePhoneCase(int id)
        {
            PhoneCaseRepository phoneCaseRepository = new PhoneCaseRepository();
            phoneCaseRepository.RemovePhoneCase(id);

            return RedirectToAction("PhoneCaseList", "Admin");
        }

        [HttpGet]
        public IActionResult UpdatePhoneCase(int id)
        {
            ApplicationDbContext applicationDbContext = new ApplicationDbContext();
            PhoneCase phoneCase = applicationDbContext.PhoneCases.Find(id);
            EditPhoneCaseVM item = new EditPhoneCaseVM();

            item.Id = phoneCase.Id;
            item.Brand = phoneCase.Brand;
            item.Colour = phoneCase.Colour;
            item.FitFor = phoneCase.FitFor;
            item.Price = phoneCase.Price;

            return View(item);
        }
        [HttpPost]
        public IActionResult UpdatePhoneCase(EditPhoneCaseVM item)
        {
            PhoneCaseRepository phoneCaseRepository = new PhoneCaseRepository();
            if (!ModelState.IsValid)
            {
                return RedirectToAction("PhoneCaseList", "Admin");
            }

            phoneCaseRepository.UpdatePhoneCase(item);

            return RedirectToAction("PhoneCaseList", "Admin");
        }

        public IActionResult PhoneProtectorList(DisplayPhoneProtectorVM model)
        {
            model.Filter ??= new FilterPhoneProtectorVM();

            var filter = model.Filter.GetFilter();

            PhoneProtectorRepository phoneProtectorRepository = new PhoneProtectorRepository();
            model.PhoneProtectors = phoneProtectorRepository.GetAllPhoneProtectors(filter);
        
            return View(model);
        }

        [HttpGet]
        public IActionResult CreatePhoneProtector()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreatePhoneProtector(CreatePhoneProtectorVM item, IFormFile Image)
        {
            PhoneProtectorRepository phoneProtectorRepository = new PhoneProtectorRepository();
            PhoneProtector phoneProtector = new PhoneProtector();

            if (Image != null)
            {
                phoneProtector.Image = Image.FileName.Substring(0, Image.FileName.Length - 4);

                var directoryPath = Path.Combine(_webHostEnvironment.WebRootPath + "\\img\\phoneprotectors");
                var path = Path.Combine(_webHostEnvironment.WebRootPath + "\\img\\phoneprotectors", Image.FileName);

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                using var fileStream = new FileStream(path, FileMode.Create);
                Image.CopyTo(fileStream);
            }
            
            phoneProtector.Brand = item.Brand;
            phoneProtector.FitFor = item.FitFor;
            phoneProtector.Price = item.Price;

            phoneProtectorRepository.AddPhoneProtector(phoneProtector);

            return RedirectToAction("PhoneProtectorList", "Admin");
        }

        public IActionResult DeletePhoneProtector(int id)
        {
            PhoneProtectorRepository phoneProtectorRepository = new PhoneProtectorRepository();
            phoneProtectorRepository.RemovePhoneProtector(id);

            return RedirectToAction("PhoneProtectorList", "Admin");
        }

        [HttpGet]
        public IActionResult UpdatePhoneProtector(int id)
        {
            ApplicationDbContext applicationDbContext = new ApplicationDbContext();
            PhoneProtector phoneProtector = applicationDbContext.PhoneProtectors.Find(id);
            EditPhoneProtectorVM item = new EditPhoneProtectorVM();

            item.Id = phoneProtector.Id;
            item.Brand = phoneProtector.Brand;
            item.FitFor = phoneProtector.FitFor;
            item.Price = phoneProtector.Price;

            return View(item);
        }
        [HttpPost]
        public IActionResult UpdatePhoneProtector(EditPhoneProtectorVM item)
        {
            PhoneProtectorRepository phoneProtectorRepository = new PhoneProtectorRepository();
            if (!ModelState.IsValid)
            {
                return RedirectToAction("PhoneCaseList", "Admin");
            }

            phoneProtectorRepository.UpdatePhoneProtector(item);

            return RedirectToAction("PhoneProtectorList", "Admin");
        }
    }
}
