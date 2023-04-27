using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MobilePhonesWebsite.Data;
using MobilePhonesWebsite.Models;
using MobilePhonesWebsite.Repository;
using System.Diagnostics;

namespace MobilePhonesWebsite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationDbContext applicationDbContext;
        private readonly MobilePhonesRepository mobilePhonesRepository;
        private readonly WiredHeadphonesRepository wiredHeadphonesRepository;
        private readonly WirelessHeadphonesRepository wirelessHeadphonesRepository;
        private readonly SmartwatchRepository smartwatchRepository;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            applicationDbContext = new ApplicationDbContext();
            mobilePhonesRepository = new MobilePhonesRepository();
            wiredHeadphonesRepository = new WiredHeadphonesRepository();
            wirelessHeadphonesRepository = new WirelessHeadphonesRepository();
            smartwatchRepository = new SmartwatchRepository();
        }

        public IActionResult Index()
        {
            MobilePhone mobilePhone = mobilePhonesRepository.GetNewestMobilePhone();
            Smartwatch smartwatch = smartwatchRepository.GetNewestSmartwatch();
            WiredHeadphones wiredHeadphones = wiredHeadphonesRepository.GetNewestWiredHeadphones();
            WirelessHeadphones wirelessHeadphoness = wirelessHeadphonesRepository.GetNewestWirelessHeadphones();

            List<dynamic> newestItems = new List<dynamic>
            {
                mobilePhone,
                smartwatch,
                wiredHeadphones,
                wirelessHeadphoness
            };

            return View("Index", newestItems);
        }
        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

       
    }
}