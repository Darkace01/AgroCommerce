using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgroCommerce.ViewModels
{
    public class FarmAddViewModel
    {
        [Required]
        public string Name { get; set; }
        public string Location { get; set; }

        public string ImagePath { get; set; }

        [Display(Name = "Farm Logo")]
        [DataType(DataType.Upload)]
        public IFormFile FarmLogoImage { get; set; }
    }
}
