using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Together.Topic.API.Models;

namespace Together.Topic.API.Data
{
    public class TopicDbContext
        : DbContext
    {
        public DbSet<Category> Categories { get; set; }

        public TopicDbContext(DbContextOptions options)
            : base(options)
        {

        }
    }
}
