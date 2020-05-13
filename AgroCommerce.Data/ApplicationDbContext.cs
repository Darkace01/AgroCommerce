﻿using AgroCommerce.Core;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgroCommerce.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        public DbSet<Farm> Farm { get; set; }
        public DbSet<Listing> Listing { get; set; }
        public DbSet<AnimalType> AnimalType { get; set; }
        public DbSet<LocationMgt> LocationMgt { get; set; }
        public DbSet<Transaction> Transaction { get; set; }
        public DbSet<Review> Review { get; set; }

    }
}
