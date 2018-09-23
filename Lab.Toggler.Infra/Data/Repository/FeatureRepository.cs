using Lab.Toggler.Domain.Entities;
using Lab.Toggler.Domain.Interface.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace Lab.Toggler.Infra.Data.Repository
{
    public class FeatureRepository : EntityFrameworkRepository<Feature>, IFeatureRepository
    {
        public FeatureRepository(DbContext context) : base(context)
        {
        }
    }
}
