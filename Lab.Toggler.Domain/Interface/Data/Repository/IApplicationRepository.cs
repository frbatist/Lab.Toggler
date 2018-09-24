using Lab.Toggler.Domain.Entities;
using System.Threading.Tasks;

namespace Lab.Toggler.Domain.Interface.Data.Repository
{
    /// <summary>
    /// Application repository
    /// </summary>
    public interface IApplicationRepository : IRepository<Application>
    {
        /// <summary>
        /// Get application by name and version
        /// </summary>
        /// <param name="name"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        Task<Application> GetAsync(string name, string version);
    }
}
