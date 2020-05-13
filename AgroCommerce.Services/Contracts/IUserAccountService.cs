using AgroCommerce.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AgroCommerce.Services.Contracts
{
    public interface IUserAccountService
    {
        int GetTotalNumberOfUsersInDb();
        ApplicationUser GetById(string Id);
        ApplicationUser GetByUsername(string username);
        Task Update(ApplicationUser user);
        bool IsAccountComplete(string userId);
        ApplicationUser GetByFarmID(long id);
        int GetTotalNumberOfBuyers();
        int GetTotalNumberOfSellers();
        ApplicationUser GetUSerByPhoneNumber(string phoneNumber);
    }
}
