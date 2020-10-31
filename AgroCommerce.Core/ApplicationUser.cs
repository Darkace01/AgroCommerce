using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AgroCommerce.Core
{
    public class ApplicationUser : IdentityUser
    {
        public enum Gender
        {
            Male, Female
        }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string Address { get; set; }
        public string LocalGovtArea { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Email_ { get; set; }
        public Gender Gender_ { get; set; }
        public string ZipCode { get; set; }
        public string role { get; set; }

        public bool IsBasicProfileDetailsSet { get; set; }
        public bool IsAccountComplete { get; set; }

        [Display(Name = "Upload Image")]
        public string ImagePath { get; set; }
        public virtual long FarmId { get; set; }
        public virtual List<Review> Reviews { get; set; }
        public virtual List<Transaction> Transactions { get; set; }

    }
}
