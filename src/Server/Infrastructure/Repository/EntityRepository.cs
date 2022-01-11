using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Interfaces;
using Infrastructure.Persistence;
using System.Data.Entity;
using System;

namespace Infrastructure.Repository
{
    public class EntityRepository<S, T> : IEntityRepository<S, T>
    where S : class where T : class, IEntity
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
        public virtual async Task<T> GetById(string id)
        {
           
        return await DbContext.Set<T>().FindAsync(id);
            
        }
        public async virtual Task<T> Insert(T entity, string requesterId = null)
        {
            return await InsertEntity(entity, DbContext);
        }

        public virtual Task<IEnumerable<T>> GetByFilter(string filter)
        {
            throw new System.NotImplementedException();
        }
        public virtual Task<IEnumerable<S>> GetSummary(string filter)
        {
            throw new System.NotImplementedException();
        }
        public virtual Task<T> Update(S entity)
        {
            throw new System.NotImplementedException();
        }
        public static async Task<T> InsertEntity(T entity, PortalDbContext dbContext){
            var now = DateTime.Now;
            entity.DateCreated = now;
            entity.DateUpdated = now;
            var savedEntity = (await dbContext.Set<T>().AddAsync(entity)).Entity;
            await dbContext.SaveChangesAsync();
            return savedEntity;
        }

        public static async Task<T> UpdateEntity(T entity, PortalDbContext dbContext){
            entity.DateUpdated = DateTime.Now;
            await dbContext.SaveChangesAsync();
            return entity;
        }
    }
}