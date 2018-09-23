using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab.Toggler.Domain.Service
{
    public class ErrorNotificationHandler : INotificationHandler<ErrorNotification>, IErrorNotificationHandler
    {
        private List<ErrorNotification> _notifications;

        public ErrorNotificationHandler()
        {
            _notifications = new List<ErrorNotification>();
        }

        public Task Handle(ErrorNotification notification, CancellationToken cancellationToken)
        {
            _notifications.Add(notification);
            return Task.FromResult<object>(null);
        }

        public virtual List<ErrorNotification> GetNotifications()
        {
            return _notifications;
        }
        public virtual bool HasNotifications()
        {
            return GetNotifications().Any();
        }
    }
}
