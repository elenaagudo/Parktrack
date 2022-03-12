using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Context
{
    public class ParktrackContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Master> Master { get; set; }
        public DbSet<Car> Car { get; set; }
        public DbSet<CarLocation> CarLocation { get; set; }
        public DbSet<Manufacturer> Manufacturer { get; set; }
        public DbSet<Log> Log { get; set; }

        public ParktrackContext(DbContextOptions<ParktrackContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

    }
}
