using Autofac;
using Consul;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Together.Activity.API.Applications.Commands;
using Together.Activity.API.Applications.Queries;
using Together.Activity.Domain.AggregatesModel.ActivityAggregate;
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

            builder.RegisterType<ActivityRepository>()
                .As<IActivityRepository>()
                .InstancePerLifetimeScope();

            //builder.RegisterAssemblyTypes(typeof(CreateActivityCommandHandler).GetTypeInfo().Assembly)
            //    .AsClosedTypesOf(typeof(IIntegrationEventHandler<>));

            builder.RegisterType<ConsulClient>()
                .As<IConsulClient>()
                .WithParameter("configOverride", new Action<ConsulClientConfiguration>(cfg =>
                {
                    var serviceConfiguration = _serviceOptions.Value;
                    if (!string.IsNullOrEmpty(serviceConfiguration.Consul.HttpEndpoint))
                    {
                        // if not configured, the client will use the default value "127.0.0.1:8500"
                        cfg.Address = new Uri(serviceConfiguration.Consul.HttpEndpoint);
                    }
                }))
                .SingleInstance();

        }
    }
}
