using Autofac;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Together.Activity.API.Applications.Commands;

namespace Together.Activity.API.Infrastructure.AutofacModules
{
    public class MediatorModule
        : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
                 .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(typeof(CreateActivityCommand).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler <,>));

        }
    }
}
