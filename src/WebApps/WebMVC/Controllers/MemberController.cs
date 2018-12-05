using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebMVC.Controllers
{
    public class MemberController : Controller
    {
        [Authorize]
        public IActionResult Me()
        {
            return View();
        }
    }
}