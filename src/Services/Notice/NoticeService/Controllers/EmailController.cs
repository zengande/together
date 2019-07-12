using DotNetCore.CAP;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using Together.Notice.Services;
using Together.Notice.ViewModels;

namespace NoticeService.Controllers
{
    [Route("api/[controller]")]
    public class EmailController : Controller
    {
        private readonly ICapPublisher _publisher;
        private readonly IEmailSender _sender;
        public EmailController(ICapPublisher publisher,
            IEmailSender sender)
        {
            _sender = sender;
            _publisher = publisher;
        }

        [HttpPost]
        [Route("send")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Send([FromBody]MialboxViewModel vm)
        {
            if (ModelState.IsValid)
            {
                await _sender.Send(vm.Tos, vm.Title, vm.Content);
                return Ok();
            }
            return BadRequest();
        }
    }
}