using Lab.Toggler.Domain.Entities;
using Lab.Toggler.Domain.Interface.Data.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Lab.Toggler.Infra.Data.Repository
{
    public class ApplicationRepository : EntityFrameworkRepository<Application>, IApplicationRepository
    {
        public ApplicationRepository(DbContext context) : base(context)
        {
        }

        public Task<Application> GetAsync(string name, string version)
        {
            var query = GetAll().Where(d => d.Name.Equals(name) && d.Version.Equals(version));
            return query.FirstOrDefaultAsync();
        }
    }
}
