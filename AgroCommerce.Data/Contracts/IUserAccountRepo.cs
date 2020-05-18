using AgroCommerce.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgroCommerce.Data.Contracts
{
    public interface IUserAccountRepo : ICoreRepo<ApplicationUser>
    {
        IEnumerable<ApplicationUser> GetAllUsersWithRelationships();
        ApplicationUser GetUserWithRelationships(string userId);
    }
}
