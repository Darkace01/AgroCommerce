using AgroCommerce.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AgroCommerce.Services.Contracts
{
    public interface ILocationMgtService
    {
        Task Create(LocationMgt location);
        Task DeleteLocation(LocationMgt location);
        IEnumerable<LocationMgt> GetAll();
        LocationMgt GetByID(long id);
        Task UpdateLocation(LocationMgt location);

    }
}
