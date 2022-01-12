using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Interfaces;
using Infrastructure.Persistence;
using System;
using Microsoft.EntityFrameworkCore;
using Application.Exceptions;

namespace Infrastructure.Repository
{
    public  class EntityRepository<T> : IEntityRepository<T> where T : class, IEntity
    {

        protected readonly PortalDbContext DbContext;

        public EntityRepository(PortalDbContext dbContext)
        {
            DbContext = dbContext;
        }
        public async virtual Task<IEnumerable<T>> Get()
        {
            return await DbContext.Set<T>().ToListAsync();
        }
        public virtual async Task<T> GetById(int id)
        {

            var result = await DbContext.Set<T>().FindAsync(id);
            if(result == null){
                throw new NotFoundException($"{typeof(T)} Not Found");
            }
            return result;
        }
        public async virtual Task<T> Insert(T entity, int requesterId)
        {
            return await InsertEntity(entity, DbContext);
        }

        public virtual Task<IEnumerable<T>> GetByFilter(string filter)
        {
            throw new System.NotImplementedException();
        }

        public virtual Task<T> Update(T entity, int requesterId)
        {
            throw new System.NotImplementedException();
        }
        public static async Task<T> InsertEntity(T entity, PortalDbContext dbContext)
        {
            var now = DateTime.Now;
            entity.DateCreated = now;
            entity.DateUpdated = now;
            var savedEntity = (await dbContext.Set<T>().AddAsync(entity)).Entity;
            await dbContext.SaveChangesAsync();
            return savedEntity;
        }

        public static async Task<T> UpdateEntity(T entity, PortalDbContext dbContext)
        {
            entity.DateUpdated = DateTime.Now;
            await dbContext.SaveChangesAsync();
            return entity;
        }
    }
}