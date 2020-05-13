using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AgroCommerce.Core
{
    public class Review : Entity
    {
        [Required]
        public int TransactionId { get; set; }
        [Required]
        public virtual Transaction Transaction { get; set; }

        [Display(Name = "Review Comment")]
        public string ReviewComment { get; set; }

        public long Y { get; set; }
    }
}
