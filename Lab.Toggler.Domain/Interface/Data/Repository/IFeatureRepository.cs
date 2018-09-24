using Lab.Toggler.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Toggler.Domain.Interface.Data.Repository
{
    public interface IFeatureRepository : IRepository<Feature>
    {
        Task<Feature> GetByName(string name);
    }
}
