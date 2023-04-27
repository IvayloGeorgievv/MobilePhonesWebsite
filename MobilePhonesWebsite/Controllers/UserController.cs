using Microsoft.AspNetCore.Mvc;
using MobilePhonesWebsite.Data;
using MobilePhonesWebsite.Models;
using MobilePhonesWebsite.ViewModels.UserVM;
using MobilePhonesWebsite.Repository;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Scrypt;
using static MobilePhonesWebsite.Enumerators.UserEnum;

namespace MobilePhonesWebsite.Controllers
{
    public class UserController : Controller
    {

        private UserRepository userRepository;
        private ScryptEncoder scryptEncoder;

        public UserController()
        {
            this.userRepository = new UserRepository();
            this.scryptEncoder = new ScryptEncoder();
        }

        [HttpGet]
        public IActionResult RegisterPage()
        {
            return View();
        }
        [HttpPost]
        public IActionResult RegisterPage(RegisterUserVM item)
        {
            if (!ModelState.IsValid)
                return View(item);

            UserRepository userRepository = new UserRepository();
            User user = new User();

            user.Username = item.Username;
            user.Password = scryptEncoder.Encode(item.Password);
            user.Email = item.Email;
            user.IsAdmin = false;
            userRepository.AddUser(user);
            user = userRepository.GetByEmailAndPassword(item.Email,item.Password);
            List<Claim> claims = new List<Claim>() {
                    new Claim(ClaimTypes.NameIdentifier , $"{user.Username}"),
                    new Claim(ClaimTypes.Email , user.Email),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role , user.IsAdmin ? "Admin": "User"),
                    new Claim(ClaimTypes.Sid , user.Id.ToString())
                };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
            CookieAuthenticationDefaults.AuthenticationScheme);

            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true,
                IsPersistent = true
            };

            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), properties);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult RegisterPageAdmin()
        {
            return View();
        }
        [HttpPost]
        public IActionResult RegisterPageAdmin(RegisterUserVM item)
        {
            if (!ModelState.IsValid)
                return View(item);

            UserRepository userRepository = new UserRepository();
            User user = new User();

            user.Username = item.Username;
            user.Password = scryptEncoder.Encode(item.Password);
            user.Email = item.Email;
            user.IsAdmin = true;
            userRepository.AddUser(user);
            user = userRepository.GetByEmailAndPassword(item.Email, item.Password);
            List<Claim> claims = new List<Claim>() {
                    new Claim(ClaimTypes.NameIdentifier , $"{user.Username}"),
                    new Claim(ClaimTypes.Email , user.Email),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role , user.IsAdmin ? "Admin": "User"),
                    new Claim(ClaimTypes.Sid , user.Id.ToString())
                };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
            CookieAuthenticationDefaults.AuthenticationScheme);

            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true,
                IsPersistent = true
            };

            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), properties);

            return RedirectToAction("Index", "Home");
        }

        //Login:

        [HttpGet]
        public IActionResult LoginPage()
        {
            return View();
        }
        [HttpPost]
        public IActionResult LoginPage(string email, string password)
        {
            User user = userRepository.GetByEmailAndPassword(email, password);
            if (user == null)
            {
                return RedirectToAction("LoginPage", "User");
            }
            else
            {
                List<Claim> claims = new List<Claim>() {
                    new Claim(ClaimTypes.NameIdentifier , $"{user.Username}"),
                    new Claim(ClaimTypes.Email , user.Email),
                    new Claim(ClaimTypes.Name , user.Username),
                    new Claim(ClaimTypes.Role , user.IsAdmin ? "Admin": "User"),
                    new Claim(ClaimTypes.Sid , user.Id.ToString())
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = true
                };

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), properties);
            }
            return RedirectToAction("Index", "Home");

        }
        [HttpGet]
        public IActionResult UserPage()
        {
            ApplicationDbContext applicationDbContext = new ApplicationDbContext();
            int id = Convert.ToInt32(User.FindFirst(ClaimTypes.Sid).Value);
            User item = applicationDbContext.Users.Find(id);
            UserPageVM user = new UserPageVM();
            user.Id = id;
            user.Username = item.Username;
            return View(user);
        }

        //Update account: 
        [HttpGet]
        public IActionResult UpdateUser(int id)
        {
            ApplicationDbContext applicationDbContext = new ApplicationDbContext();
            User user = applicationDbContext.Users.Find(id);
            EditUserVM item = new EditUserVM();

            item.Id = user.Id;
            item.Username = user.Username;
            item.Password = user.Password;
            item.Email = user.Email;
            item.IsAdmin = user.IsAdmin;

            return View(item);
        }

        [HttpPost]
        public IActionResult UpdateUser(EditUserVM item)
        {
            UserRepository userRepository = new UserRepository();
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }

            userRepository.UpdateYourUserAcc(item);

            return RedirectToAction("Index", "Home");

        }

        public async Task<IActionResult> UserOrdersList()
        {
            OrderRepository orderRepository = new OrderRepository();
            int id = Convert.ToInt32(User.FindFirst(ClaimTypes.Sid).Value);
            List<Order> userOrders = await orderRepository.GetByUserIdAsync(id);
            return View(userOrders);
        }
        //Logout:
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }

    }
}
