using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MobilePhonesWebsite.Data;
using MobilePhonesWebsite.Models;
using MobilePhonesWebsite.ViewModels.MobilePhoneVM;
using MobilePhonesWebsite.ViewModels.HeadphonesVM;
using MobilePhonesWebsite.ViewModels.SmartwatchVM;
using MobilePhonesWebsite.ViewModels.PhoneCaseVM;
using MobilePhonesWebsite.ViewModels.PhoneProtectorVM;
using MobilePhonesWebsite.ViewModels.SharedVM;
using MobilePhonesWebsite.ViewModels.UserVM;
using MobilePhonesWebsite.Repository;
using System.Security.Claims;
using MobilePhonesWebsite.ViewModels.OrderVM;
using static MobilePhonesWebsite.Enumerators.OrderEnum;
using MobilePhonesWebsite.ViewModels.ProductsVM;

namespace MobilePhonesWebsite.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private ApplicationDbContext applicationDbContext;
        private readonly MobilePhonesRepository mobilePhonesRepository;
        private readonly WiredHeadphonesRepository wiredHeadphonesRepository;
        private readonly WirelessHeadphonesRepository wirelessHeadphonesRepository;
        private readonly SmartwatchRepository smartwatchRepository;
        private readonly PhoneCaseRepository phoneCaseRepository;
        private readonly PhoneProtectorRepository phoneProtectorRepository;
        private readonly CartRepository cartRepository;
        private readonly LikedProductRepository likedProductRepository;
        private readonly OrderRepository orderRepository;

        public ProductsController(IWebHostEnvironment webHost)
        {
            webHostEnvironment = webHost;
            applicationDbContext = new ApplicationDbContext();
            mobilePhonesRepository = new MobilePhonesRepository();
            wiredHeadphonesRepository = new WiredHeadphonesRepository();
            wirelessHeadphonesRepository = new WirelessHeadphonesRepository();
            smartwatchRepository = new SmartwatchRepository();
            phoneCaseRepository = new PhoneCaseRepository();
            phoneProtectorRepository = new PhoneProtectorRepository();
            cartRepository = new CartRepository();
            likedProductRepository = new LikedProductRepository();
            orderRepository = new OrderRepository();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MobilePhonesPage(DisplayMobilePhoneVM model)
        {
            model.Filter ??= new FilterMobilePhoneVM();

            List<string> Brand = new List<string>();
            if (model.Brand1)
            {
                Brand.Add("Apple");
            }
            if (model.Brand2)
            {
                Brand.Add("Samsung");
            }
            if (model.Brand3)
            {
                Brand.Add("Xiaomi");
            }
            if (model.Brand4)
            {
                Brand.Add("Huawei");
            }

            List<int> StorageSpace = new List<int>();
            if (model.StorageSpace1)
            {
                StorageSpace.Add(512);
            }
            if (model.StorageSpace2)
            {
                StorageSpace.Add(256);
            }
            if (model.StorageSpace3)
            {
                StorageSpace.Add(128);
            }
            if (model.StorageSpace4)
            {
                StorageSpace.Add(64);
            }

            List<int> OperatingMemory = new List<int>();
            if (model.OperatingMemory1)
            {
                OperatingMemory.Add(12);
            }
            if (model.OperatingMemory2)
            {
                OperatingMemory.Add(8);
            }
            if (model.OperatingMemory3)
            {
                OperatingMemory.Add(6);
            }
            if (model.OperatingMemory4)
            {
                OperatingMemory.Add(4);
            }

            model.Filter.Brand = Brand;
            model.Filter.StorageSpace = StorageSpace;
            model.Filter.OperatingMemory = OperatingMemory;
            model.Filter.MinPrice = model.MinPrice;
            model.Filter.MaxPrice = model.MaxPrice;

            var filter = model.Filter.GetFilter();

            model.MobilePhones = mobilePhonesRepository.GetAllMobilePhones(filter);
            return View(model);
        }
        public FileResult GetFirstImageMobilePhones(int id)
        {
            MobilePhone mobilePhone = applicationDbContext.MobilePhones.Find(id);

            string rootPath = webHostEnvironment.WebRootPath;
            var path = Path.Combine(rootPath + "\\img\\mobilephones", mobilePhone.Image1 + ".png");

            byte[] imageByteData = System.IO.File.ReadAllBytes(path);
            return File(imageByteData, "image/png");
        }
        public FileResult GetSecondImageMobilePhones(int id)
        {
            MobilePhone mobilePhone = applicationDbContext.MobilePhones.Find(id);

            string rootPath = webHostEnvironment.WebRootPath;
            var path = Path.Combine(rootPath + "\\img\\mobilephones", mobilePhone.Image2 + ".png");

            byte[] imageByteData = System.IO.File.ReadAllBytes(path);
            return File(imageByteData, "image/png");
        }

        public IActionResult SingleMobilePhonePage(int id)
        {
            MobilePhone mobilePhone = applicationDbContext.MobilePhones.Find(id);
            MobilePhoneProcessor processor = applicationDbContext.MobilePhoneProcessors.Find(mobilePhone.MobilePhoneProcessorId);
            MobilePhoneDisplay display = applicationDbContext.MobilePhoneDisplays.Find(mobilePhone.MobilePhoneDisplayId);
            MobilePhoneCamera camera = applicationDbContext.MobilePhoneCameras.Find(mobilePhone.MobilePhoneCameraId);

            SingleMobilePhoneVM mobilePhoneVM = new SingleMobilePhoneVM();

            mobilePhoneVM.phone = mobilePhone;
            mobilePhoneVM.processor = processor;
            mobilePhoneVM.display = display;
            mobilePhoneVM.camera = camera;

            return View(mobilePhoneVM);
        }



        [HttpPost]
        public async Task AddToCartMobilePhone(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                CookieOptions cookieOptions = new CookieOptions();
                cookieOptions.Expires = DateTimeOffset.Now.AddMonths(1);
                Response.Cookies.Append("cartItemPhone" + id.ToString(), id.ToString(), cookieOptions);
            }
            else
            {
                Cart cart = cartRepository.CheckForExisting(Convert.ToInt32(User.FindFirst(ClaimTypes.Sid).Value), id, "mobilePhone");
                if (cart == null)
                {
                    Cart item = new Cart();

                    item.ProductId = id;
                    item.ProductType = "mobilePhone";
                    item.UserId = Convert.ToInt32(User.FindFirst(ClaimTypes.Sid).Value);
                    item.Quantity = 1;
                    item.Image = mobilePhonesRepository.GetMobilePhoneImageLink(item.ProductId);
                    item.PriceForOne = mobilePhonesRepository.GetMobilePhonePrice(item.ProductId);
                    item.Price = mobilePhonesRepository.GetMobilePhonePrice(item.ProductId);
                    item.ProductName = mobilePhonesRepository.GetMobilePhoneName(item.ProductId);
                    cartRepository.AddToCart(item);
                }
                else
                {
                    int newQuantity = cart.Quantity + 1;
                    cart.Quantity = newQuantity;
                    await cartRepository.UpdateCartItemAsync(cart);
                }

            }
        }

        [HttpPost]
        public async Task AddToLikedMobilePhone(int id)
        {

            LikedProduct likedProduct = likedProductRepository.CheckForExisting(Convert.ToInt32(User.FindFirst(ClaimTypes.Sid).Value), id, "mobilePhone");
            if (likedProduct == null)
            {
                LikedProduct item = new LikedProduct();

                item.ProductId = id;
                item.ProductType = "mobilePhone";
                item.UserId = Convert.ToInt32(User.FindFirst(ClaimTypes.Sid).Value);
                item.Image = mobilePhonesRepository.GetMobilePhoneImageLink(item.ProductId);
                item.Price = mobilePhonesRepository.GetMobilePhonePrice(item.ProductId);
                item.ProductName = mobilePhonesRepository.GetMobilePhoneName(item.ProductId);
                likedProductRepository.AddToLikedProducts(item);
            }
            else
            {
                BadRequest("Продукта вече е добавен в харесани!");
            }

        }


        public IActionResult HeadphonesPage(DisplayHeadphonesVM model)
        {
            List<string> Brand = new List<string>();
            if (model.Brand1)
            {
                Brand.Add("Apple");
            }
            if (model.Brand2)
            {
                Brand.Add("Samsung");
            }
            if (model.Brand3)
            {
                Brand.Add("Huawei");
            }
            if (model.Brand4)
            {
                Brand.Add("Beats");
            }

            model.Filter ??= new FilterHeadphonesVM();
            model.Filter.Brand = Brand;
            model.Filter.MinPrice = model.MinPrice;
            model.Filter.MaxPrice = model.MaxPrice;

            var filter = model.Filter.GetFilter();
            var filterTwo = model.Filter.GetFilterForWireless();

            model.WiredHeadphones = wiredHeadphonesRepository.GetAllWiredHeadphones(filter);
            model.WirelessHeadphones = wirelessHeadphonesRepository.GetAllWirelessHeadphones(filterTwo);
            
            return View(model);
        }

        public FileResult GetImageWiredHeadphones(int id)
        {
            WiredHeadphones wiredHeadphones = applicationDbContext.WiredHeadphones.Find(id);

            string rootPath = webHostEnvironment.WebRootPath;
            var path = Path.Combine(rootPath + "\\img\\headphones\\wired", wiredHeadphones.Image + ".png");

            byte[] imageByteData = System.IO.File.ReadAllBytes(path);
            return File(imageByteData, "image/png");
        }

        public FileResult GetImageWirelessHeadphones(int id)
        {
            WirelessHeadphones wirelessHeadphones = applicationDbContext.WirelessHeadphones.Find(id);

            string rootPath = webHostEnvironment.WebRootPath;
            var path = Path.Combine(rootPath + "\\img\\headphones\\wireless", wirelessHeadphones.Image + ".png");

            byte[] imageByteData = System.IO.File.ReadAllBytes(path);
            return File(imageByteData, "image/png");
        }

        public IActionResult SingleWiredHeadphonesPage(int id)
        {
            WiredHeadphones item = applicationDbContext.WiredHeadphones.Find(id);

            SingleWiredHeadphonesVM headphones = new SingleWiredHeadphonesVM();

            headphones.Id = item.Id;
            headphones.Brand = item.Brand;
            headphones.Model = item.Model;
            headphones.Type = item.Type;
            headphones.Colour = item.Colour;
            headphones.Price = item.Price;

            return View(headphones);
        }



        [HttpPost]
        public async Task AddToCartWiredHeadphones(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                CookieOptions cookieOptions = new CookieOptions();
                cookieOptions.Expires = DateTimeOffset.Now.AddMonths(1);
                Response.Cookies.Append("cartItemWiredHeadphones" + id.ToString(), id.ToString(), cookieOptions);
            }
            else
            {
                Cart cart = cartRepository.CheckForExisting(Convert.ToInt32(User.FindFirst(ClaimTypes.Sid).Value), id, "wiredHeadphones");
                if (cart == null)
                {
                    Cart item = new Cart();

                    item.ProductId = id;
                    item.ProductType = "wiredHeadphones";
                    item.UserId = Convert.ToInt32(User.FindFirst(ClaimTypes.Sid).Value);
                    item.Quantity = 1;
                    item.Image = wiredHeadphonesRepository.GetWiredHeadphonesImageLink(item.ProductId);
                    item.PriceForOne = wiredHeadphonesRepository.GetWiredHeadphonesPrice(item.ProductId);
                    item.Price = wiredHeadphonesRepository.GetWiredHeadphonesPrice(item.ProductId);
                    item.ProductName = wiredHeadphonesRepository.GetWiredHeadphonesName(item.ProductId);
                    cartRepository.AddToCart(item);
                }
                else
                {
                    int newQuantity = cart.Quantity + 1;
                    cart.Quantity = newQuantity;
                    await cartRepository.UpdateCartItemAsync(cart);
                }

            }
        }

        [HttpPost]
        public async Task AddToLikedWiredHeadphones(int id)
        {

            LikedProduct likedProduct = likedProductRepository.CheckForExisting(Convert.ToInt32(User.FindFirst(ClaimTypes.Sid).Value), id, "wiredHeadphones");
            if (likedProduct == null)
            {
                LikedProduct item = new LikedProduct();

                item.ProductId = id;
                item.ProductType = "wiredHeadphones";
                item.UserId = Convert.ToInt32(User.FindFirst(ClaimTypes.Sid).Value);
                item.Image = wiredHeadphonesRepository.GetWiredHeadphonesImageLink(item.ProductId);
                item.Price = wiredHeadphonesRepository.GetWiredHeadphonesPrice(item.ProductId);
                item.ProductName = wiredHeadphonesRepository.GetWiredHeadphonesName(item.ProductId);
                likedProductRepository.AddToLikedProducts(item);
            }
            else
            {
                BadRequest("Продукта вече е добавен в харесани продукти");
            }

        }

        public IActionResult SingleWirelessHeadphonesPage(int id)
        {
            WirelessHeadphones item = applicationDbContext.WirelessHeadphones.Find(id);

            SingleWirelessHeadphonesVM headphones = new SingleWirelessHeadphonesVM();

            headphones.Id = item.Id;
            headphones.Brand = item.Brand;
            headphones.Model = item.Model;
            headphones.Type = item.Type;
            headphones.Colour = item.Colour;
            headphones.BatteryLife = item.BatteryLife;
            headphones.BatteryLifeWithCase = item.BatteryLifeWithCase;
            headphones.Price = item.Price;

            return View(headphones);
        }



        [HttpPost]
        public async Task AddToCartWirelessHeadphones(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                CookieOptions cookieOptions = new CookieOptions();
                cookieOptions.Expires = DateTimeOffset.Now.AddMonths(1);
                Response.Cookies.Append("cartItemWirelessHeadphones" + id.ToString(), id.ToString(), cookieOptions);
            }
            else
            {
                Cart cart = cartRepository.CheckForExisting(Convert.ToInt32(User.FindFirst(ClaimTypes.Sid).Value), id, "wirelessHeadphones");
                if (cart == null)
                {
                    Cart item = new Cart();

                    item.ProductId = id;
                    item.ProductType = "wirelessHeadphones";
                    item.UserId = Convert.ToInt32(User.FindFirst(ClaimTypes.Sid).Value);
                    item.Quantity = 1;
                    item.Image = wirelessHeadphonesRepository.GetWirelessHeadphonesImageLink(item.ProductId);
                    item.PriceForOne = wirelessHeadphonesRepository.GetWirelessHeadphonesPrice(item.ProductId);
                    item.Price = wirelessHeadphonesRepository.GetWirelessHeadphonesPrice(item.ProductId);
                    item.ProductName = wirelessHeadphonesRepository.GetWirelessHeadphonesName(item.ProductId);
                    cartRepository.AddToCart(item);
                }
                else
                {
                    int newQuantity = cart.Quantity + 1;
                    cart.Quantity = newQuantity;
                    await cartRepository.UpdateCartItemAsync(cart);
                }

            }
        }

        [HttpPost]
        public async Task AddToLikedWirelessHeadphones(int id)
        {

            LikedProduct likedProduct = likedProductRepository.CheckForExisting(Convert.ToInt32(User.FindFirst(ClaimTypes.Sid).Value), id, "wiredHeadphones");
            if (likedProduct == null)
            {
                LikedProduct item = new LikedProduct();

                item.ProductId = id;
                item.ProductType = "wirelessHeadphones";
                item.UserId = Convert.ToInt32(User.FindFirst(ClaimTypes.Sid).Value);
                item.Image = wirelessHeadphonesRepository.GetWirelessHeadphonesImageLink(item.ProductId);
                item.Price = wirelessHeadphonesRepository.GetWirelessHeadphonesPrice(item.ProductId);
                item.ProductName = wirelessHeadphonesRepository.GetWirelessHeadphonesName(item.ProductId);
                likedProductRepository.AddToLikedProducts(item);
            }
            else
            {
                BadRequest("Продукта вече е добавен в харесани продукти");
            }

        }

        public IActionResult SmartwatchesPage(DisplaySmartwatchVM model)
        {
            model.Filter ??= new FilterSmartwatchVM();

            List<string> Brand = new List<string>();
            if (model.Brand1)
            {
                Brand.Add("Apple");
            }
            if (model.Brand2)
            {
                Brand.Add("Huawei");
            }
            if (model.Brand3)
            {
                Brand.Add("Xiaomi");
            }
            if (model.Brand4)
            {
                Brand.Add("Samsung");
            }

            model.Filter.Brand = Brand;

            model.Filter.MinPrice = model.MinPrice;
            model.Filter.MaxPrice = model.MaxPrice;

            var filter = model.Filter.GetFilter();

            SmartwatchRepository smartwatchRepository = new SmartwatchRepository();
            model.Smartwatches = smartwatchRepository.GetAllSmartwatches(filter);
            return View(model);
        }


        public FileResult GetImageSmartwatch(int id)
        {
            Smartwatch smartwatch = applicationDbContext.Smartwatches.Find(id);

            string rootPath = webHostEnvironment.WebRootPath;
            var path = Path.Combine(rootPath + "\\img\\smartwatches", smartwatch.Image + ".png");

            byte[] imageByteData = System.IO.File.ReadAllBytes(path);
            return File(imageByteData, "image/png");
        }


        public IActionResult SingleSmartwatchPage(int id)
        {
            Smartwatch item = applicationDbContext.Smartwatches.Find(id);

            SingleSmartwatchVM smartwatch = new SingleSmartwatchVM();

            smartwatch.Id = item.Id;
            smartwatch.Brand = item.Brand;
            smartwatch.Model = item.Model;
            smartwatch.Colour = item.Colour;
            smartwatch.Weight = item.Weight;
            smartwatch.BatteryLife = item.BatteryLife;
            smartwatch.DisplaySize = item.DisplaySize;
            smartwatch.DisplayTechnology = item.DisplayTechnology;
            smartwatch.Price = item.Price;

            return View(smartwatch);
        }


        [HttpPost]
        public async Task AddToCartSmartwatch(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                CookieOptions cookieOptions = new CookieOptions();
                cookieOptions.Expires = DateTimeOffset.Now.AddMonths(1);
                Response.Cookies.Append("cartItemSmartwatch" + id.ToString(), id.ToString(), cookieOptions);
            }
            else
            {
                Cart cart = cartRepository.CheckForExisting(Convert.ToInt32(User.FindFirst(ClaimTypes.Sid).Value), id, "smartwatch");
                if (cart == null)
                {
                    Cart item = new Cart();

                    item.ProductId = id;
                    item.ProductType = "smartwatch";
                    item.UserId = Convert.ToInt32(User.FindFirst(ClaimTypes.Sid).Value);
                    item.Quantity = 1;
                    item.Image = smartwatchRepository.GetSmartwatchImageLink(item.ProductId);
                    item.PriceForOne = smartwatchRepository.GetSmartwatchPrice(item.ProductId);
                    item.Price = smartwatchRepository.GetSmartwatchPrice(item.ProductId);
                    item.ProductName = smartwatchRepository.GetSmartwatchName(item.ProductId);
                    cartRepository.AddToCart(item);
                }
                else
                {
                    int newQuantity = cart.Quantity + 1;
                    cart.Quantity = newQuantity;
                    await cartRepository.UpdateCartItemAsync(cart);
                }

            }
        }

        [HttpPost]
        public async Task AddToLikedSmartwatch(int id)
        {

            LikedProduct likedProduct = likedProductRepository.CheckForExisting(Convert.ToInt32(User.FindFirst(ClaimTypes.Sid).Value), id, "smartwatch");
            if (likedProduct == null)
            {
                LikedProduct item = new LikedProduct();

                item.ProductId = id;
                item.ProductType = "smartwatch";
                item.UserId = Convert.ToInt32(User.FindFirst(ClaimTypes.Sid).Value);
                item.Image = smartwatchRepository.GetSmartwatchImageLink(item.ProductId);
                item.Price = smartwatchRepository.GetSmartwatchPrice(item.ProductId);
                item.ProductName = smartwatchRepository.GetSmartwatchName(item.ProductId);
                likedProductRepository.AddToLikedProducts(item);
            }
            else
            {
                BadRequest("Продукта вече е добавен в харесани продукти");
            }
        }

        public IActionResult PhoneCasesPage(DisplayPhoneCaseVM model)
        {
            model.Filter ??= new FilterPhoneCaseVM();

            model.Filter.MinPrice = model.MinPrice;
            model.Filter.MaxPrice = model.MaxPrice;

            var filter = model.Filter.GetFilter();

            PhoneCaseRepository phoneCaseRepository = new PhoneCaseRepository();
            model.PhoneCases = phoneCaseRepository.GetAllPhoneCases(filter);

            return View(model);
        }

        public FileResult GetImagePhoneCase(int id)
        {
            PhoneCase phoneCase = applicationDbContext.PhoneCases.Find(id);

            string rootPath = webHostEnvironment.WebRootPath;
            var path = Path.Combine(rootPath + "\\img\\phonecases", phoneCase.Image + ".png");

            byte[] imageByteData = System.IO.File.ReadAllBytes(path);
            return File(imageByteData, "image/png");
        }

        public IActionResult SinglePhoneCasePage(int id)
        {
            PhoneCase item = applicationDbContext.PhoneCases.Find(id);

            SinglePhoneCaseVM phoneCase = new SinglePhoneCaseVM();

            phoneCase.Id = id;
            phoneCase.Colour = item.Colour;
            phoneCase.Brand = item.Brand;
            phoneCase.FitFor = item.FitFor;
            phoneCase.Price = item.Price;

            return View(phoneCase);
        }



        [HttpPost]
        public async Task AddToCartPhoneCase(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                CookieOptions cookieOptions = new CookieOptions();
                cookieOptions.Expires = DateTimeOffset.Now.AddMonths(1);
                Response.Cookies.Append("cartItemCase" + id.ToString(), id.ToString(), cookieOptions);
            }
            else
            {
                Cart cart = cartRepository.CheckForExisting(Convert.ToInt32(User.FindFirst(ClaimTypes.Sid).Value), id, "phoneCase");
                if (cart == null)
                {
                    Cart item = new Cart();

                    item.ProductId = id;
                    item.ProductType = "phoneCase";
                    item.UserId = Convert.ToInt32(User.FindFirst(ClaimTypes.Sid).Value);
                    item.Quantity = 1;
                    item.Image = phoneCaseRepository.GetPhoneCaseImageLink(item.ProductId);
                    item.PriceForOne = phoneCaseRepository.GetPhoneCasePrice(item.ProductId);
                    item.Price = phoneCaseRepository.GetPhoneCasePrice(item.ProductId);
                    item.ProductName = phoneCaseRepository.GetPhoneCaseName(item.ProductId);
                    cartRepository.AddToCart(item);
                }
                else
                {
                    int newQuantity = cart.Quantity + 1;
                    cart.Quantity = newQuantity;
                    await cartRepository.UpdateCartItemAsync(cart);

                }
            }
        }

        [HttpPost]
        public async Task AddToLikedPhoneCase(int id)
        {

            LikedProduct likedProduct = likedProductRepository.CheckForExisting(Convert.ToInt32(User.FindFirst(ClaimTypes.Sid).Value), id, "phoneCase");
            if (likedProduct == null)
            {
                LikedProduct item = new LikedProduct();

                item.ProductId = id;
                item.ProductType = "phoneCase";
                item.UserId = Convert.ToInt32(User.FindFirst(ClaimTypes.Sid).Value);
                item.Image = phoneCaseRepository.GetPhoneCaseImageLink(item.ProductId);
                item.Price = phoneCaseRepository.GetPhoneCasePrice(item.ProductId);
                item.ProductName = phoneCaseRepository.GetPhoneCaseName(item.ProductId);
                likedProductRepository.AddToLikedProducts(item);
            }
            else
            {
                BadRequest("Продукта вече е добавен в харесани продукти");
            }
        }

        public IActionResult PhoneProtectorsPage(DisplayPhoneProtectorVM model)
        {
            model.Filter ??= new FilterPhoneProtectorVM();
            model.Filter.MinPrice = model.MinPrice;
            model.Filter.MaxPrice = model.MaxPrice;

            var filter = model.Filter.GetFilter();

            PhoneProtectorRepository phoneProtectorRepository = new PhoneProtectorRepository();
            model.PhoneProtectors = phoneProtectorRepository.GetAllPhoneProtectors(filter);

            return View(model);
        }
        public FileResult GetImagePhoneProtector(int id)
        {
            PhoneProtector phoneProtector = applicationDbContext.PhoneProtectors.Find(id);

            string rootPath = webHostEnvironment.WebRootPath;
            var path = Path.Combine(rootPath + "\\img\\phoneprotectors", phoneProtector.Image + ".png");

            byte[] imageByteData = System.IO.File.ReadAllBytes(path);
            return File(imageByteData, "image/png");
        }

        public IActionResult SinglePhoneProtectorPage(int id)
        {
            PhoneProtector item = applicationDbContext.PhoneProtectors.Find(id);

            SinglePhoneProtectorVM phoneProtector = new SinglePhoneProtectorVM();

            phoneProtector.Id = id;
            phoneProtector.Brand = item.Brand;
            phoneProtector.FitFor = item.FitFor;
            phoneProtector.Price = item.Price;

            return View(phoneProtector);
        }


        [HttpPost]
        public async Task AddToCartPhoneProtector(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                CookieOptions cookieOptions = new CookieOptions();
                cookieOptions.Expires = DateTimeOffset.Now.AddMonths(1);
                Response.Cookies.Append("cartItemProtector" + id.ToString(), id.ToString(), cookieOptions);
            }
            else
            {
                Cart cart = cartRepository.CheckForExisting(Convert.ToInt32(User.FindFirst(ClaimTypes.Sid).Value), id, "phoneProtector");
                if (cart == null)
                {
                    Cart item = new Cart();

                    item.ProductId = id;
                    item.ProductType = "phoneProtector";
                    item.UserId = Convert.ToInt32(User.FindFirst(ClaimTypes.Sid).Value);
                    item.Quantity = 1;
                    item.Image = phoneProtectorRepository.GetPhoneProtectorImageLink(item.ProductId);
                    item.PriceForOne = phoneProtectorRepository.GetPhoneProtectorPrice(item.ProductId);
                    item.Price = phoneProtectorRepository.GetPhoneProtectorPrice(item.ProductId);
                    item.ProductName = phoneProtectorRepository.GetPhoneProtectorName(item.ProductId);
                    cartRepository.AddToCart(item);
                }
                else
                {
                    int newQuantity = cart.Quantity + 1;
                    cart.Quantity = newQuantity;
                    await cartRepository.UpdateCartItemAsync(cart);
                }

            }
        }

        [HttpPost]
        public async Task AddToLikedPhoneProtector(int id)
        {

            LikedProduct likedProduct = likedProductRepository.CheckForExisting(Convert.ToInt32(User.FindFirst(ClaimTypes.Sid).Value), id, "phoneProtector");
            if (likedProduct == null)
            {
                LikedProduct item = new LikedProduct();

                item.ProductId = id;
                item.ProductType = "phoneProtector";
                item.UserId = Convert.ToInt32(User.FindFirst(ClaimTypes.Sid).Value);
                item.Image = phoneProtectorRepository.GetPhoneProtectorImageLink(item.ProductId);
                item.Price = phoneProtectorRepository.GetPhoneProtectorPrice(item.ProductId);
                item.ProductName = phoneProtectorRepository.GetPhoneProtectorName(item.ProductId);
                likedProductRepository.AddToLikedProducts(item);
            }
            else
            {
                BadRequest("Продукта вече е добавен в харесани продукти");
            }
        }


        public async Task<IActionResult> Cart()
        {
            CartVM model = new CartVM();
            model.Cart = new List<Cart>();
            model.TotalPrice = 0;
            model.TotalProducts = 0;

            if (!User.Identity.IsAuthenticated)
            {
                foreach (var item in Request.Cookies.Where(x => x.Key.Contains("cartItemPhone")))
                {
                    Cart userCart = new Cart();

                    userCart.ProductId = int.Parse(item.Value);

                    userCart.ProductName = mobilePhonesRepository.GetMobilePhoneName(userCart.ProductId);
                    userCart.Image = mobilePhonesRepository.GetMobilePhoneImageLink(userCart.ProductId);
                    userCart.Quantity = 1;
                    userCart.PriceForOne = mobilePhonesRepository.GetMobilePhonePrice(userCart.ProductId);
                    userCart.Price = mobilePhonesRepository.GetMobilePhonePrice(userCart.ProductId);
                    model.TotalPrice += mobilePhonesRepository.GetMobilePhonePrice(userCart.ProductId);
                    model.TotalProducts += userCart.Quantity;
                    model.Cart.Add(userCart);
                }
                foreach (var item in Request.Cookies.Where(x => x.Key.Contains("cartItemWiredHeadphones")))
                {
                    Cart userCart = new Cart();
                    userCart.ProductId = int.Parse(item.Value);
                    userCart.ProductName = wiredHeadphonesRepository.GetWiredHeadphonesName(userCart.ProductId);
                    userCart.Image = wiredHeadphonesRepository.GetWiredHeadphonesImageLink(userCart.ProductId);
                    userCart.Quantity = 1;
                    userCart.PriceForOne = wiredHeadphonesRepository.GetWiredHeadphonesPrice(userCart.ProductId);
                    userCart.Price = wiredHeadphonesRepository.GetWiredHeadphonesPrice(userCart.ProductId);
                    model.TotalPrice += wiredHeadphonesRepository.GetWiredHeadphonesPrice(userCart.ProductId);
                    model.TotalProducts += userCart.Quantity;
                    model.Cart.Add(userCart);
                }
                foreach (var item in Request.Cookies.Where(x => x.Key.Contains("cartItemWirelessHeadphones")))
                {
                    Cart userCart = new Cart();
                    userCart.ProductId = int.Parse(item.Value);
                    userCart.ProductName = wirelessHeadphonesRepository.GetWirelessHeadphonesName(userCart.ProductId);
                    userCart.Image = wirelessHeadphonesRepository.GetWirelessHeadphonesImageLink(userCart.ProductId);
                    userCart.Quantity = 1;
                    userCart.PriceForOne = wirelessHeadphonesRepository.GetWirelessHeadphonesPrice(userCart.ProductId);
                    userCart.Price = wirelessHeadphonesRepository.GetWirelessHeadphonesPrice(userCart.ProductId);
                    model.TotalPrice += wirelessHeadphonesRepository.GetWirelessHeadphonesPrice(userCart.ProductId);
                    model.TotalProducts += userCart.Quantity;
                    model.Cart.Add(userCart);
                }
                foreach (var item in Request.Cookies.Where(x => x.Key.Contains("cartItemSmartwatch")))
                {
                    Cart userCart = new Cart();
                    userCart.ProductId = int.Parse(item.Value);
                    userCart.ProductName = smartwatchRepository.GetSmartwatchName(userCart.ProductId);
                    userCart.Image = smartwatchRepository.GetSmartwatchImageLink(userCart.ProductId);
                    userCart.Quantity = 1;
                    userCart.PriceForOne = smartwatchRepository.GetSmartwatchPrice(userCart.ProductId);
                    userCart.Price = smartwatchRepository.GetSmartwatchPrice(userCart.ProductId);
                    model.TotalPrice += smartwatchRepository.GetSmartwatchPrice(userCart.ProductId);
                    model.TotalProducts += userCart.Quantity;
                    model.Cart.Add(userCart);
                }
                foreach (var item in Request.Cookies.Where(x => x.Key.Contains("cartItemPhoneCase")))
                {
                    Cart userCart = new Cart();
                    userCart.ProductId = int.Parse(item.Value);
                    userCart.ProductName = phoneCaseRepository.GetPhoneCaseName(userCart.ProductId);
                    userCart.Image = phoneCaseRepository.GetPhoneCaseImageLink(userCart.ProductId);
                    userCart.Quantity = 1;
                    userCart.PriceForOne = phoneCaseRepository.GetPhoneCasePrice(userCart.ProductId);
                    userCart.Price = phoneCaseRepository.GetPhoneCasePrice(userCart.ProductId);
                    model.TotalPrice += phoneCaseRepository.GetPhoneCasePrice(userCart.ProductId);
                    model.TotalProducts += userCart.Quantity;
                    model.Cart.Add(userCart);
                }
                foreach (var item in Request.Cookies.Where(x => x.Key.Contains("cartItemPhoneProtector")))
                {
                    Cart userCart = new Cart();
                    userCart.ProductId = int.Parse(item.Value);
                    userCart.ProductName = phoneProtectorRepository.GetPhoneProtectorName(userCart.ProductId);
                    userCart.Image = phoneProtectorRepository.GetPhoneProtectorImageLink(userCart.ProductId);
                    userCart.Quantity = 1;
                    userCart.PriceForOne = phoneProtectorRepository.GetPhoneProtectorPrice(userCart.ProductId);
                    userCart.Price = phoneProtectorRepository.GetPhoneProtectorPrice(userCart.ProductId);
                    model.TotalPrice += phoneProtectorRepository.GetPhoneProtectorPrice(userCart.ProductId);
                    model.TotalProducts += userCart.Quantity;
                    model.Cart.Add(userCart);
                }
            }
            else
            {
                model.Cart = await cartRepository.GetByUserIdAsync(int.Parse(User.FindFirst(ClaimTypes.Sid).Value));
                foreach (var item in model.Cart)
                {
                    if (item.ProductType == "mobilePhone")
                    {
                        model.TotalPrice += mobilePhonesRepository.GetMobilePhonePrice(item.ProductId);
                    }
                    if (item.ProductType == "wiredHeadphones")
                    {
                        model.TotalPrice += wiredHeadphonesRepository.GetWiredHeadphonesPrice(item.ProductId);
                    }
                    if (item.ProductType == "wirelessHeadphones")
                    {
                        model.TotalPrice += wirelessHeadphonesRepository.GetWirelessHeadphonesPrice(item.ProductId);
                    }
                    if (item.ProductType == "smartwatch")
                    {
                        model.TotalPrice += smartwatchRepository.GetSmartwatchPrice(item.ProductId);
                    }
                    if (item.ProductType == "phoneCase")
                    {
                        model.TotalPrice += phoneCaseRepository.GetPhoneCasePrice(item.ProductId);
                    }
                    if (item.ProductType == "phoneProtector")
                    {
                        model.TotalPrice += phoneProtectorRepository.GetPhoneProtectorPrice(item.ProductId);
                    }
                    model.TotalProducts += item.Quantity;
                }
            }

            return View(model);
        }


        [HttpPost]
        public IActionResult Search(string searchWord)
        {

            List<dynamic> searchInPhones = mobilePhonesRepository.MobilePhoneSearch(searchWord);
            List<dynamic> searchInWiredHeadphones = wiredHeadphonesRepository.WiredHeadphonesSearch(searchWord);
            List<dynamic> searchInWirelessHeadphones = wirelessHeadphonesRepository.WirelessHeadphonesSearch(searchWord);
            List<dynamic> searchInSmartwatches = smartwatchRepository.SmartwatchSearch(searchWord);
            List<dynamic> searchInPhoneCases = phoneCaseRepository.PhoneCaseSearch(searchWord);
            List<dynamic> searchInPhoneProtectors = phoneProtectorRepository.PhoneProtectorSearch(searchWord);

            List<dynamic> searchResults = new List<dynamic>();
            searchResults.AddRange(searchInPhones);
            searchResults.AddRange(searchInWiredHeadphones);
            searchResults.AddRange(searchInWirelessHeadphones);
            searchResults.AddRange(searchInSmartwatches);
            searchResults.AddRange(searchInPhoneCases);
            searchResults.AddRange(searchInPhoneProtectors);

            SearchResultsVM searchResultsVM = new SearchResultsVM();
            searchResultsVM.SearchResults = searchResults;
            searchResultsVM.SearchWord = searchWord;

            return View("SearchResults", searchResultsVM);
        }

        public FileResult GetImageForCartAndLiked(int productId, string type)
        {
            string rootPath;
            string path;
            byte[] imageByteData = null;
            if (!User.Identity.IsAuthenticated)
            {
                int id = 0;
                foreach (var item in Request.Cookies.Where(x => x.Key.Contains("cartItemPhone")))
                {
                    id = int.Parse(item.Value);
                    MobilePhone mobilePhone = applicationDbContext.MobilePhones.Find(id);

                    rootPath = webHostEnvironment.WebRootPath;
                    path = Path.Combine(rootPath + "\\img\\mobilephones", mobilePhone.Image1 + ".png");

                    imageByteData = System.IO.File.ReadAllBytes(path);

                }
                foreach (var item in Request.Cookies.Where(x => x.Key.Contains("cartItemWiredHeadphones")))
                {
                    id = int.Parse(item.Value);
                    WiredHeadphones wiredHeadphones = applicationDbContext.WiredHeadphones.Find(id);

                    rootPath = webHostEnvironment.WebRootPath;
                    path = Path.Combine(rootPath + "\\img\\headphones\\wired", wiredHeadphones.Image + ".png");

                    imageByteData = System.IO.File.ReadAllBytes(path);

                }
                foreach (var item in Request.Cookies.Where(x => x.Key.Contains("cartItemWirelessHeadphones")))
                {
                    id = int.Parse(item.Value);
                    WirelessHeadphones wirelessHeadphones = applicationDbContext.WirelessHeadphones.Find(id);

                    rootPath = webHostEnvironment.WebRootPath;
                    path = Path.Combine(rootPath + "\\img\\headphones\\wireless", wirelessHeadphones.Image + ".png");

                    imageByteData = System.IO.File.ReadAllBytes(path);
                }
                foreach (var item in Request.Cookies.Where(x => x.Key.Contains("cartItemSmartwatch")))
                {
                    id = int.Parse(item.Value);
                    Smartwatch smartwatch = applicationDbContext.Smartwatches.Find(id);

                    rootPath = webHostEnvironment.WebRootPath;
                    path = Path.Combine(rootPath + "\\img\\smartwatches", smartwatch.Image + ".png");

                    imageByteData = System.IO.File.ReadAllBytes(path);
                }
                foreach (var item in Request.Cookies.Where(x => x.Key.Contains("cartItemPhoneCase")))
                {
                    id = int.Parse(item.Value);
                    PhoneCase phoneCase = applicationDbContext.PhoneCases.Find(id);

                    rootPath = webHostEnvironment.WebRootPath;
                    path = Path.Combine(rootPath + "\\img\\phonecases", phoneCase.Image + ".png");

                    imageByteData = System.IO.File.ReadAllBytes(path);
                }
                foreach (var item in Request.Cookies.Where(x => x.Key.Contains("cartItemPhoneProtector")))
                {
                    id = int.Parse(item.Value);
                    PhoneProtector phoneProtector = applicationDbContext.PhoneProtectors.Find(id);

                    rootPath = webHostEnvironment.WebRootPath;
                    path = Path.Combine(rootPath + "\\img\\phoneprotectors", phoneProtector.Image + ".png");

                    imageByteData = System.IO.File.ReadAllBytes(path);
                }
            }
            else
            {
                switch (type)
                {
                    case "mobilePhone":

                        MobilePhone mobilePhone = applicationDbContext.MobilePhones.Find(productId);

                        rootPath = webHostEnvironment.WebRootPath;
                        path = Path.Combine(rootPath + "\\img\\mobilephones", mobilePhone.Image1 + ".png");

                        imageByteData = System.IO.File.ReadAllBytes(path);
                        break;
                    case "wiredHeadphones":

                        WiredHeadphones wiredHeadphones = applicationDbContext.WiredHeadphones.Find(productId);

                        rootPath = webHostEnvironment.WebRootPath;
                        path = Path.Combine(rootPath + "\\img\\headphones\\wired", wiredHeadphones.Image + ".png");

                        imageByteData = System.IO.File.ReadAllBytes(path);

                        break;
                    case "wirelessHeadphones":

                        WirelessHeadphones wirelessHeadphones = applicationDbContext.WirelessHeadphones.Find(productId);

                        rootPath = webHostEnvironment.WebRootPath;
                        path = Path.Combine(rootPath + "\\img\\headphones\\wireless", wirelessHeadphones.Image + ".png");

                        imageByteData = System.IO.File.ReadAllBytes(path);

                        break;
                    case "smartwatch":

                        Smartwatch smartwatch = applicationDbContext.Smartwatches.Find(productId);

                        rootPath = webHostEnvironment.WebRootPath;
                        path = Path.Combine(rootPath + "\\img\\smartwatches", smartwatch.Image + ".png");

                        imageByteData = System.IO.File.ReadAllBytes(path);

                        break;
                    case "phoneCase":

                        PhoneCase phoneCase = applicationDbContext.PhoneCases.Find(productId);

                        rootPath = webHostEnvironment.WebRootPath;
                        path = Path.Combine(rootPath + "\\img\\phonecases", phoneCase.Image + ".png");

                        imageByteData = System.IO.File.ReadAllBytes(path);

                        break;
                    case "phoneProtector":

                        PhoneProtector phoneProtector = applicationDbContext.PhoneProtectors.Find(productId);

                        rootPath = webHostEnvironment.WebRootPath;
                        path = Path.Combine(rootPath + "\\img\\phoneprotectors", phoneProtector.Image + ".png");

                        imageByteData = System.IO.File.ReadAllBytes(path);

                        break;


                }
            }

            return File(imageByteData, "image/png");
        }

        public IActionResult SingleItemPage(int productId, string type)
        {
            string method = "";
            switch (type)
            {
                case "mobilePhone":
                    method = "SingleMobilePhonePage";
                    break;
                case "wiredHeadphones":
                    method = "SingleWiredHeadphonesPage";
                    break;
                case "wirelessHeadphones":
                    method = "SingleWirelessHeadphonesPage";
                    break;
                case "smartwatch":
                    method = "SingleSmartwatchPage";
                    break;
                case "phoneCase":
                    method = "SinglePhoneCasePage";
                    break;
                case "phoneProtector":
                    method = "SinglePhoneProtectorPage";
                    break;
            }

            return RedirectToAction(method, "Products", new { id = productId });
        }

        public async Task<IActionResult> RemoveFromCart(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {

                foreach (var item in Request.Cookies.Where(x => x.Key.Contains("cartItemPhone") && x.Value.Contains(id.ToString())))
                {
                    Response.Cookies.Delete("cartItemPhone" + id.ToString());
                }
                foreach (var item in Request.Cookies.Where(x => x.Key.Contains("cartItemWiredHeadphones") && x.Value.Contains(id.ToString())))
                {
                    Response.Cookies.Delete("cartItemWiredHeadphones" + id.ToString());
                }
                foreach (var item in Request.Cookies.Where(x => x.Key.Contains("cartItemWirelessHeadphones") && x.Value.Contains(id.ToString())))
                {
                    Response.Cookies.Delete("cartItemWirelessHeadphones" + id.ToString());
                }
                foreach (var item in Request.Cookies.Where(x => x.Key.Contains("cartItemSmartwatch") && x.Value.Contains(id.ToString())))
                {
                    Response.Cookies.Delete("cartItemSmartwatch" + id.ToString());
                }
                foreach (var item in Request.Cookies.Where(x => x.Key.Contains("cartItemPhoneCase") && x.Value.Contains(id.ToString())))
                {
                    Response.Cookies.Delete("cartItemPhoneCase" + id.ToString());
                }
                foreach (var item in Request.Cookies.Where(x => x.Key.Contains("cartItemPhoneProtector") && x.Value.Contains(id.ToString())))
                {
                    Response.Cookies.Delete("cartItemPhoneProtector" + id.ToString());
                }
            }
            else
            {
                await cartRepository.RemoveFromCart(id);
            }
            return RedirectToAction("Cart", "Products");
        }

        public Task RemoveEverythingFromCartUnauthenticatedUser()
        {
            foreach (var item in Request.Cookies.Where(x => x.Key.Contains("cartItemPhone")))
            {
                Response.Cookies.Delete("cartItemPhone");
            }
            foreach (var item in Request.Cookies.Where(x => x.Key.Contains("cartItemWiredHeadphones")))
            {
                Response.Cookies.Delete("cartItemWiredHeadphones");
            }
            foreach (var item in Request.Cookies.Where(x => x.Key.Contains("cartItemWirelessHeadphones")))
            {
                Response.Cookies.Delete("cartItemWirelessHeadphones");
            }
            foreach (var item in Request.Cookies.Where(x => x.Key.Contains("cartItemSmartwatch")))
            {
                Response.Cookies.Delete("cartItemSmartwatch");
            }
            foreach (var item in Request.Cookies.Where(x => x.Key.Contains("cartItemPhoneCase")))
            {
                Response.Cookies.Delete("cartItemPhoneCase");
            }
            foreach (var item in Request.Cookies.Where(x => x.Key.Contains("cartItemPhoneProtector")))
            {
                Response.Cookies.Delete("cartItemPhoneProtector");
            }
            return Task.CompletedTask;
        }

        public async Task<IActionResult> LikedProducts()
        {
            LikedVM model = new LikedVM();
            model.LikedProducts = new List<LikedProduct>();
            model.TotalProducts = 0;

            model.LikedProducts = await likedProductRepository.GetByUserIdAsync(int.Parse(User.FindFirst(ClaimTypes.Sid).Value));
            foreach (var item in model.LikedProducts)
            {
                model.TotalProducts += 1;
            }

            return View(model);
        }
        public async Task<IActionResult> RemoveFromLiked(int id)
        {
            await likedProductRepository.RemoveFromLikedProducts(id);
            applicationDbContext.SaveChanges();

            return RedirectToAction("LikedProducts", "Products");
        }

        [HttpPost]
        public async Task UpdateCartItemQuantity(int productId, string productType, int quantity)
        {
            var cartItem = await cartRepository.GetCartItemByProductIdAndTypeAsync(productId, productType);

            cartItem.Quantity = quantity;
            await cartRepository.UpdateCartItemAsync(cartItem);
        }

        [HttpGet]
        public IActionResult ChoosePaymentMethodAndShippingMethod()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ChoosePaymentMethodAndShippingMethod(ChoosePaymentAndShippingMethodVM order)
        {
            return RedirectToAction("PlaceOrder", order);
        }
        [HttpGet]
        public async Task<IActionResult> PlaceOrder(ChoosePaymentAndShippingMethodVM methods)
        {
            AddOrderVM order = new AddOrderVM();
            order.PaymentMethod = methods.PaymentMethod;
            order.ShippingMethod = methods.ShippingMethod;
            if(order.ShippingMethod == ShippingMethod.Експресна_доставка)
            {
                order.OrderTotalPrice += 10;
            }
            if (!User.Identity.IsAuthenticated)
            {
                foreach (var item in Request.Cookies.Where(x => x.Key.Contains("cartItemPhone")))
                {
                    order.ProductsId += $"{int.Parse(item.Value)},";
                    order.ProductsTypes += $"mobilePhone,";
                    order.ProductsQuantities += $"{1},";
                    order.OrderTotalPrice += mobilePhonesRepository.GetMobilePhonePrice(int.Parse(item.Value));
                }
                foreach (var item in Request.Cookies.Where(x => x.Key.Contains("cartItemWiredHeadphones")))
                {
                    order.ProductsId += $"{int.Parse(item.Value)},";
                    order.ProductsTypes += $"wiredHeadphones,";
                    order.ProductsQuantities += $"{1},";
                    order.OrderTotalPrice += wiredHeadphonesRepository.GetWiredHeadphonesPrice(int.Parse(item.Value));
                }
                foreach (var item in Request.Cookies.Where(x => x.Key.Contains("cartItemWirelessHeadphones")))
                {
                    order.ProductsId += $"{int.Parse(item.Value)},";
                    order.ProductsTypes += $"wirelessHeadphones,";
                    order.ProductsQuantities += $"{1},";
                    order.OrderTotalPrice += wirelessHeadphonesRepository.GetWirelessHeadphonesPrice(int.Parse(item.Value));
                }
                foreach (var item in Request.Cookies.Where(x => x.Key.Contains("cartItemSmartwatch")))
                {
                    order.ProductsId += $"{int.Parse(item.Value)},";
                    order.ProductsTypes += $"smartwatch,";
                    order.ProductsQuantities += $"{1},";
                    order.OrderTotalPrice += smartwatchRepository.GetSmartwatchPrice(int.Parse(item.Value));
                }
                foreach (var item in Request.Cookies.Where(x => x.Key.Contains("cartItemPhoneCase")))
                {
                    order.ProductsId += $"{int.Parse(item.Value)},";
                    order.ProductsTypes += $"phoneCase,";
                    order.ProductsQuantities += $"{1},";
                    order.OrderTotalPrice += phoneCaseRepository.GetPhoneCasePrice(int.Parse(item.Value));
                }
                foreach (var item in Request.Cookies.Where(x => x.Key.Contains("cartItemPhoneProtector")))
                {
                    order.ProductsId += $"{int.Parse(item.Value)},";
                    order.ProductsTypes += $"phoneProtector,";
                    order.ProductsQuantities += $"{1},";
                    order.OrderTotalPrice += phoneProtectorRepository.GetPhoneProtectorPrice(int.Parse(item.Value));
                }
            }
            else
            {
                int userId = Convert.ToInt32(User.FindFirst(ClaimTypes.Sid).Value);
                List<Cart> cart = await cartRepository.GetByUserIdAsync(userId);
                order.UserId = userId;
                foreach (var item in cart)
                {
                    order.ProductsId += $"{item.ProductId},";
                    order.ProductsTypes += $"{item.ProductType},";
                    order.ProductsQuantities += $"{item.Quantity},";
                    order.OrderTotalPrice += item.Price;
                }
            }
            return View(order);
        }
        [HttpPost]
        public async Task<IActionResult> PlaceOrder(AddOrderVM item)
        {
            Order order = new Order();
            if (User.Identity.IsAuthenticated)
            {
                order.UserId = item.UserId;
            }
            order.ProductsId = item.ProductsId;
            order.FirstAndLastName = item.FirstAndLastName;
            order.PhoneNumber = item.PhoneNumber;
            order.ProductsTypes = item.ProductsTypes;
            order.ProductsQuantities = item.ProductsQuantities;
            order.DateOrdered = DateTime.Now;
            order.OrderStatus = OrderStatusEnum.Предстояща;
            order.ShippingAddress = item.ShippingCity + ", " + item.ShippingAddress;
            order.ShippingMethod = item.ShippingMethod;
            if(order.ShippingMethod == ShippingMethod.Експресна_доставка)
            {
                order.OrderTotalPrice = item.OrderTotalPrice + 10;
            }
            else
            {
                order.OrderTotalPrice = item.OrderTotalPrice;
            }
            try
            {
                await orderRepository.AddOrderAsync(order);
                if (!User.Identity.IsAuthenticated)
                {
                    await RemoveEverythingFromCartUnauthenticatedUser();
                }
                else
                {
                    await cartRepository.RemoveAllFromCartByUserId(order.UserId);
                }
                return RedirectToAction("OrderPlaced");
            }
            catch (Exception ex)
            {
                return RedirectToAction("OrderError");
            }

        }
    
        public IActionResult OrderPlaced()
        {
            return View();
        }
        public IActionResult OrderError()
        {
            return View();
        }

        public async Task<IActionResult> CheckOrder(int id)
        {
            ApplicationDbContext applicationDbContext = new ApplicationDbContext();
            Order order = applicationDbContext.Orders.Find(id);
            CheckOrderVM checkOrder = new CheckOrderVM();
            List<Cart> orderItems = new List<Cart>();
            checkOrder.OrderNumber = order.OrderNum;
            checkOrder.OrderPrice = order.OrderTotalPrice;
            checkOrder.OrderStatus = order.OrderStatus;
            checkOrder.OrderId = order.Id;

            string[] idS = order.ProductsId.Split(',');
            string[] types = order.ProductsTypes.Split(",");
            string[] quantities = order.ProductsQuantities.Split(",");

            for (int i = 0; i < idS.Length; i++)
            {
                if (idS[i] == "")
                {
                    break;
                }
                    Cart cart = new Cart();
                if (types[i] == "mobilePhone")
                {
                    cart.ProductId = int.Parse(idS[i]);
                    cart.ProductName = mobilePhonesRepository.GetMobilePhoneName(cart.ProductId);
                    cart.Quantity = int.Parse(quantities[i]);
                    cart.ProductType = "mobilePhone";
                    cart.Price = mobilePhonesRepository.GetMobilePhonePrice(cart.ProductId);
                }
                else if (types[i] == "wiredHeadphones")
                {
                    cart.ProductId = int.Parse(idS[i]);
                    cart.ProductName = wiredHeadphonesRepository.GetWiredHeadphonesName(cart.ProductId);
                    cart.Quantity = int.Parse(quantities[i]);
                    cart.ProductType = "wiredHeadphones";
                    cart.Price = wiredHeadphonesRepository.GetWiredHeadphonesPrice(cart.ProductId);
                }
                else if (types[i] == "wirelessHeadphones")
                {
                    cart.ProductId = int.Parse(idS[i]);
                    cart.ProductName = wirelessHeadphonesRepository.GetWirelessHeadphonesName(cart.ProductId);
                    cart.Quantity = int.Parse(quantities[i]);
                    cart.ProductType = "wirelessHeadphones";
                    cart.Price = wirelessHeadphonesRepository.GetWirelessHeadphonesPrice(cart.ProductId);
                }
                else if (types[i] == "smartwatch")
                {
                    cart.ProductId = int.Parse(idS[i]);
                    cart.ProductName = smartwatchRepository.GetSmartwatchName(cart.ProductId);
                    cart.Quantity = int.Parse(quantities[i]);
                    cart.ProductType = "smartwatch";
                    cart.Price = smartwatchRepository.GetSmartwatchPrice(cart.ProductId);
                }
                else if (types[i] == "phoneCase")
                {
                    cart.ProductId = int.Parse(idS[i]);
                    cart.ProductName = phoneCaseRepository.GetPhoneCaseName(cart.ProductId);
                    cart.Quantity = int.Parse(quantities[i]);
                    cart.ProductType = "phoneCase";
                    cart.Price = phoneCaseRepository.GetPhoneCasePrice(cart.ProductId);
                }
                else if (types[i] == "phoneProtector")
                {
                    cart.ProductId = int.Parse(idS[i]);
                    cart.ProductName = phoneProtectorRepository.GetPhoneProtectorName(cart.ProductId);
                    cart.Quantity = int.Parse(quantities[i]);
                    cart.ProductType = "phoneProtector";
                    cart.Price = phoneProtectorRepository.GetPhoneProtectorPrice(cart.ProductId);
                }

                orderItems.Add(cart);
            }
            checkOrder.OrderItems = orderItems;
            return View(checkOrder);
        }
        public async Task CancelOrder(int id)
        {
            await orderRepository.CancelOrder(id);
        }
    }
}
