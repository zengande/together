using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Together.Identity.API.Models;

namespace Together.Identity.API.Data
{
    public class IdentityDbContext
        : Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext<ApplicationUser>
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
            : base(options)
        {

        }
    }

    //public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<IdentityDbContext>
    //{
    //    private readonly IConfiguration _configuration;
    //    public DesignTimeDbContextFactory(IConfiguration configuration)
    //    {
    //        _configuration = configuration;
    //    }

    //    public IdentityDbContext CreateDbContext(string[] args)
    //    {
    //        var optionsBuilder = new DbContextOptionsBuilder<IdentityDbContext>()
    //            .UseNpgsql(_configuration.GetValue<string>("ConnectionString"));
    //        return new IdentityDbContext(optionsBuilder.Options);
    //    }
    //}
}
