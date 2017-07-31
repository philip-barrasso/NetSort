using Microsoft.EntityFrameworkCore;
using NetSort.Samples.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetSort.Samples.Api
{
    public class PersistentDbContext : DbContext
    {
        public PersistentDbContext(DbContextOptions<PersistentDbContext> options)
            : base(options)
        {
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<House> Houses { get; set; }
        public DbSet<Person> Persons { get; set; }
    }
}
