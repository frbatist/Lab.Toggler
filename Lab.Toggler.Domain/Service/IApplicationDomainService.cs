using System.Threading.Tasks;
using Lab.Toggler.Domain.DTO;
using Lab.Toggler.Domain.Entities;

namespace Lab.Toggler.Domain.Service
{
    public interface IApplicationDomainService
    {
        Task<Application> Add(ApplicationDTO dto);
    }
}