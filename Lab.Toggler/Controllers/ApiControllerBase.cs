using Lab.Toggler.Domain.Service;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Lab.Toggler.Controllers
{
    public class ApiControllerBase : ControllerBase
    {
        private readonly IErrorNotificationHandler _notificationHandler;

        public ApiControllerBase(INotificationHandler<ErrorNotification> notificationHandler)
        {
            _notificationHandler = (IErrorNotificationHandler)notificationHandler;
        }

        protected bool IsValidOperation()
        {
            return (!_notificationHandler.HasNotifications());
        }

        protected new IActionResult Response(object result = null)
        {
            if (IsValidOperation())
            {
                return Ok(result);
            }

            return BadRequest(_notificationHandler.GetNotifications().Select(n => n.Error));
        }
    }
}
