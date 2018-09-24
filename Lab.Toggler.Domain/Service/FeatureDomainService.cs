using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Lab.Toggler.Domain.DTO;
using Lab.Toggler.Domain.Entities;
using Lab.Toggler.Domain.Interface.Data.Repository;
using Lab.Toggler.Domain.Resources;
using MediatR;

namespace Lab.Toggler.Domain.Service
{
    public class FeatureDomainService : ServiceBase, IFeatureDomainService
    {
        private readonly IFeatureRepository _featureRepository;

        public FeatureDomainService(IMediator mediator, IFeatureRepository featureRepository) : base(mediator)
        {
            _featureRepository = featureRepository;
        }

        public async Task<Feature> AddFeature(FeatureDTO featureDTO)
        {
            if (!featureDTO.IsValid())
            {
                return null;
            }

            var existingFeature = await _featureRepository.GetByName(featureDTO.Name);
            if (existingFeature != null)
                return UpdateFeature(existingFeature, featureDTO);

            var feature = new Feature(featureDTO.Name, featureDTO.IsActive);
            _featureRepository.Add(feature);
            return feature;
        }

        private static Feature UpdateFeature(Feature existingFeature, FeatureDTO featureDTO)
        {
            if(featureDTO.IsActive == existingFeature.IsActive)
                return existingFeature;
            if (featureDTO.IsActive)
                existingFeature.Enable();
            else
                existingFeature.Disable();
            return existingFeature;
        }

        public async Task TogleFeature(FeatureDTO featureDTO)
        {
            if (!featureDTO.IsValid())
            {
                return;
            }

            var existingFeature = await _featureRepository.GetByName(featureDTO.Name);
            if (existingFeature != null)
                UpdateFeature(existingFeature, featureDTO);
            else
                await NotifyError(DomainMessageError.NonExistentFeature);
        }
    }
}
;