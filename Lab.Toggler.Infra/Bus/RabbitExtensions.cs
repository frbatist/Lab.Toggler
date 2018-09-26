using Microsoft.Extensions.DependencyInjection;
using RawRabbit.vNext;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lab.Toggler.Infra.Bus
{
    public static class RabbitExtensions
    {
        /// <summary>
        /// Configure the service to send messages to rabbitMq
        /// </summary>
        /// <param name="services"></param>        
        /// <param name="serviceId"></param>        
        public static IServiceCollection AddRabbitMQ(this IServiceCollection services)
        {
            services.AddRawRabbit();
            return services;
        }
    }
}
