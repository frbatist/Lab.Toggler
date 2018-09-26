using Lab.Toggler.Domain.Interface.Notifications;
using Lab.Toggler.Domain.Service;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Lab.Toggler.Infra.Notifications
{
    public class ErrorNotificationManager : IErrorNotificationsManager
    {
        private readonly IErrorNotificationHandler _errorNotificationHandler;

        public ErrorNotificationManager(INotificationHandler<ErrorNotification> errorNotificationHandler)
        {
            _errorNotificationHandler = (IErrorNotificationHandler)errorNotificationHandler;
        }

        public List<ErrorNotification> GetNotifications()
        {
            return _errorNotificationHandler.GetNotifications();
        }

        public Task Handle(ErrorNotification notification, CancellationToken cancellationToken)
        {
            return _errorNotificationHandler.Handle(notification, cancellationToken);
        }

        public bool HasNotifications()
        {
            return _errorNotificationHandler.HasNotifications();
        }
    }
}
