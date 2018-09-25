using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab.Toggler.Domain.Interface.Data;

namespace Lab.Toggler.ApplicationService
{
    public class FeatureAppService : ApplicationBase
    {

        public FeatureAppService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
