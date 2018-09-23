using System.Threading.Tasks;
using Lab.Toggler.Domain.DTO;

namespace Lab.Toggler.Domain.Service
{
    public interface IFeatureDomainService
    {
        Task AddFeature(FeatureDTO featureDTO);
        Task TogleFeature(FeatureDTO featureDTO);
    }
}