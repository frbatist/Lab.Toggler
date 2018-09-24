using System.Threading.Tasks;
using Lab.Toggler.Domain.DTO;
using Lab.Toggler.Domain.Entities;

namespace Lab.Toggler.Domain.Service
{
    public interface IFeatureDomainService
    {
        Task<Feature> AddFeature(FeatureDTO featureDTO);
        Task TogleFeature(FeatureDTO featureDTO);
    }
}