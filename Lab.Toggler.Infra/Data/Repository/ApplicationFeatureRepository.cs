using Lab.Toggler.Domain.Entities;
using Lab.Toggler.Domain.Interface.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace Lab.Toggler.Infra.Data.Repository
{
    public class ApplicationFeatureRepository : EntityFrameworkRepository<ApplicationFeature>, IApplicationFeatureRepository
    {
        public ApplicationFeatureRepository(DbContext context) : base(context)
        {
        }
    }
}
