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
        private readonly IApplicationFeatureDomainService _applicationFeatureDomainService;

        public FeatureAppService(
            IUnitOfWork unitOfWork, 
            IApplicationRepository applicationRepository,
            IFeatureRepository featureRepository, 
            IFeatureDomainService featureDomainService,
            IApplicationFeatureDomainService applicationFeatureDomainService) : base(unitOfWork)
        {
            _applicationRepository = applicationRepository;
            _featureRepositoriy = featureRepository;
            _featureDomainService = featureDomainService;
            _applicationFeatureDomainService = applicationFeatureDomainService;
        }

        public async Task<FeatureModelResponse> Add(FeatureModel featureModel)
        {
            var newFeature = await _featureDomainService.AddFeature(new FeatureDTO(featureModel.Name, featureModel.IsActive));
            if (newFeature == null)
                return null;
            return new FeatureModelResponse(newFeature.Id, newFeature.Name, newFeature.IsActive);
        }

        public async Task<ApplicationFeatureResponseModel> AddApplicationFeature(ApplicationFeatureModel applicationFeatureModel)
        {
            var application = await _applicationRepository.GetAsync(applicationFeatureModel.ApplicationName, applicationFeatureModel.ApplicationVersion);
            var feature = await _featureRepositoriy.GetByName(applicationFeatureModel.FeatureName);
            var newApplicationFeature = await _applicationFeatureDomainService.Add(new ApplicationFeatureDTO(feature, application, applicationFeatureModel.IsActive));
            if (newApplicationFeature == null)
                return null;
            return new ApplicationFeatureResponseModel(newApplicationFeature.Id, application.Name, application.Version, feature.Name, newApplicationFeature.IsActive);
        }
    }
}
