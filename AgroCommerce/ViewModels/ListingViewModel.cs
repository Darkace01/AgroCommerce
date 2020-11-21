using AgroCommerce.Core;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AgroCommerce.ViewModels
{
    public class ListingViewModel
    {

    }

    public class ViewLisitingViewModel
    {
        public long ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Location { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public string AnimalType { get; set; }
    }
    public class AddListingViewModel
    {
        public long ID { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Location { get; set; }
        public string AnimalType { get; set; }
        public string Class { get; set; }
        public string Breed { get; set; }
        //public int Age { get; set; }
        public string Type { get; set; }

        public int FarmId { get; set; }
        public virtual Farm Farm { get; set; }

        //Listings should have user


        [Display(Name = "Upload Image")]
        public string ImagePath { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }

    public class EditListingViewModel
    {
        public long ID { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Location { get; set; }
        public string AnimalType { get; set; }
        public string Class { get; set; }
        public string Breed { get; set; }
        //public int Age { get; set; }
        public string Type { get; set; }

        public int FarmId { get; set; }
        public virtual Farm Farm { get; set; }

        //Listings should have user


        [Display(Name = "Upload Image")]
        public string ImagePath { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
