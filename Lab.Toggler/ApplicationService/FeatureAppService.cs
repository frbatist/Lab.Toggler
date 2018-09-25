using Lab.Toggler.Domain.DTO;
using Lab.Toggler.Domain.Interface.Data;
using Lab.Toggler.Domain.Interface.Data.Repository;
using Lab.Toggler.Domain.Service;
using Lab.Toggler.Model;
using System.Threading.Tasks;

namespace Lab.Toggler.ApplicationService
{
    public class FeatureAppService : ApplicationBase
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly IFeatureRepository _featureRepositoriy;
        private readonly IFeatureDomainService _featureDomainService;

        public FeatureAppService(
            IUnitOfWork unitOfWork, 
            IApplicationRepository applicationRepository,
            IFeatureRepository featureRepository, 
            IFeatureDomainService featureDomainService) : base(unitOfWork)
        {
            _applicationRepository = applicationRepository;
            _featureRepositoriy = featureRepository;
            _featureDomainService = featureDomainService;
        }

        public async Task<FeatureModelResponse> Add(FeatureModel featureModel)
        {
            var newFeature = await _featureDomainService.AddFeature(new FeatureDTO(featureModel.Name, featureModel.IsActive));
            if (newFeature == null)
                return null;
            return new FeatureModelResponse(newFeature.Id, newFeature.Name, newFeature.IsActive);
        }
    }
}
