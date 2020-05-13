using AgroCommerce.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AgroCommerce.Services.Contracts
{
    public interface IFarmService
    {
        Task SetupFarm(Farm farm, ApplicationUser farmOwner);
        Task SaveChanges();
        IEnumerable<Farm> GetAll();
        Farm GetByID(long id);
        Task UpdateFarm(Farm farm);
        int GetAllFarmNumber(string id);
    }
}
