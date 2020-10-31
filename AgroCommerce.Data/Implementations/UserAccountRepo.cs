using AgroCommerce.Core;
using AgroCommerce.Data.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgroCommerce.Data.Implementations
{
    public class UserAccountRepo : CoreRepo<ApplicationUser>, IUserAccountRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<ApplicationUser> _DbSet;
        public UserAccountRepo(ApplicationDbContext ctx) : base(ctx)
        {
            this._context = ctx;
            this._DbSet = this._context.Set<ApplicationUser>();
        }

        public IEnumerable<ApplicationUser> GetAllUsersWithRelationships()
        {
            return _DbSet
                //.Include(c => c.Farm)
                .Include(c => c.Reviews)
                .Include(c => c.Transactions)
                .ToList();
        }

        public ApplicationUser GetUserWithRelationships(string userId)
        {
            return _DbSet
                .Where(c => c.Id == userId)
                //.Include(c => c.Farm)
                .Include(c => c.Reviews)
                .Include(c => c.Transactions)
                .FirstOrDefault();
        }
    }
}
