using AgroCommerce.Core;
using AgroCommerce.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgroCommerce.Data.Implementations
{
    public class FarmRepo : CoreRepo<Farm>, IFarmRepo
    {
        public FarmRepo(ApplicationDbContext ctx) : base(ctx)
        {

        }

    }
}
