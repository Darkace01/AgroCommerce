using System;
using System.Collections.Generic;
using System.Text;

namespace AgroCommerce.Core
{
    public class Transaction : Entity
    {
        public enum Progress
        {
            Processing, Successful, Failed
        }

        public string BuyerId { get; set; }
        public virtual ApplicationUser Buyer { get; set; }

        public string SellerId { get; set; }
        public virtual ApplicationUser Seller { get; set; }

        public long ListingId { get; set; }
        public Listing Listing { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Progress progress { get; set; }

        public DateTime? TransactionDate { get; set; }

        public virtual List<Review> Reviews { get; set; }
    }
}
