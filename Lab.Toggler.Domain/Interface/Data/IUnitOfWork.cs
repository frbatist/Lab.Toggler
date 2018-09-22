using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Toggler.Domain.Interface.Data
{
    public interface IUnitOfWork : IDisposable
    {
        Task CommitAsync();
    }
}
