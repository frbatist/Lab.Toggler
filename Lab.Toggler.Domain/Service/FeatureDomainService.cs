using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Lab.Toggler.Domain.Service
{
    public class FeatureDomainService : ServiceBase
    {
        public FeatureDomainService(IMediator mediator) : base(mediator)
        {
        }


    }
}
