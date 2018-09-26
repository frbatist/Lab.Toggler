using System.Threading.Tasks;
using Lab.Toggler.Model;

namespace Lab.Toggler.ApplicationService
{
    public interface IApplicationAppService
    {
        Task<ApplicationResponseModel> Add(ApplicationModel model);
    }
}