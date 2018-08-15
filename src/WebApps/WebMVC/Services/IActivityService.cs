using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Services
{
    public interface IActivityService
    {
        Task<string> GetActivities(int pageIndex = 1, int pageSize = 20);
    }
}
