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
    public class AnimalTypeService : IAnimalTypeService
    {
        private readonly UnitOfWork _uow;
        public AnimalTypeService(IUnitOfWork uow)
        {
            this._uow = uow as UnitOfWork;
        }
        public async Task Create(AnimalType animalType)
        {
            _uow.AnimalTypeRepo.Add(animalType);
            await _uow.Save();
        }


        public IEnumerable<AnimalType> GetAll()
        {
            return _uow.AnimalTypeRepo.GetAll();
        }

        public AnimalType GetByID(long id)
        {
            return _uow.AnimalTypeRepo.Get(id);
        }

        public async Task UpdateAnimalType(AnimalType animalType)
        {
            _uow.AnimalTypeRepo.Update(animalType);
            await _uow.Save();
        }

        public async Task DeleteAnimalType(AnimalType animalType)
        {
            _uow.AnimalTypeRepo.Remove(animalType);
            await _uow.Save();
        }

        public long GetIdByAnimalTypeName(string animalTypeName)
        {
            return _uow.AnimalTypeRepo.Find(a => String.Compare(animalTypeName, a.Name, true) == 0).FirstOrDefault().ID;
        }
    }
}
