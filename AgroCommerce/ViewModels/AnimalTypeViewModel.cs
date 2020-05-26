using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgroCommerce.ViewModels
{
    public class AnimalTypeViewModel
    {
        public long ID { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Name must be atleast 3 characters long and not more than 20")]
        public string Name { get; set; }
        public string Class { get; set; }
        public string Breed { get; set; }
    }

    public class AnimalTypeClassBreedViewModel
    {
        public List<string> Classes { get; set; }
        public List<string> Breed { get; set; }

    }
}
