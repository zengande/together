using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Together.Notice.ViewModels;

namespace Together.Notice.Services
{
   public interface IStatisticsService
    {
        Task<DashboardViewModel> GetOverviewData(int days = 7);
    }
}
