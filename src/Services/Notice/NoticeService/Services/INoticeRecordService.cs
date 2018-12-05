using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Together.Notice.Models;

namespace Together.Notice.Services
{
    public interface INoticeRecordService
    {
        Task RecordAsync(IList<NoticeRecord> recordset);
    }
}
