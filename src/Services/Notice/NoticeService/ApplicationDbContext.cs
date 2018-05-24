using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Together.Notice.Models;

namespace Together.Notice
{
    public class ApplicationDbContext
        :DbContext
    {
        public DbSet<EmailTemplate> EmailTemplates { get; set; }

        public ApplicationDbContext(DbContextOptions options)
            :base(options)
        {
        }
    }
}
