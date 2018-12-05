using Microsoft.EntityFrameworkCore;
using Together.Notice.Models;

namespace Together.Notice
{
    public class ApplicationDbContext
        : DbContext
    {
        public DbSet<EmailTemplate> EmailTemplates { get; set; }
        public DbSet<NoticeRecord> NoticeRecords { get; set; }

        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}
