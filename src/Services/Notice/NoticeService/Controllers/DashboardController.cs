using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Together.Notice.Services;

namespace NoticeService.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IEmailSender _sender;
        public DashboardController(IEmailSender sender)
        {
            _sender = sender;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Test(string to)
        {
            await _sender.Send(to, "测试邮件", "这只是一封测试邮件");
            return Ok();
        }
    }
}