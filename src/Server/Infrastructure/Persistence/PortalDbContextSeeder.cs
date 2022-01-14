
using System;
using System.Collections.Generic;
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
            }

            if (!_dbContext.Users.Any())
            {
                _dbContext.Users.AddRange(AddUsers());
            }
            if (!_dbContext.Spaces.Any())
            {
                _dbContext.Spaces.AddRange(AddSpaces());

            }
            if (!_dbContext.Bookings.Any())
            {
                _dbContext.Bookings.AddRange(AddBookings());
            }

            if (!_dbContext.Results.Any())
            {
                _dbContext.Results.AddRange(AddResults());
            }

            await _dbContext.SaveChangesAsync();
        }

        private static IEnumerable<Space> AddSpaces()
        {
            return new List<Space>
           {
                new Space()
                {
                    LocationName = "SEYCHELLES",
                    Date = DateTime.Today,
                    SpacesAvailable = 30,
                    SpacesCreated = 30,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,

                },
                new Space()
                {
                    LocationName = "MALTA",
                    Date = DateTime.Today,
                    SpacesAvailable = 20,
                    SpacesCreated = 20,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,

                },
                new Space()
                {
                    LocationName = "GREENLAND",
                    Date = DateTime.Today,
                    SpacesAvailable = 30,
                    SpacesCreated = 30,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,

                },
                new Space()
                {
                    LocationName = "SEYCHELLES",
                    Date = DateTime.Today.AddDays(1),
                    SpacesAvailable = 30,
                    SpacesCreated = 30,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,

                },
                new Space()
                {
                    LocationName = "MALTA",
                    Date = DateTime.Today.AddDays(1),
                    SpacesAvailable = 20,
                    SpacesCreated = 20,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,

                },
                new Space()
                {
                    LocationName = "GREENLAND",
                    Date = DateTime.Today.AddDays(1),
                    SpacesAvailable = 30,
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
                {
                    Email = "admin.cea@covport.check",
                    Name = "cea",
                    UserRole = "ADMIN",
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                },

                new User()
                {

                    Email = "labadmin.liwu@covport.check",
                    Name = "liwu",
                    UserRole = "LABADMIN",
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,

                },
                new User()
                {
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
                    UserId = 1,
                    SpaceId = 1,
                    Status = "COMPLETED",
                    TestType ="RAPID",
                    LocationName ="SEYCHELLES",
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now
                },

                new Booking()
                {
                    UserId = 1,
                    SpaceId = 4,
                    Status = "PENDING",
                    TestType ="PCR",
                    LocationName ="SEYCHELLES",
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now
                },
                new Booking()
                {
                    UserId = 2,
                    SpaceId = 2,
                    Status = "COMPLETED",
                    TestType ="PCR",
                    LocationName ="MALTA",
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now
                },

                new Booking()
                {
                    UserId = 2,
                    SpaceId = 5,
                    Status = "CANCELLED",
                    TestType ="PCR",
                    LocationName ="MALTA",
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now


                },
                new Booking()
                {
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

                   BookingId = 3,
                   UserId = 2,
                   TestLocation = "MALTA",
                   TestType = "PCR",
                   Status = "PENDING",
                   DateCreated=DateTime.Now,
                   DateUpdated= DateTime.Now

                },
                new Result()
                {
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