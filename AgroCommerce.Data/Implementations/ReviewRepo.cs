using AgroCommerce.Core;
using AgroCommerce.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgroCommerce.Data.Implementations
{
    public class ReviewRepo : CoreRepo<Review>, IReviewRepo
    {
        public ReviewRepo(ApplicationDbContext ctx) : base(ctx)
        {

        }
    }
}
