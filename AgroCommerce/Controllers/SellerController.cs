﻿using System;
using System.Collections.Generic;
using System.IO;
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
            if (user.IsAccountComplete) return RedirectToAction(nameof(EditFarm));


            FarmAddViewModel farm = new FarmAddViewModel()
            {
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
                    if (user.IsAccountComplete == false)
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
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"\n{DateTime.Now} Error occured in Seller/SetUpFarm\n");
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists contact the administrator." + ex);
                throw;
            }

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult EditFarm()
        {
            var user = GetLoggedInUser();
            var farm = _farmService.GetFarmByUserID(user.Id);
            if (farm == null)
                return RedirectToAction(nameof(SetUpFarm));

            FarmAddViewModel Farm = new FarmAddViewModel()
            {
                Name = string.IsNullOrEmpty(farm.Name) ? "" : farm.Name,
                Location = string.IsNullOrEmpty(farm.Location) ? "" : farm.Location,
                ImagePath = string.IsNullOrEmpty(farm.ImagePath) ? "https://image.freepik.com/free-vector/farmer-peasant-illustration-man-with-beard-spade-farmland_33099-575.jpg" : farm.ImagePath
            };
            return View(Farm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditFarm(FarmAddViewModel _farm)
        {
            var user = GetLoggedInUser();
            var oldFarm = _farmService.GetFarmByUserID(user.Id);
            if (oldFarm == null)
                return RedirectToAction(nameof(SetUpFarm));
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
                        // throw new NoFileFoundException();
                        _farm.ImagePath = oldFarm.ImagePath;
                    }
                    if (oldFarm == null)
                        RedirectToAction(nameof(SetUpFarm));

                    Farm farm = _farmService.GetByID(oldFarm.ID);
                    farm.Name = _farm.Name;
                    farm.Location = _farm.Location;
                    farm.ImagePath = string.IsNullOrEmpty(_farm.ImagePath) ? farm.ImagePath : _farm.ImagePath;

                    _farmService.UpdateFarm(farm, user);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(_farm);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"\n{DateTime.Now} Error occured in Seller/EditFarm\n");
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists contact the administrator." + ex);
                return View();
            }
        }

        


        [HttpGet]
        public IActionResult AddListing()
        {
            var user = GetLoggedInUser();
            var farm = _farmService.GetFarmByUserID(user.Id);
            if (farm == null)
                if (farm.IsActive == false)
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
            var farm = _farmService.GetFarmByUserID(user.Id);
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
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.InvalidModel = "one or more input is invalid";
                    return View();
                }
               
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"\n{DateTime.Now} Error occured in Seller/AddListing\n");
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists contact the administrator." + ex);
                throw;
            }

        }

        public IActionResult EditListing(int? Id)
        {
            if (Id.Value == null)
                return NotFound();
            ViewBag.AnimalTypes = _animalTypeService.GetAll();
            var user = GetLoggedInUser();
            var farm = _farmService.GetFarmByUserID(user.Id);
            var _listing = _listingService.GetByID(Id.Value);

            if (_listing == null && _listing.Farm.ApplicationUserId != user.Id)
                return NotFound();

            EditListingViewModel listing = new EditListingViewModel()
            {
                ID = _listing.ID,
                Title = _listing.Title,
                Description = _listing.Description,
                Price = _listing.Price,
                Location = _listing.Location,
                ImagePath = _listing.ImagePath,
                Farm = farm
            };

            ViewBag.SelectedAnimalType = _listing.AnimalType;
            ViewBag.SelectedAnimalTypeID = String.IsNullOrEmpty(_listing.AnimalType) ? 0 : _animalTypeService.GetIdByAnimalTypeName(_listing.AnimalType);
            ViewBag.SelectedClass = _listing.Class;
            ViewBag.SelectedBreed = _listing.Breed;
            ViewBag.SelectedGender = _listing.Gender;

            ViewBag.AgeYear = _listing.AgeYear;
            ViewBag.AgeMonth = _listing.AgeMonth;
            ViewBag.AgeWeek = _listing.AgeWeek;

            return View(listing);
        }

        [HttpPost]
        public IActionResult EditListing(EditListingViewModel model, int animalTypeId, string _class, string breed, string gender, int listingYears = 0, int listingMonths = 0, int listingWeeks = 0)
        {
            var user = GetLoggedInUser();
            var farm = _farmService.GetFarmByUserID(user.Id);

            var file = model.ImageFile;
            if (file != null)
            {
                string uri = FileService.SaveImage(file, $"{farm.Name + "_" + farm.ID}/Logo");
                if (string.IsNullOrEmpty(uri) || string.IsNullOrWhiteSpace(uri))
                    throw new FileSaveErrorException();

                model.ImagePath = uri;
            }
            else
            {
                model.ImagePath = "https://image.freepik.com/free-vector/farmer-peasant-illustration-man-with-beard-spade-farmland_33099-575.jpg";
            }
            
            Listing listing = _listingService.GetByID(model.ID);

            if (listing == null)
            {
                return NotFound();
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

            int ageConvertedToWeeks = (listingYears * 52) + (listingMonths * 4) + listingWeeks;
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
            try
            {
                if (ModelState.IsValid)
                {
                    listing.Title = model.Title;
                    listing.Description = model.Description;
                    listing.Price = model.Price;
                    listing.Location = model.Location;
                    listing.Age = listingAge + " old";
                    listing.AgeYear = listingYears;
                    listing.AgeMonth = listingMonths;
                    listing.AgeWeek = listingWeeks;
                    listing.AgeConvertedToWeeks = ageConvertedToWeeks;
                    listing.Gender = String.IsNullOrEmpty(gender) ? "None" : gender;
                    listing.AnimalType = animalTypeName;
                    listing.Breed = breed;
                    listing.Class = _class;
                    listing.ImagePath = String.IsNullOrEmpty(model.ImagePath) ? listing.ImagePath : model.ImagePath;
                    listing.Farm = farm;
                    _listingService.UpdateListing(listing);

                    return RedirectToAction(nameof(listing));
                }
                else
                {
                    ViewBag.InvalidModel = "one or more input is invalid";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists contact the administrator." + ex);
                throw;
            }
            return RedirectToAction(nameof(listing));
        }

        public IActionResult Listing()
        {
            var farm = _farmService.GetFarmByUserID(GetLoggedInUser().Id);
            var listing = _listingService.GetByFarm(farm.ID);


            IEnumerable<ViewLisitingViewModel> listingList = listing
                .Select(l => new ViewLisitingViewModel()
                {
                    Title = l.Title,
                    Price = l.Price,
                    Location = l.Location,
                    Description = l.Description,
                    AnimalType = l.AnimalType,
                    Age = l.Age,
                    Gender = l.Gender,
                    ID = l.ID
                }).OrderBy(o => o.ID).ToList();
            return View(listingList);
        }


        #region Helper Methods
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
            return Json(new { result = url, url = Url.Action(nameof(Index), "Seller") });
        }

        public ActionResult LoadClassAndBreed(int animalTypeId)
        {
            var animalType = _animalTypeService.GetByID(animalTypeId);
            var model = new AnimalTypeClassBreedViewModel();
            model.Classes = animalType != null ? animalType.Class.Split(',').ToList() : new List<string>();
            model.Breed = animalType != null ? animalType.Breed.Split(',').ToList() : new List<string>();

            return Json(model);
        }

        private ApplicationUser GetLoggedInUser()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return _userAccountService.GetById(userId);
        }
        #endregion
    }
}