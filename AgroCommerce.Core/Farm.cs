using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AgroCommerce.Core
{
    public class Farm : Entity
    {
        public string Name { get; set; }
        public string Location { get; set; }


        public string ApplicationUserId { get; set; }
        [Required]
        public virtual ApplicationUser FarmOwner { get; set; }
        public virtual List<Listing> Listings { get; set; }

        public string ImagePath { get; set; }
        public bool IsActive { get; set; }
    }
}
