using AgroCommerce.Core;
using AgroCommerce.Data.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgroCommerce.Data.Implementations
{
    public class ListingRepo : CoreRepo<Listing>, IListingRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Listing> _DbSet;
        public ListingRepo(ApplicationDbContext ctx) : base(ctx)
        {
            this._context = ctx;
            this._DbSet = this._context.Set<Listing>();
        }

        public List<Listing> GetByFarmId(long farmId)
        {
            return _DbSet.Where(l => l.Farm.ID == farmId).ToList();
        }
        public IQueryable<Listing> QueryAllListings()
        {
            return _DbSet.Where(a => a.Farm.IsActive && a.ImagePath != null);
           
        }
    }
}
