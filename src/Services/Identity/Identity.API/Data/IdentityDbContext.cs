using Microsoft.EntityFrameworkCore;
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var entry = builder.Entity<ApplicationUser>();
            entry.Property(a => a.Avatar)
                .HasMaxLength(512);
            entry.Property(a => a.Nickname)
                .IsUnicode()
                .HasMaxLength(100);
            entry.Property(a => a.Gender)
                .HasDefaultValue(Gender.Unknown);

            base.OnModelCreating(builder);
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
