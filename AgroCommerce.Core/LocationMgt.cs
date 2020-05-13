using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AgroCommerce.Core
{
    public class LocationMgt : Entity
    {
        public string State { get; set; }
        public string LocalGovtArea { get; set; }
        public string City { get; set; }
        [Display(Name = "Is Clustered")]
        public bool IsClustered { get; set; }
    }
}
