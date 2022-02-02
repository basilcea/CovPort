
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class PortalDbContextSeeder
    {
        private readonly PortalDbContext _dbContext;
        public PortalDbContextSeeder(PortalDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Seed()
        {
           
            if ((await _dbContext.Database.GetPendingMigrationsAsync()).Any())
            {
                await _dbContext.Database.MigrateAsync();
                string reportScript = File.ReadAllText("../Infrastructure/SQL/ReportView.sql");
                string userScript = File.ReadAllText("../Infrastructure/SQL/UserSummaryView.sql");
                await _dbContext.Database.ExecuteSqlRawAsync(reportScript);
                await _dbContext.Database.ExecuteSqlRawAsync(userScript);
            }
             _dbContext.Database.OpenConnection();

            if (!_dbContext.Users.Any())
            {  
                await _dbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Users ON");
                _dbContext.Users.AddRange(AddUsers());
                await _dbContext.SaveChangesAsync();
                await _dbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Users OFF");
              
            }
            if (!_dbContext.Spaces.Any())
            {
                await _dbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Spaces ON");
                _dbContext.Spaces.AddRange(AddSpaces());      
                await _dbContext.SaveChangesAsync();
                await _dbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Spaces OFF");
            

            }
            if (!_dbContext.Bookings.Any())
            {
               await _dbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Bookings ON");
               _dbContext.Bookings.AddRange(AddBookings());
                await _dbContext.SaveChangesAsync();
                await _dbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Bookings OFF");
              
            }

            if (!_dbContext.Results.Any())
            {
                await _dbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Results ON");
                _dbContext.Results.AddRange(AddResults());
                await _dbContext.SaveChangesAsync();
                await _dbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Results OFF");
            }
              _dbContext.Database.CloseConnection();
        }

        private static IEnumerable<Space> AddSpaces()
        {
            return new List<Space>
           {
                new Space()
                {
                    Id = 1,
                    LocationName = "SEYCHELLES",
                    Date = DateTime.Today,
                    SpacesCreated = 30,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,

                },
                new Space()
                {
                    Id = 2,
                    LocationName = "MALTA",
                    Date = DateTime.Today,
                    SpacesCreated = 20,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,

                },
                new Space()
                {
                    Id = 3,
                    LocationName = "GREENLAND",
                    Date = DateTime.Today,
                    SpacesCreated = 30,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,

                },
                new Space()
                {   Id = 4,
                    LocationName = "SEYCHELLES",
                    Date = DateTime.Today.AddDays(1),
                    SpacesCreated = 30,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,

                },
                new Space()
                {
                    Id = 5,
                    LocationName = "MALTA",
                    Date = DateTime.Today.AddDays(1),
                    SpacesCreated = 20,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,

                },
                new Space()
                {
                    Id = 6,
                    LocationName = "GREENLAND",
                    Date = DateTime.Today.AddDays(1),
                    SpacesCreated = 30,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                }

            };

        }
        private static IEnumerable<User> AddUsers()
        {
            return new List<User>
            {
                 new User()
                {   Id = 1,
                    Email = "admin.cea@covport.check",
                    Name = "cea",
                    UserRole = "ADMIN",
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                },

                new User()
                {   Id = 2,
                    Email = "labadmin.liwu@covport.check",
                    Name = "liwu",
                    UserRole = "LABADMIN",
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,

                },
                new User()
                {
                    Id = 3,
                    Email = "me.patient@gmail.com",
                    Name = "Andres",
                    UserRole = "USER",
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                }
            };
        }

        private static IEnumerable<Booking> AddBookings()
        {
            return new List<Booking>
            {

                new Booking()
                {
                    Id = 1,
                    UserId = 1,
                    SpaceId = 1,
                    Status = "COMPLETED",
                    TestType ="RAPID",
                    LocationName ="SEYCHELLES",
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now
                },

                new Booking()
                {   Id = 2,
                    UserId = 1,
                    SpaceId = 4,
                    Status = "PENDING",
                    TestType ="PCR",
                    LocationName ="SEYCHELLES",
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now
                },
                new Booking()
                {   Id = 3,
                    UserId = 2,
                    SpaceId = 2,
                    Status = "COMPLETED",
                    TestType ="PCR",
                    LocationName ="MALTA",
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now
                },

                new Booking()
                {   Id = 4,
                    UserId = 2,
                    SpaceId = 5,
                    Status = "CANCELLED",
                    TestType ="PCR",
                    LocationName ="MALTA",
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now


                },
                new Booking()
                {   Id =5,
                    UserId = 3,
                    SpaceId = 3,
                    Status = "PENDING",
                    TestType ="PCR",
                    LocationName ="GREENLAND",
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now
                },
                   new Booking()
                {
                    Id = 6,
                    UserId = 3,
                    SpaceId = 1,
                    Status = "COMPLETED",
                    TestType ="PCR",
                    LocationName ="SEYCHELLES",
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now
                },

                new Booking()
                {
                    Id = 7,
                    UserId = 3,
                    SpaceId = 6,
                    Status = "PENDING",
                    TestType ="RAPID",
                    LocationName ="MALTA",
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now

                }

            };
        }


        private static IEnumerable<Result> AddResults()
        {
            return new List<Result>
            {
                 new Result()
                {
                   Id = 1,
                   BookingId = 1,
                   UserId = 1,
                   TestLocation = "SEYCHELLES",
                   TestType = "RAPID",
                   Status = "PENDING",
                   DateCreated=DateTime.Now,
                   DateUpdated= DateTime.Now
                },

                new Result()
                {
                   Id =2,
                   BookingId = 3,
                   UserId = 2,
                   TestLocation = "MALTA",
                   TestType = "PCR",
                   Status = "PENDING",
                   DateCreated=DateTime.Now,
                   DateUpdated= DateTime.Now

                },
                new Result()
                {  Id= 3,
                   BookingId = 6,
                   UserId = 3,
                   TestLocation = "SEYCHELLES",
                   TestType = "PCR",
                   Status = "COMPLETED",
                   Positive = true,
                   DateCreated=DateTime.Now,
                   DateUpdated= DateTime.Now

                }
            };
        }


    }
}