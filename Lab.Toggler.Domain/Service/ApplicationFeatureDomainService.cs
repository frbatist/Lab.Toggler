using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Lab.Toggler.Domain.Service
{
    public class ApplicationFeatureDomainService : ServiceBase
    {
        public ApplicationFeatureDomainService(IMediator mediator) : base(mediator)
        {
        }
    }
}
