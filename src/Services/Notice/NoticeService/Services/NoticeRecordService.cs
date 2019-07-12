using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using Together.Notice.Models;

namespace Together.Notice.Services
{
    public class NoticeRecordService
        : INoticeRecordService
    {
        private readonly ApplicationDbContext _dbContext;
        public NoticeRecordService(IConfiguration configuration)
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));

            _dbContext = new ApplicationDbContext(builder.Options);
        }

        public async Task RecordAsync(IList<NoticeRecord> recordset)
        {
            if (recordset == null)
            {
                return;
            }
            _dbContext.NoticeRecords.AddRange(recordset);

            await _dbContext.SaveChangesAsync();
        }
    }
}
