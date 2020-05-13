using AgroCommerce.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroCommerce.Services.Contracts
{
    public interface IListingService
    {
        Task Create(Listing listing);
        IEnumerable<Listing> GetByFarm(long farmId);
        IEnumerable<Listing> GetAllWithImage();
        Listing GetByID(long id);
        Task UpdateListing(Listing listing);
        Task DeleteListing(Listing listing);
        int GetTotalNumberOfListing();
        int GetNumberOfListingOfAUser(long farmId);
        IQueryable<Listing> QueryAll();
    }
}
