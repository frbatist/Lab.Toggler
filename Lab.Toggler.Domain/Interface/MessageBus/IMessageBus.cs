using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Toggler.Domain.Interface.MessageBus
{
    public interface IMessageBus
    {
        Task Publish<T>(T message, string exchange) where T : class;
    }
}
