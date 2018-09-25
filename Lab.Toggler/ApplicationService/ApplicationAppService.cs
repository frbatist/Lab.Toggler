using Lab.Toggler.Domain.DTO;
using Lab.Toggler.Domain.Interface.Data;
using Lab.Toggler.Domain.Service;
using Lab.Toggler.Model;
using System.Threading.Tasks;

namespace Lab.Toggler.ApplicationService
{
    public class ApplicationAppService : ApplicationBase
    {
        private readonly IApplicationDomainService _applicationDomainService;

        public ApplicationAppService(IUnitOfWork unitOfWork, IApplicationDomainService applicationDomainService) : base(unitOfWork)
        {
            _applicationDomainService = applicationDomainService;
        }

        public async Task<ApplicationResponseModel> Add(ApplicationModel model)
        {
            var dto = new ApplicationDTO(model.Name, model.Version);
            var application = await _applicationDomainService.Add(dto);
            if (application == null)
                return null;
            return new ApplicationResponseModel
            {
                Id = application.Id,
                Name = application.Name,
                Version = application.Version
            };
        }
    }
}
