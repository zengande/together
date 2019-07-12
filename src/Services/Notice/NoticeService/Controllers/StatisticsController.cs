using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Together.Notice.Services;

namespace NoticeService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsService _statisticsService;
        public StatisticsController(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }
        [Route("overview")]
        [HttpGet]
        public async Task<IActionResult> GetOverview()
        {
            //var data = new
            //{
            //    total = 362,
            //    success = 280,
            //    failure = 82,
            //    category = new[] {
            //        "2018/08/26",
            //        "2018/08/27",
            //        "2018/08/28",
            //        "2018/08/29",
            //        "2018/08/30",
            //        "2018/08/31",
            //        "2018/09/01"
            //    },
            //    series = new[] {
            //        new { name="要求", data=new[]{ 100,45,56,78,12,15,56 } },
            //        new { name="成功", data=new[]{ 50,30,45,77,12,10,56 } },
            //        new { name="失败", data=new[]{ 50,15,11,1,0,5,0 } }
            //    }
            //};
            var result = await _statisticsService.GetOverviewData(30);
            return Ok(result);
        }
    }
}