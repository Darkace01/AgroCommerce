using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AgroCommerce.Core;
using AgroCommerce.Services.Contracts;
using AgroCommerce.Utilities;
using AgroCommerce.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public SellerController(IUserAccountService userAccountService, IFarmService farmService, IListingService listingService, IAnimalTypeService animalTypeService, ITransactionService transactionService, IReviewService reviewService)
        {
            _userAccountService = userAccountService;
            _farmService = farmService;
            _listingService = listingService;
            _animalTypeService = animalTypeService;
            _transactionService = transactionService;
            _reviewService = reviewService;
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

            
            FarmAddViewModel farm = new FarmAddViewModel();
            farm.ImagePath = "https://image.freepik.com/free-vector/farmer-peasant-illustration-man-with-beard-spade-farmland_33099-575.jpg";
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
                                throw new Exception("File Save error");

                            farm.ImagePath = uri;
                        }
                        else
                        {
                            throw new Exception("File not found");
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
                            throw new Exception("File Save error");

                        _farm.ImagePath = uri;
                    }
                    else
                    {
                        throw new Exception("File not found");
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
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists contact the administrator." + ex);
                throw;
            }
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