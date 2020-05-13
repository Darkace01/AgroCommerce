using AgroCommerce.Core;
using AgroCommerce.Data.Contracts;
using AgroCommerce.Data.Implementations;
using AgroCommerce.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AgroCommerce.Services.Implementations
{
    public class LocationMgtService : ILocationMgtService
    {
        private readonly UnitOfWork _uow;
        public LocationMgtService(IUnitOfWork uow)
        {
            this._uow = uow as UnitOfWork;
        }


        public async Task Create(LocationMgt location)
        {
            _uow.LocationMgtRepo.Add(location);
            await _uow.Save();
        }

        public async Task DeleteLocation(LocationMgt location)
        {
            _uow.LocationMgtRepo.Remove(location);
            await _uow.Save();
        }

        public IEnumerable<LocationMgt> GetAll()
        {
            return _uow.LocationMgtRepo.GetAll();
        }

        public LocationMgt GetByID(long id)
        {
            return _uow.LocationMgtRepo.Get(id);
        }

        public async Task UpdateLocation(LocationMgt location)
        {
            _uow.LocationMgtRepo.Update(location);
            await _uow.Save();
        }
    }
}
