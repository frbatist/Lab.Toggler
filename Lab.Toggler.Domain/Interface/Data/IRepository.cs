using Lab.Toggler.Domain.Interface.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Toggler.Domain.Interface.Data
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        void Add(TEntity entity);        
        Task<TEntity> GetAsync<TId>(TId id);
        IQueryable<TEntity> GetAll();
    }
}
