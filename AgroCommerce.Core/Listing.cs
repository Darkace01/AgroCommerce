using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AgroCommerce.Core
{
    public class Listing : Entity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Location { get; set; }
        public string AnimalType { get; set; }
        public string Class { get; set; }
        public string Breed { get; set; }
        public string Age { get; set; }
        public int? AgeYear { get; set; }
        public int? AgeMonth { get; set; }
        public int? AgeWeek { get; set; }
        public int AgeConvertedToWeeks { get; set; }

        public string Gender { get; set; }
        public long FarmId { get; set; }
        public virtual Farm Farm { get; set; }
        //Listings should have user

        public virtual List<Review> Reviews { get; set; }

        public int T { get; set; }
        [Display(Name = "Upload Image")]
        public string ImagePath { get; set; }
    }
}
