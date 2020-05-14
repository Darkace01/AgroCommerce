using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AgroCommerce.Core;
using AgroCommerce.Services.Contracts;
using AgroCommerce.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AgroCommerce.Controllers
{
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
                    if(farm.ImagePath != null)
                    {
                        farm.ImagePath = "https://image.freepik.com/free-vector/farmer-peasant-illustration-man-with-beard-spade-farmland_33099-575.jpg";
                    }

                    Farm _farm = new Farm()
                    {
                        Name = farm.Name,
                        Location = farm.Location,
                        ImagePath = "https://image.freepik.com/free-vector/farmer-peasant-illustration-man-with-beard-spade-farmland_33099-575.jpg",
                        ApplicationUserId = user.Id,
                        IsActive = true,
                    };
                    _farmService.SetupFarm(_farm, user);
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

        #region Helper Methods
        private ApplicationUser GetLoggedInUser()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return _userAccountService.GetById(userId);
        }
        #endregion
    }
}