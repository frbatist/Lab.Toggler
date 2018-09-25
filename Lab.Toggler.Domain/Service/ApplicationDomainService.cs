using System.Threading.Tasks;
using Lab.Toggler.Domain.DTO;
using Lab.Toggler.Domain.Entities;
using Lab.Toggler.Domain.Interface.Data.Repository;
using Lab.Toggler.Domain.Resources;
using MediatR;

namespace Lab.Toggler.Domain.Service
{
    public class ApplicationDomainService : ServiceBase, IApplicationDomainService
    {
        private readonly IApplicationRepository _applicationRepository;

        public ApplicationDomainService(IMediator mediator, IApplicationRepository applicationRepository) : base(mediator)
        {
            _applicationRepository = applicationRepository;
        }

        public async Task<Application> Add(ApplicationDTO dto)
        {
            if (!dto.IsValid())
            {
                return null;
            }

            var existingApplication = await _applicationRepository.GetAsync(dto.Name, dto.Version);
            if (existingApplication != null)
            {
                await NotifyError(DomainMessageError.ApplicationAlreadyExists);
                return null;
            }

            var application = new Application(dto.Name, dto.Version);
            _applicationRepository.Add(application);
            return application;
        }
    }
}
