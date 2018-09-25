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

        public ApplicationFeatureDomainService(IMediator mediator, IApplicationFeatureRepository applicationFeatureRepository) : base(mediator)
        {
            _applicationFeatureRepository = applicationFeatureRepository;
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
    }
}
