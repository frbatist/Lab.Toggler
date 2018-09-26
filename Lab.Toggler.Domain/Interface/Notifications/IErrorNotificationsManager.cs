using Lab.Toggler.Domain.Service;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Lab.Toggler.Domain.Interface.Notifications
{
    public interface IErrorNotificationsManager
    {
        List<ErrorNotification> GetNotifications();
        Task Handle(ErrorNotification notification, CancellationToken cancellationToken);
        bool HasNotifications();
    }
}
