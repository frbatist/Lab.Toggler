using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Lab.Toggler.Domain.Service
{
    public class FeatureDomainService : ServiceBase
    {
        public FeatureDomainService(IMediator mediator) : base(mediator)
        {
        }

        //public async Task AddFeature
    }
}
