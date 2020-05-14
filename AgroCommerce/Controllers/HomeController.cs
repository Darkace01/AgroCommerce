using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AgroCommerce.Models;
using AgroCommerce.Services.Contracts;

namespace AgroCommerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly IListingService _listingService;
        private readonly IAnimalTypeService _animalTypeService;
        private readonly IUserAccountService _userAccountService;

        public HomeController(IListingService listingService, IAnimalTypeService animalTypeService, IUserAccountService userAccountService)
        {
            this._listingService = listingService;
            this._animalTypeService = animalTypeService;
            this._userAccountService = userAccountService;
        }
        public IActionResult Index()
        {
            ViewBag.AnimalTypes = _animalTypeService.GetAll();
            ViewBag.ActiveFeaturedList = _listingService.GetAllWithImage().FirstOrDefault();
            ViewBag.FeaturedList = _listingService.GetAllWithImage().Skip(1).Take(5);
            ViewBag.AllListings = _listingService.GetAllWithImage().Skip(4);
            ViewBag.Chickens = _listingService.GetAllWithImage().Where(l => String.Compare(l.AnimalType, "Chicken", true) == 0);
            ViewBag.Cows = _listingService.GetAllWithImage().Where(l => String.Compare(l.AnimalType, "Cow", true) == 0);
            ViewBag.Fishes = _listingService.GetAllWithImage().Where(l => String.Compare(l.AnimalType, "Fish", true) == 0);
            ViewBag.Goats = _listingService.GetAllWithImage().Where(l => String.Compare(l.AnimalType, "Goat", true) == 0);
            ViewBag.Horses = _listingService.GetAllWithImage().Where(l => String.Compare(l.AnimalType, "Horse", true) == 0);
            ViewBag.Pigs = _listingService.GetAllWithImage().Where(l => String.Compare(l.AnimalType, "Pig", true) == 0);
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
