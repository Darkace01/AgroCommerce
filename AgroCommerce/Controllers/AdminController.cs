using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgroCommerce.Core;
using AgroCommerce.Services.Contracts;
using AgroCommerce.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AgroCommerce.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUserAccountService _userAccountService;
        private readonly IFarmService _farmService;
        private readonly IListingService _listingService;
        private readonly IAnimalTypeService _animalTypeService;
        private readonly ITransactionService _transactionService;
        private readonly IReviewService _reviewService;
        public AdminController(IUserAccountService userAccountService, IFarmService farmService, IListingService listingService, IAnimalTypeService animalTypeService, ITransactionService transactionService, IReviewService reviewService)
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
            return View();
        }

        public IActionResult AnimalTypes()
        {
            return View(_animalTypeService.GetAll());
        }

        public IActionResult AddAnimalType()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddAnimalType(AnimalTypeViewModel model, List<String> classTags, List<String> breedTags)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string classT = classTags != null ? String.Join(",", classTags) : "";
                    string breedT = breedTags != null ? String.Join(",", breedTags) : "";
                    AnimalType animalType = new AnimalType
                    {
                        Name = model.Name,
                        Class = classT,
                        Breed = breedT,
                    };

                    _animalTypeService.Create(animalType);
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return RedirectToAction(nameof(AnimalTypes));
        }
        
        public IActionResult EditAnimalType(int id = 0)
        {
            if (id == 0) return new BadRequestResult();

            AnimalType animalType = _animalTypeService.GetByID(id);

            if(animalType == null)
            {
                return NotFound();
            }

            AnimalTypeViewModel animal = new AnimalTypeViewModel()
            {
                ID = animalType.ID,
                Name = animalType.Name,
                Class = animalType.Class,
                Breed = animalType.Class
            };

            List<string> c = animalType.Class.Split(',').ToList();
            List<string> b = animalType.Breed.Split(',').ToList();

            ViewBag.ClassTags = c;
            ViewBag.BreedTags = b;
            return View(animal);
        }

        [HttpPost]
        public IActionResult EditAnimalType(AnimalTypeViewModel model, List<String> classTags, List<String> breedTags)
        {
            if (ModelState.IsValid)
            {

                string classT = classTags != null ? String.Join(",", classTags) : "";
                string breedT = breedTags != null ? String.Join(",", breedTags) : "";
                AnimalType animalType = _animalTypeService.GetByID(model.ID);
                animalType.Name = model.Name;
                animalType.Class = classT;
                animalType.Breed = breedT;

                _animalTypeService.UpdateAnimalType(animalType);
            }
            return RedirectToAction("AnimalTypes");
        }

        public IActionResult AnimalTypeDetails(int? id)
        {
            if (id == null) return new BadRequestResult();
            AnimalType animalType = _animalTypeService.GetByID(id.Value);
            if (animalType == null)
            {
                return NotFound();
            }
            return View(animalType);
        }
    }
}