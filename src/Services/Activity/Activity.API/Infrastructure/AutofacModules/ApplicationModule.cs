using Autofac;
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
        public ApplicationModule(string qconstr)
        {
            QueriesConnectionString = qconstr;
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

        }
    }
}
