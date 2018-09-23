using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Lab.Toggler.Domain.Service
{
    public interface IErrorNotificationHandler
    {
        List<ErrorNotification> GetNotifications();
        Task Handle(ErrorNotification notification, CancellationToken cancellationToken);
        bool HasNotifications();
    }
}