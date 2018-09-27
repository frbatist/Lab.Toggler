using Lab.Toggler.Domain.DTO;
using Lab.Toggler.Domain.Entities;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lab.Toggler.Domain.Interface.Data.Repository
{
    public interface IApplicationFeatureRepository : IRepository<ApplicationFeature>
    {
        Task<ApplicationFeature> GetAsync(int applicationId, int featureId);
        Task<ApplicationFeature> GetAsync(string application, string version, string featureName);
        Task<IEnumerable<FeatureDTO>> GetAllAsync(string application, string version);
        Task<ApplicationFeature> GetApplicationFeatureAsync(int id);        
    }
}
