using Lab.Toggler.Domain.Interface.MessageBus;
using RawRabbit;
using RawRabbit.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Toggler.Infra.Bus
{
    public class RabbitMqMessageBus : IMessageBus
    {
        private readonly IBusClient _bus;

        public RabbitMqMessageBus(IBusClient bus)
        {
            _bus = bus;
        }

        public async Task Publish<T>(T message) where T:class
        {
            await _bus.PublishAsync(message);
        }
    }
}
