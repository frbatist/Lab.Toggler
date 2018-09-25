using Lab.Toggler.Domain.Interface.Data;
using Lab.Toggler.Domain.Interface.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Toggler.Infra.Data
{
    public class EntityFrameworkRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        private DbContext _context;

        public EntityFrameworkRepository(DbContext context)
        {
            _context = context;
        }

        public void Add(TEntity entity)
        {
            _context.Add(entity);
        }

        public IQueryable<TEntity> GetAll()
        {
            return _context.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] include)
        {
            IQueryable<TEntity> query = Include(include);
            return query;
        }

        private IQueryable<TEntity> Include(Expression<Func<TEntity, object>>[] include)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            foreach (var item in include)
            {
                query = query.Include(item);
            }

            return query;
        }

        public Task<TEntity> GetAsync<TId>(TId id)
        {
            return _context.FindAsync<TEntity>(id);
        }
    }
}


