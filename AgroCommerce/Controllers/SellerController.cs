using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AgroCommerce.Core;
using AgroCommerce.Services.Contracts;
using AgroCommerce.Utilities;
using AgroCommerce.Utilities.CustomExceptions;
using AgroCommerce.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AgroCommerce.Controllers
{
    [Authorize]
    public class SellerController : Controller
    {
        private readonly IUserAccountService _userAccountService;
        private readonly IFarmService _farmService;
        private readonly IListingService _listingService;
        private readonly IAnimalTypeService _animalTypeService;
        private readonly ITransactionService _transactionService;
        private readonly IReviewService _reviewService;
        private readonly ILogger _logger;
        public SellerController(IUserAccountService userAccountService, IFarmService farmService, IListingService listingService, IAnimalTypeService animalTypeService, ITransactionService transactionService, IReviewService reviewService, ILogger<SellerController> logger)
        {
            _userAccountService = userAccountService;
            _farmService = farmService;
            _listingService = listingService;
            _animalTypeService = animalTypeService;
            _transactionService = transactionService;
            _reviewService = reviewService;
            _logger = logger;
        }
        public IActionResult Index()
        {
            ViewBag.Farm = _farmService.GetAll().Count();
            return View();
        }

        public IActionResult SetUpFarm()
        {
            var user = GetLoggedInUser();
            if(user.IsAccountComplete == true)
                 return RedirectToAction(nameof(Index));

            
            FarmAddViewModel farm = new FarmAddViewModel() {
                ImagePath = "https://image.freepik.com/free-vector/farmer-peasant-illustration-man-with-beard-spade-farmland_33099-575.jpg"
                };
            return View(farm);
        }

        [HttpPost]
        public IActionResult SetUpFarm(FarmAddViewModel farm)
        {
            var user = GetLoggedInUser();

            try
            {
                if (ModelState.IsValid)
                {
                    if(user.IsAccountComplete == false)
                    {
                        var file = farm.FarmLogoImage;
                        if (farm.FarmLogoImage != null)
                        {
                            string uri = FileService.SaveImage(file, $"{farm.Name}/Icons");
                            if (string.IsNullOrEmpty(uri) || string.IsNullOrWhiteSpace(uri))
                                throw new FileSaveErrorException();

                            farm.ImagePath = uri;
                        }
                        else
                        {
                            throw new NoFileFoundException();
                        }

                        Farm _farm = new Farm()
                        {
                            Name = farm.Name,
                            Location = farm.Location,
                            ImagePath = farm.ImagePath,
                            ApplicationUserId = user.Id,
                            IsActive = true,
                        };
                        _farmService.SetupFarm(_farm, user);
                    }
                    else
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                else
                {
                    return View(farm);
                }
            }catch(Exception ex)
            {
                _logger.LogError(ex, $"\n{DateTime.Now} Error occured in Seller/SetUpFarm\n");
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists contact the administrator."+ex);
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult EditFarm()
        {
            var user = GetLoggedInUser();

            FarmAddViewModel farm = new FarmAddViewModel()
            {
                Name = string.IsNullOrEmpty(user.Farm.Name) ? "" : user.Farm.Name,
                Location = string.IsNullOrEmpty(user.Farm.Location) ? "" : user.Farm.Location,
                ImagePath = string.IsNullOrEmpty(user.Farm.ImagePath) ? "https://image.freepik.com/free-vector/farmer-peasant-illustration-man-with-beard-spade-farmland_33099-575.jpg" : user.Farm.ImagePath
            };
            return View(farm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditFarm(FarmAddViewModel _farm)
        {
            var user = GetLoggedInUser();

            try
            {
                if (ModelState.IsValid)
                {
                    var file = _farm.FarmLogoImage;
                    if (_farm.FarmLogoImage != null)
                    {
                        string uri = FileService.SaveImage(file, $"{_farm.Name}/Icons");
                        if (string.IsNullOrEmpty(uri) || string.IsNullOrWhiteSpace(uri))
                            throw new FileSaveErrorException();

                        _farm.ImagePath = uri;
                    }
                    else
                    {
                        throw new NoFileFoundException();
                    }
                    if (user.Farm == null)
                        RedirectToAction(nameof(SetUpFarm));

                    Farm farm = _farmService.GetByID(user.Farm.ID);
                    farm.Name = _farm.Name;
                    farm.Location = _farm.Location;
                    farm.ImagePath = string.IsNullOrEmpty(_farm.ImagePath) ? farm.ImagePath : _farm.ImagePath;

                    _farmService.UpdateFarm(farm);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(_farm);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"\n{DateTime.Now} Error occured in Seller/EditFarm\n");
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists contact the administrator." + ex);
                throw;
            }
        }

        public IActionResult LoadClassAndBreed(int animalTypeId)
        {
            var animalType = _animalTypeService.GetByID(animalTypeId);
            AnimalTypeClassBreedViewModel model = new AnimalTypeClassBreedViewModel();
            model.Classes = animalType != null ? animalType.Class.Split(',').ToList() : new List<string>();
            model.Breed = animalType != null ? animalType.Breed.Split(',').ToList() : new List<string>();

            return Json(model);
        }


        [HttpGet]
        public IActionResult AddListing()
        {
            var user = GetLoggedInUser();

            if (user.Farm.IsActive == false)
            {
                return NotFound();
            }
            ViewBag.AnimaTypes = _animalTypeService.GetAll();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddListing(AddListingViewModel model, string breed, string _class, int animalTypeId, string gender,
            int listingYears = 0, int listingMonths = 0, int listingWeeks = 0)
        {
            var user = GetLoggedInUser();
            var farm = _farmService.GetByID(user.Farm.ID);
            try
            {
                if (ModelState.IsValid)
                {
                    var file = model.ImageFile;
                    if (file != null)
                    {
                        string fileName = FileService.SaveImage(file, $"{farm.Name}/Listing/Animal/{model.Type}/");
                        if (string.IsNullOrEmpty(fileName) || string.IsNullOrWhiteSpace(fileName))
                            throw new FileSaveErrorException();

                        model.ImagePath = fileName;
                    }
                    else
                    {
                        throw new NoFileFoundException();
                    }
                    string animalTypeName = "";
                    if (animalTypeId < 1)
                    {
                        breed = "";
                        _class = "";
                        animalTypeName = "";
                    }
                    else
                    {
                        breed = breed == "-1" ? "" : breed;
                        _class = _class == "-1" ? "" : _class;
                        animalTypeName = _animalTypeService.GetByID(animalTypeId).Name;
                    }

                    //to ensure no negatives;
                    listingYears = listingYears < 0 ? 0 : listingYears;
                    listingMonths = listingMonths < 0 ? 0 : listingMonths;
                    listingWeeks = listingWeeks < 0 ? 0 : listingWeeks;


                    //age
                    string listingAge = "";

                    if (listingYears > 0)
                    {
                        listingAge = listingYears.ToString() + " years ";
                    }
                    if (listingMonths > 0)
                    {
                        listingAge = listingAge + listingMonths.ToString() + " months ";
                    }
                    if (listingWeeks > 0)
                    {
                        listingAge = listingAge + listingWeeks.ToString() + " weeks ";
                    }

                    int ageConvertedToWeeks = (listingYears * 52) + (listingMonths * 4) + listingWeeks;

                    Listing listing = new Listing()
                    {
                        Title = model.Title,
                        Description = model.Description,
                        Price = model.Price,
                        Location = model.Location,
                        Age = listingAge + "old",
                        AgeYear = listingYears,
                        AgeMonth = listingMonths,
                        AgeWeek = listingWeeks,
                        AgeConvertedToWeeks = ageConvertedToWeeks,
                        Gender = String.IsNullOrEmpty(gender) ? "None" : gender,
                        AnimalType = animalTypeName,
                        Breed = breed,
                        Class = _class,
                        ImagePath = model.ImagePath,
                        FarmId = farm.ID,
                        Farm = farm,

                    };

                    _listingService.Create(listing);
                }
                else
                {
                    ViewBag.InvalidModel = "one or more input is invalid";
                    return View();
                }
                return RedirectToAction(nameof(Index));
            }catch(Exception ex)
            {
                _logger.LogError(ex, $"\n{DateTime.Now} Error occured in Seller/AddListing\n");
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists contact the administrator." + ex);
                throw;
            }
           
        }

        [HttpGet]
        public IActionResult UserImage()
        {
            string url = null;
            var user = GetLoggedInUser();
            if (user == null)
                throw new Exception();
            if (string.IsNullOrEmpty(user.ImagePath))
            {
                url = "https://miro.medium.com/max/1200/1*mk1-6aYaf_Bes1E3Imhc0A.jpeg";
            }
            else
            {
                url = user.ImagePath;
                url = url.Remove(0, 8);
                url = "/" + url;
            }
            return Json(new { result = url, url = Url.Action(nameof(Index), "Store") });
        }
        #region Helper Methods
        private ApplicationUser GetLoggedInUser()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return _userAccountService.GetById(userId);
        }
        #endregion
    }
}