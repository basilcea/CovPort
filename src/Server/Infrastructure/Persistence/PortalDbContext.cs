using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class PortalDbContext : DbContext
    {
        public PortalDbContext(DbContextOptions<PortalDbContext> options): base(options)
        {
        }

        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<Space> Spaces { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>().HasKey(x => x.Id);
          
            modelBuilder.Entity<Space>().HasKey(x => x.Id);
            modelBuilder.Entity<Space>().HasIndex(x => new { x.LocationName, x.Date}).IsUnique();

            modelBuilder.Entity<Result>().HasKey(x => x.Id);
            modelBuilder.Entity<Result>().HasIndex(x => x.BookingId).IsUnique();
            
            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<User>().HasIndex(x => x.Email).IsUnique();

           
        }
    }
}