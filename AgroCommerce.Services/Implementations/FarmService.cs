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
    public class FarmService : IFarmService
    {
        private readonly UnitOfWork _uow;
        public FarmService(IUnitOfWork uow)
        {
            this._uow = uow as UnitOfWork;
        }

        public async Task SaveChanges()
        {
            await _uow.Save();
        }

        public IEnumerable<Farm> GetAll()
        {
            return _uow.FarmRepo.GetAll();
        }

        public Farm GetByID(long Id)
        {
            return _uow.FarmRepo.Get(Id);
        }

        public Farm GetFarmByUserID(string userId)
        {
            return _uow.FarmRepo.GetAll().Where(f => f.FarmOwner.Id == userId).FirstOrDefault();
        }

        public async Task SetupFarm(Farm farm, ApplicationUser farmOwner)
        {
            farmOwner.IsAccountComplete = true;
            _uow.UserAccountRepo.Update(farmOwner);

            farm.FarmOwner = farmOwner;
            _uow.FarmRepo.Add(farm);

            await _uow.Save();
        }

        public async Task UpdateFarm(Farm farm)
        {
            _uow.FarmRepo.Update(farm);
            await _uow.Save();
        }

        public int GetAllFarmNumber(string id)
        {
            return _uow.FarmRepo.GetAll().Where(f => f.ApplicationUserId == id).Count();
        }
    }
}
