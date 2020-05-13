using AgroCommerce.Core;
using AgroCommerce.Data.Contracts;
using AgroCommerce.Data.Implementations;
using AgroCommerce.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroCommerce.Services.Implementations
{
    public class ListingService : IListingService
    {
        private readonly UnitOfWork _uow;
        public ListingService(IUnitOfWork uow)
        {
            this._uow = uow as UnitOfWork;
        }

        public async Task Create(Listing listing)
        {
            _uow.ListingRepo.Add(listing);
            await _uow.Save();
        }

        public async Task DeleteListing(Listing listing)
        {
            _uow.ListingRepo.Remove(listing);
            await _uow.Save();
        }

        public IEnumerable<Listing> GetAll()
        {
            return _uow.ListingRepo.GetAll();
        }

        public IEnumerable<Listing> GetByFarm(long farmId)
        {
            return _uow.ListingRepo.GetByFarmId(farmId);
        }

        public IEnumerable<Listing> GetAllWithImage()
        {
            return _uow.ListingRepo.Find(l => l.ImagePath != null);
        }
        public IQueryable<Listing> QueryAll()
        {
            return _uow.ListingRepo.QueryAllListings();
        }

        public Listing GetByID(long id)
        {
            return _uow.ListingRepo.Get(id);
        }
        public async Task UpdateListing(Listing listing)
        {
            _uow.ListingRepo.Update(listing);
            await _uow.Save();
        }
        public int GetTotalNumberOfListing()
        {
            return _uow.ListingRepo.Count();
        }

        public int GetNumberOfListingOfAUser(long farmId)
        {
            return _uow.ListingRepo.GetAll().Where(l => l.FarmId == farmId).Count();
        }
    }
}
