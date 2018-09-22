using Lab.Toggler.Domain.Interface.Data;
using Lab.Toggler.Domain.Interface.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Toggler.Infra.Data
{
    public class EntityFrameworkRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        private DbContext _context;

        public void Add(TEntity entity)
        {
            _context.Add(entity);
        }

        public IQueryable<TEntity> GetAll()
        {
            return _context.Set<TEntity>();
        }

        public Task<TEntity> GetAsync<TId>(TId id)
        {
            return _context.FindAsync<TEntity>(id);
        }
    }
}
