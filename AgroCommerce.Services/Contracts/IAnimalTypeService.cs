using AgroCommerce.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AgroCommerce.Services.Contracts
{
    public interface IAnimalTypeService
    {
        Task Create(AnimalType animalType);
        IEnumerable<AnimalType> GetAll();
        AnimalType GetByID(long id);
        Task UpdateAnimalType(AnimalType animalType);
        Task DeleteAnimalType(AnimalType animalType);
        long GetIdByAnimalTypeName(string animalTypeName);
    }
}
