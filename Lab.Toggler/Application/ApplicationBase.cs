using Lab.Toggler.Domain.Interface.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab.Toggler.Application
{
    public class ApplicationBase : IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;

        public ApplicationBase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task CommitAsync()
        {
            return _unitOfWork.CommitAsync();
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
