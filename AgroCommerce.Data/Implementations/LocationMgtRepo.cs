using AgroCommerce.Core;
using AgroCommerce.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgroCommerce.Data.Implementations
{
    public class LocationMgtRepo : CoreRepo<LocationMgt>, ILocationMgtRepo
    {
        public LocationMgtRepo(ApplicationDbContext ctx) : base(ctx)
        {

        }
    }
}
