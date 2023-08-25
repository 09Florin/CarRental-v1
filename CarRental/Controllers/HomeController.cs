using CarRental.Data;
using CarRental.Models;
using CarRental.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CarRental.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _dbContext;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Registration(RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var client = new Client
                {
                    // Map properties from RegistrationViewModel to Client
                    Username = model.Username,
                    Password = model.Password,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Birthday = model.Birthday,
                    CreatedAt = DateTime.UtcNow
                };

                _dbContext.Clients?.Add(client);
                _dbContext.SaveChanges();

                HttpContext.Session.SetInt32("ClientId", client.Id);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        //[HttpPost]
        //public IActionResult Login(LoginViewModel model)
        //{
        //    var client = _dbContext.Clients?.FirstOrDefault(c => c.Username == model.Username && c.Password == model.Password);

        //    if (client != null)
        //    {
        //        HttpContext.Session.SetString("ClientId", client.Id.ToString());
        //        ViewBag.userClient = client;
        //        return RedirectToAction("Index", "Home");
        //    }
        //    else
        //    {
        //        ModelState.AddModelError("Username", "Invalid username or password.");
        //        return View(model);
        //    }
        //}

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            var client = _dbContext.Clients?.FirstOrDefault(c => c.Username == model.Username && c.Password == model.Password);

            if (client != null)
            {
                TempData["ClientId"] = client.Id;
                ViewBag.userClient = client;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("Username", "Invalid username or password.");
                return View(model);
            }
        }


        [HttpPost]
        public IActionResult Search(string manufacturer, string model, int? year, string color, decimal? minPrice, decimal? maxPrice)
        {
            var cars = _dbContext.Cars?.Where(c =>
                (manufacturer == null || c.Manufacturer == manufacturer) &&
                (model == null || c.Model == model) &&
                (!year.HasValue || c.Year == year.Value) &&
                (color == null || c.Color == color) &&
                (!minPrice.HasValue || c.RentalPricePerDay >= minPrice.Value) &&
                (!maxPrice.HasValue || c.RentalPricePerDay <= maxPrice.Value)).ToList();

            if (string.IsNullOrWhiteSpace(manufacturer) &&
                string.IsNullOrWhiteSpace(model) &&
                !year.HasValue &&
                string.IsNullOrWhiteSpace(color) &&
                !minPrice.HasValue &&
                !maxPrice.HasValue)
            {
                return View("NoResults"); // NoResults.cshtml to be created
            }

            return View(cars);
        }




        [HttpPost]
        public IActionResult Profile(ProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var client = ViewBag.userClient;
                {
                    model.Username = client.Username;
                    model.Email = client.Email;
                    model.FirstName = client.FirstName;
                    model.LastName = client.LastName;
                    model.Birthday = client.Birthday;
                    model.City = client.City;
                };

            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Profile()
        {
            return View();
        }

    }
}
