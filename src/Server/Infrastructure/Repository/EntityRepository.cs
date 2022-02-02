using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Interfaces;
using Infrastructure.Persistence;
using System;
using Microsoft.EntityFrameworkCore;
using Application.Exceptions;
using Microsoft.Extensions.Logging;
using Domain.Entities;
using Domain.ValueObjects;
using System.Linq;

namespace Infrastructure.Repository
{
    public class EntityRepository<T> : IEntityRepository<T> where T : class, IEntity
    {

        protected readonly PortalDbContext _dbContext;
        protected readonly ILogger<T> _logger;

        public EntityRepository(PortalDbContext DbContext, ILogger<T> logger)
        {
            _dbContext = DbContext;
            _logger = logger;
        }
        public async virtual Task<IEnumerable<T>> Get()
        {
            try
            {
                var result = await _dbContext.Set<T>().ToListAsync();
                _logger.LogInformation("Received {@type} response: {@response}", typeof(T).Name, result);
                return result;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"An sql error occurred:-  {ex.Message}");
                throw new UserDefinedSQLException();

            }
        }
        public virtual async Task<T> GetById(int id)
        {
            try
            {
                var result = await _dbContext.Set<T>().FindAsync(id);
                if (result == null)
                {
                    throw new NotFoundException($"{typeof(T).Name} Not Found");
                }
                _logger.LogInformation("Received {@type} response: {@response}", typeof(T).Name, result);
                return result;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"An sql error occurred:-  {ex.Message}");
                throw new UserDefinedSQLException();

            }
        }
        public async virtual Task<T> Insert(T entity, int requesterId)
        {
            try
            {
                return await InsertEntity(entity, _dbContext, _logger);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"An sql error occurred:-  {ex.Message}");
                throw new UserDefinedSQLException();

            }
        }

        public virtual Task<IEnumerable<T>> GetByFilter(string filter)
        {
            throw new System.NotImplementedException();
        }

        public virtual Task<T> Update(T entity, int requesterId)
        {
            throw new System.NotImplementedException();
        }
        public static async Task<T> InsertEntity(T entity, PortalDbContext _dbContext, ILogger<T> logger)
        {
            var now = DateTime.Now;
            entity.DateCreated = now;
            entity.DateUpdated = now;
            var savedEntity = (await _dbContext.Set<T>().AddAsync(entity)).Entity;
            await _dbContext.SaveChangesAsync();
            logger.LogInformation("{@type} created and saved: {@response}", typeof(T).Name, savedEntity);
            return savedEntity;
        }

        public static async Task<T> UpdateEntity(T entity, PortalDbContext _dbContext, ILogger<T> logger)
        {
            entity.DateUpdated = DateTime.Now;
            await _dbContext.SaveChangesAsync();
            logger.LogInformation("{@type} updated and saved: {@response}", typeof(T).Name, entity);
            return entity;
        }

        public static async Task<Space> GetSpaceById(int Id, PortalDbContext _dbContext, ILogger<T> logger)
        {

            var space = await _dbContext.Spaces.FindAsync(Id);
            if (space == null)
            {
                throw new NotFoundException("Space Not Found");
            }
            return space;

        }

        
    }
}