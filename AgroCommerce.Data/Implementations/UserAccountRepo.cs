using AgroCommerce.Core;
using AgroCommerce.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgroCommerce.Data.Implementations
{
    public class UserAccountRepo : CoreRepo<ApplicationUser>, IUserAccountRepo
    {
        public UserAccountRepo(ApplicationDbContext ctx) : base(ctx)
        {

        }
    }
}
