using System.Threading.Tasks;
using Lab.Toggler.Domain.DTO;
using Lab.Toggler.Domain.Entities;
using Lab.Toggler.Domain.Interface.Data.Repository;
using Lab.Toggler.Domain.Resources;
using MediatR;

namespace Lab.Toggler.Domain.Service
{
    public class ApplicationFeatureDomainService : ServiceBase, IApplicationFeatureDomainService
    {
        private readonly IApplicationFeatureRepository _applicationFeatureRepository;
        private readonly IFeatureRepository _featureRepository;

        public ApplicationFeatureDomainService(IMediator mediator, IApplicationFeatureRepository applicationFeatureRepository, IFeatureRepository featureRepository) : base(mediator)
        {
            _applicationFeatureRepository = applicationFeatureRepository;
            _featureRepository = featureRepository;
        }

        public async Task<ApplicationFeature> Add(ApplicationFeatureDTO applicationFeatureDTO)
        {
            if (!applicationFeatureDTO.IsValid())
            {
                return null;
            }

            var existingFeature = await _applicationFeatureRepository.GetAsync(applicationFeatureDTO.Application.Id, applicationFeatureDTO.Feature.Id);
            if (existingFeature != null)
                return UpdateApplicationFeature(existingFeature, applicationFeatureDTO);

            var applicationFeature = new ApplicationFeature(applicationFeatureDTO.Feature, applicationFeatureDTO.Application, applicationFeatureDTO.IsActive);
            _applicationFeatureRepository.Add(applicationFeature);

            return applicationFeature;
        }

        public async Task TogleApplicationFeature(ApplicationFeatureDTO applicationFeatureDTO)
        {
            if (!applicationFeatureDTO.IsValid())
            {
                return;
            }

            var existingFeature = await _applicationFeatureRepository.GetAsync(applicationFeatureDTO.Application.Id, applicationFeatureDTO.Feature.Id);
            if (existingFeature != null)
                UpdateApplicationFeature(existingFeature, applicationFeatureDTO);
            else
                await NotifyError(DomainMessageError.NonExistentFeature);
        }

        private static ApplicationFeature UpdateApplicationFeature(ApplicationFeature existingFeature, ApplicationFeatureDTO applicationFeatureDTO)
        {
            if (applicationFeatureDTO.IsActive == existingFeature.IsActive)
                return existingFeature;
            if (applicationFeatureDTO.IsActive)
                existingFeature.Enable();
            else
                existingFeature.Disable();
            return existingFeature;
        }

        public async Task<FeatureCheckDTO> CheckFeature(string application, string version, string featureName)
        {
            var featureCheck = await CheckApplicationFeature(application, version, featureName);

            if (featureCheck != null)
                return featureCheck;

            return await CheckGlobalFeature(featureName);
        }

        private async Task<FeatureCheckDTO> CheckGlobalFeature(string featureName)
        {
            var feature = await _featureRepository.GetByName(featureName);
            if (feature == null || feature.IsActive)
            {
                return new FeatureCheckDTO
                {
                    Enabled = true,
                    Mesage = DomainMessage.FeatureEnabled
                };
            }
            else
            {
                return new FeatureCheckDTO
                {
                    Enabled = false,
                    Mesage = DomainMessage.FeatureDisabled
                };
            }
        }

        private async Task<FeatureCheckDTO> CheckApplicationFeature(string application, string version, string featureName)
        {
            var applicationFeature = await _applicationFeatureRepository.GetAsync(application, version, featureName);
            if (applicationFeature == null)
            {
                return null;
            }
            else
            {
                if (applicationFeature.IsActive)
                {
                    return new FeatureCheckDTO
                    {
                        Enabled = true,
                        Mesage = DomainMessage.FeatureEnabled
                    };
                }
                else
                {
                    return new FeatureCheckDTO
                    {
                        Enabled = false,
                        Mesage = DomainMessage.FeatureDisabled
                    };
                }
            }
        }
    }
}
