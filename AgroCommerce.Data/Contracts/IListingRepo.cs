using AgroCommerce.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgroCommerce.Data.Contracts
{
    public interface IListingRepo : ICoreRepo<Listing>
    {
        List<Listing> GetByFarmId(long farmId);
        IQueryable<Listing> QueryAllListings();
    }
}
