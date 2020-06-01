using Autofac;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Together.BuildingBlocks.Infrastructure.Idempotency;
using Together.BuildingBlocks.Infrastructure.Identity;
using Together.BuildingBlocks.Infrastructure.Logging;
using Together.BuildingBlocks.Infrastructure.Validation;

namespace Together.BuildingBlocks.Infrastructure
{
    public static class DependencyInjection
    {
        public static ContainerBuilder RegisterMediatorModule(this ContainerBuilder builder, Type commandHandlerType, Type validatorType)
        {
            builder.RegisterType<InMemoryRequestManager>()
                .As<IRequestManager>()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();

            // Register all the Command classes (they implement IRequestHandler) in assembly holding the Commands
            builder.RegisterAssemblyTypes(commandHandlerType.GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>));

            // Register the DomainEventHandler classes (they implement INotificationHandler<>) in assembly holding the Domain Events
            //builder.RegisterAssemblyTypes(typeof(ValidateOrAddBuyerAggregateWhenOrderStartedDomainEventHandler).GetTypeInfo().Assembly)
            //    .AsClosedTypesOf(typeof(INotificationHandler<>));

            // Register the Command's Validators (Validators based on FluentValidation library)
            builder
                .RegisterAssemblyTypes(validatorType.GetTypeInfo().Assembly)
                .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
                .AsImplementedInterfaces();

            // request & notification handlers
            builder.Register<ServiceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });

            builder.RegisterGeneric(typeof(LoggingBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(ValidatorBehavior<,>)).As(typeof(IPipelineBehavior<,>));


            return builder;
        }

        public static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
            services.AddTransient<IIdentityService, IdentityService>();

            return services;
        }
    }
}
