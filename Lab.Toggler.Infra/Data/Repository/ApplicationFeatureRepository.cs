using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab.Toggler.Domain.DTO;
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

        public Task<ApplicationFeature> GetAsync(int applicationId, int featureId)
        {
            var query = GetAll().Where(d => d.Application.Id.Equals(applicationId) && d.Feature.Id.Equals(featureId));
            return query.FirstOrDefaultAsync();
        }

        public Task<ApplicationFeature> GetApplicationFeatureAsync(int id)
        {
            var query = GetAll(d => d.Application, f => f.Feature).Where(d => d.Id.Equals(id));
            return query.FirstOrDefaultAsync();
        }

        public Task<ApplicationFeature> GetAsync(string application, string version, string featureName)
        {
            var query = GetAll().Where(d=>d.Application.Name.Equals(application) && d.Application.Version.Equals(version) && d.Feature.Name.Equals(featureName));
            return query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<FeatureDTO>> GetAllAsync(string application, string version)
        {
            var query = GetAll().Where(d => d.Application.Name.Equals(application) && d.Application.Version.Equals(version))
                .Select(e=> new FeatureDTO(e.FeatureId, e.Feature.Name, e.IsActive));
            var values = await query.ToListAsync();
            return values;
        }
    }
}
