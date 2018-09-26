using Lab.Toggler.Domain.Interface.Notifications;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Lab.Toggler.Controllers
{
    public class ApiControllerBase : ControllerBase
    {
        private readonly IErrorNotificationsManager _errorNotificationsManager;

        public ApiControllerBase(IErrorNotificationsManager errorNotificationsManager)
        {
            _errorNotificationsManager = errorNotificationsManager;
        }

        protected bool IsValidOperation()
        {
            return (!_errorNotificationsManager.HasNotifications());
        }

        protected new IActionResult Response(object result = null)
        {
            if (IsValidOperation())
            {
                return Ok(result);
            }

            return BadRequest(_errorNotificationsManager.GetNotifications().Select(n => n.Error));
        }
    }
}
