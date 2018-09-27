using Lab.Toggler.Domain.Interface.MessageBus;
using RawRabbit;
using RawRabbit.Configuration.Publish;
using System;
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

        public async Task Publish<T>(T message, string exchange) where T:class
        {
            string exchangeName = typeof(T).Name;
            if (!string.IsNullOrWhiteSpace(exchange))
            {
                exchange += $"_{exchange}";
            }
            await _bus.PublishAsync(message, Guid.NewGuid(), (IPublishConfigurationBuilder config) => 
            {
                config
                .WithExchange(d => d.WithName(exchangeName))
                .WithRoutingKey("#");
            });
        }
    }
}
