using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Together.BuildingBlocks.Infrastructure.Logging;
using Together.BuildingBlocks.Infrastructure.Validation;

namespace Together.BuildingBlocks.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCustomMediatR(this IServiceCollection services, Type markedType)
        {
            services.AddMediatR(markedType)
                .AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>))
                .AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

            return services;
        }
    }
}
