using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Web;
using WebMVC.Infrastructure.Dtos;
using WebMVC.Services;
using WebMVC.ViewModels;
using WebMVC.ViewModels.Activity;

namespace WebMVC.Controllers
{
    [Authorize]
    public class ActivityController
        : BaseController
    {
        private readonly IActivityService _activityService;
        private readonly IMapper _mapper;

        public ActivityController(IActivityService activityService,
            IMapper mapper)
        {
            _activityService = activityService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var activity = await _activityService.GetActivityAsync(id);
            if (activity == null)
            {
                return NotFound();
            }
            return View(activity);
        }

        [AllowAnonymous]
        public IActionResult Participant(int id)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new ActivityCreateViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ActivityCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.Details = HttpUtility.HtmlEncode(model.Details).Replace("\n", "<br/>");
                var dto = _mapper.Map<ActivityCreateDto>(model);
                var result = await _activityService.CreateActivity(dto);

                if (result)
                {
                    return LocalRedirect("/");
                }
            }
            return View(model);
        }

        [HttpPut]
        public async Task<IActionResult> Join(int activityId)
        {
            var result = await _activityService.Join(activityId);
            return result ? Ok() : (IActionResult)BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> Search(string keyword)
        {
            var model = new ViewModels.Activity.ActivitySearchResultViewModel
            {
                Keyword = keyword
            };

            model.Results = await _activityService.SearchAsync(keyword);
            return View(model);
        }
    }
}
