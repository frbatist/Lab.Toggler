using Lab.Toggler.Domain.DTO;
using Lab.Toggler.Domain.Interface.Data;
using Lab.Toggler.Domain.Interface.Data.Repository;
using Lab.Toggler.Domain.Interface.MessageBus;
using Lab.Toggler.Domain.Service;
using Lab.Toggler.Model;
using System.Threading.Tasks;

namespace Lab.Toggler.ApplicationService
{
    public class FeatureAppService : ApplicationBase, IFeatureAppService
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly IFeatureRepository _featureRepositoriy;
        private readonly IFeatureDomainService _featureDomainService;
        private readonly IApplicationFeatureDomainService _applicationFeatureDomainService;
        private readonly IMessageBus _messageBus;
        public FeatureAppService(
            IUnitOfWork unitOfWork, 
            IApplicationRepository applicationRepository,
            IFeatureRepository featureRepository, 
            IFeatureDomainService featureDomainService,
            IApplicationFeatureDomainService applicationFeatureDomainService,
            IMessageBus messageBus) : base(unitOfWork)
        {
            _applicationRepository = applicationRepository;
            _featureRepositoriy = featureRepository;
            _featureDomainService = featureDomainService;
            _applicationFeatureDomainService = applicationFeatureDomainService;
            _messageBus = messageBus;
        }

        public async Task<FeatureModelResponse> Add(FeatureModel featureModel)
        {
            var newFeature = await _featureDomainService.AddFeature(new FeatureDTO(featureModel.Name, featureModel.IsActive));
            if (newFeature == null)
                return null;
            await CommitAsync();
            await _messageBus.Publish(featureModel);
            return new FeatureModelResponse(newFeature.Id, newFeature.Name, newFeature.IsActive);
        }

        public async Task ToggleFeature(FeatureModel featureModel)
        {
            await _featureDomainService.TogleFeature(new FeatureDTO(featureModel.Name, featureModel.IsActive));
            await CommitAsync();
        }

        public async Task ToggleApplicationFeature(ApplicationFeatureModel applicationFeatureModel)
        {
            var application = await _applicationRepository.GetAsync(applicationFeatureModel.ApplicationName, applicationFeatureModel.ApplicationVersion);
            var feature = await _featureRepositoriy.GetByName(applicationFeatureModel.FeatureName);
            await _applicationFeatureDomainService.TogleApplicationFeature(new ApplicationFeatureDTO(feature, application, applicationFeatureModel.IsActive));
            await CommitAsync();
        }

        public async Task<ApplicationFeatureResponseModel> AddApplicationFeature(ApplicationFeatureModel applicationFeatureModel)
        {
            var application = await _applicationRepository.GetAsync(applicationFeatureModel.ApplicationName, applicationFeatureModel.ApplicationVersion);
            var feature = await _featureRepositoriy.GetByName(applicationFeatureModel.FeatureName);
            var newApplicationFeature = await _applicationFeatureDomainService.Add(new ApplicationFeatureDTO(feature, application, applicationFeatureModel.IsActive));
            if (newApplicationFeature == null)
                return null;

            await CommitAsync();
            return new ApplicationFeatureResponseModel(newApplicationFeature.Id, application.Name, application.Version, feature.Name, newApplicationFeature.IsActive);
        }

        public async Task<FeatureCheckModelResponse> Check(string application, string version, string featureName)
        {
            var check = await _applicationFeatureDomainService.CheckFeature(application, version, featureName);
            if (check == null)
                return null;
            return new FeatureCheckModelResponse
            {
                Enabled = check.Enabled,
                Mesage = check.Mesage
            };
        }
    }
}
