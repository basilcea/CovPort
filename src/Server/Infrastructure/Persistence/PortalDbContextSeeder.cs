
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

            await _dbContext.Database.MigrateAsync();

            if (!_dbContext.Users.Any())
            {
                _dbContext.Users.AddRange(AddUsers());
            }
            if (!_dbContext.Spaces.Any())
            {
                _dbContext.Spaces.AddRange(AddSpaces());

            }
            await _dbContext.SaveChangesAsync();
        }

        private static IEnumerable<Space> AddSpaces()
        {
            return new List<Space>
           {
    new Space()
                {
                    LocationName = "Seychelles",
                    Date = DateTime.Parse("2022-01-20"),
                    SpacesAvailable = 30,
                    SpacesCreated = 30,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,

                },
                new Space()
                {
                    LocationName = "Malta",
                    Date = DateTime.Parse("2022-01-20"),
                    SpacesAvailable = 20,
                    SpacesCreated = 20,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,

                },
                new Space()
                {   
                    LocationName = "Seychelles",
                    Date = DateTime.Parse("2022-01-21"),
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
    }
}