using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Models;
using WebMVC.Services;
using WebMVC.ViewModels.Activity;
using WebMVC.ViewModels.Home;

namespace WebMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IActivityService _activityService;
        public HomeController(IActivityService activityService)
        {
            _activityService = activityService;
        }

        public async Task<IActionResult> Index()
        {
            var model = new IndexViewModel();
            var activities = (await _activityService.GetActivitiesNearbyAsync(null))?.ToList() ??
                new List<ActivitySummaryViewModel>();
            if (activities.Count() < 10)
            {
                activities.AddRange(MockActivities(10 - activities.Count()));
            }
            model.ActivitySummaries = activities;
            return View(model);
        }

        private IEnumerable<ActivitySummaryViewModel> MockActivities(int count)
        {
            if (count >= 1)
            {
                for (var i = 0; i < count; i++)
                {
                    yield return new ActivitySummaryViewModel
                    {
                        Title = "数据不够，模拟来凑",
                        Id = 0
                    };
                }
            }
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }
    }
}
