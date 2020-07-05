using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Together.BuildingBlocks.EntityFrameworkCore;

namespace Messaging.Infrastructure.EntityFrameworkCore
{
    public class MessagingDbContext : DbContextBase
    {
        public MessagingDbContext(DbContextOptions options, IMediator mediator) 
            : base(options, mediator)
        {
        }
    }

    public class MessagingDbContextDesignFactory : DbContextDesignFactoryBase<MessagingDbContext>
    {
        public override MessagingDbContext CreateDbContext(string[] args)
        {
            var configuration = BuildConfiguration();

            var optionsBuilder = new DbContextOptionsBuilder<MessagingDbContext>()
                   .UseMySql(configuration.GetConnectionString("Default"));

            return new MessagingDbContext(optionsBuilder.Options, new NoMediator());
        }
    }
}
