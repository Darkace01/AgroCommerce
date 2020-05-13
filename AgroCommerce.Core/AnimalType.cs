using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AgroCommerce.Core
{
    public class AnimalType : Entity
    {
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Name { get; set; }
        public string Class { get; set; }
        public string Breed { get; set; }

        public int T { get; set; }
    }

}
