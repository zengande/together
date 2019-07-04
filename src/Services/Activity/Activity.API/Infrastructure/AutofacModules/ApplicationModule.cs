using Autofac;
using Consul;
using Microsoft.Extensions.Options;
using System;
using Together.Activity.API.Applications.Queries;
using Together.Activity.Domain.AggregatesModel.ActivityAggregate;
using Together.Activity.Domain.AggregatesModel.CategoryAggregate;
using Together.Activity.Infrastructure.Idempotency;
using Together.Activity.Infrastructure.Repositories;

namespace Together.Activity.API.Infrastructure.AutofacModules
{
    public class ApplicationModule
        : Autofac.Module
    {
        public string QueriesConnectionString { get; }
        private readonly IOptions<ServiceDiscoveryOptions> _serviceOptions;
        public ApplicationModule(string qconstr, IOptions<ServiceDiscoveryOptions> serviceOptions)
        {
            QueriesConnectionString = qconstr;
            _serviceOptions = serviceOptions;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new ActivityQueries(QueriesConnectionString))
                .As<IActivityQueries>()
                .InstancePerLifetimeScope();

            builder.Register(c => new CategoryQueries(QueriesConnectionString))
                .As<ICategoryQueries>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ActivityRepository>()
                .As<IActivityRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<CategoryRepository>()
                .As<ICategoryRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<RequestManager>()
               .As<IRequestManager>()
               .InstancePerLifetimeScope();

            //builder.RegisterAssemblyTypes(typeof(CreateActivityCommandHandler).GetTypeInfo().Assembly)
            //    .AsClosedTypesOf(typeof(IIntegrationEventHandler<>));

            builder.RegisterType<ConsulClient>()
                .As<IConsulClient>()
                .WithParameter("configOverride", new Action<ConsulClientConfiguration>(cfg =>
                {
                    var serviceConfiguration = _serviceOptions.Value;
                    if (!string.IsNullOrEmpty(serviceConfiguration.ConsulHttpEndpoint))
                    {
                        // if not configured, the client will use the default value "127.0.0.1:8500"
                        cfg.Address = new Uri(serviceConfiguration.ConsulHttpEndpoint);
                    }
                }))
                .SingleInstance();

        }
    }
}
