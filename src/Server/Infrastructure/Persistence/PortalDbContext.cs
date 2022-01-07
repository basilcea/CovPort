using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class PortalDbContext : DbContext
    {
        public PortalDbContext(DbContextOptions<PortalDbContext> options)
            : base(options)
        {
        }

        public DbSet<Booking> Booking { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<Result> Result { get; set; }
        public DbSet<Space> Space { get; set; }
        public Dbset<User> User {get;set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>().HasKey(x => x.Id);

            modelBuilder.Entity<Space>().HasKey(x => x.Id);
            modelBuilder.Entity<Space>().HasIndex(x => new { x.LocationId, x.Date });

            modelBuilder.Entity<Result>().HasKey(x => x.Id);
            modelBuilder.Entity<User>().HasKey(x =>x.Id);

            modelBuilder.Entity<Location>().HasIndex(x => x.Id);
        }
        
    }
}