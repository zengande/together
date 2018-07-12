using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
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

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<IdentityDbContext>
    {
        public IdentityDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<IdentityDbContext>()
               .UseSqlServer("Data Source=.;Initial Catalog=Together.IdentityDb;Persist Security Info=True;User ID=sa;Password=pwd123");
            return new IdentityDbContext(optionsBuilder.Options);
        }
    }
}
