using Lab.Toggler.Domain.Interface.Data;
using Lab.Toggler.Domain.Interface.Data.Repository;
using Lab.Toggler.Domain.Interface.Notifications;
using Lab.Toggler.Domain.Service;
using Lab.Toggler.Infra.Data;
using Lab.Toggler.Infra.Data.Repository;
using Lab.Toggler.Infra.Notifications;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Lab.Toggler
{
    public static class Bootstrapper
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            //Infra
            services.AddScoped<DbContext, TogglerContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<INotificationHandler<ErrorNotification>, ErrorNotificationHandler>();
            services.AddScoped<IErrorNotificationsManager, ErrorNotificationManager>();

            //repository
            services.AddScoped<IFeatureRepository, FeatureRepository>();
            services.AddScoped<IApplicationRepository, ApplicationRepository>();
            services.AddScoped<IApplicationFeatureRepository, ApplicationFeatureRepository>();  

            //domain services
            services.AddScoped<IFeatureDomainService, FeatureDomainService>();
            services.AddScoped<IApplicationDomainService, ApplicationDomainService>();
            services.AddScoped<IApplicationFeatureDomainService, ApplicationFeatureDomainService>();

            return services;
        }
    }
}
