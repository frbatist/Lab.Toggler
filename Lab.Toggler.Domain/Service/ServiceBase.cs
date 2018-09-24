using Lab.Toggler.Domain.DTO;
using MediatR;
using System.Threading.Tasks;

namespace Lab.Toggler.Domain.Service
{
    public class ServiceBase
    {
        protected readonly IMediator _mediator;

        public ServiceBase(IMediator mediator)
        {
            _mediator = mediator;
        }

        protected async Task NotifyValidationErrors(DtoBase message)
        {
            foreach (var error in message.ValidationResult.Errors)
            {
                await _mediator.Publish(new ErrorNotification(error.ErrorMessage));
            }
        }

        protected async Task NotifyError(string error)
        {
            await _mediator.Publish(new ErrorNotification(error));
        }
    }
}
