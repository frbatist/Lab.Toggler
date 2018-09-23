using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lab.Toggler.Domain.Service
{
    public class ErrorNotification : INotification
    {
        public string Error { get; set; }

        public ErrorNotification(string error)
        {
            Error = error;
        }
    }
}
