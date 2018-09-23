using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Lab.Toggler.Domain.DTO;
using Lab.Toggler.Domain.Interface.Data.Repository;
using MediatR;

namespace Lab.Toggler.Domain.Service
{
    public class FeatureDomainService : ServiceBase, IFeatureDomainService
    {
        private readonly IFeatureRepository _featureRepository;

        public FeatureDomainService(IMediator mediator, IFeatureRepository featureRepository) : base(mediator)
        {
            featureRepository = _featureRepository;
        }

        public async Task AddFeature(FeatureDTO featureDTO)
        {

        }

        public async Task TogleFeature(FeatureDTO featureDTO)
        {

        }
    }
}
