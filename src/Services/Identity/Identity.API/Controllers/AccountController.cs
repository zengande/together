using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using DotNetCore.CAP;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Together.Identity.API.Data;
using Together.Identity.API.IntegrationEvents;
using Together.Identity.API.Models;
using Together.Identity.API.Models.AccountViewModels;
using Together.Identity.API.Services;

namespace Together.Identity.API.Controllers
{
    public class AccountController : Controller
    {
        private readonly IIdentityServerInteractionService _interaction;
        private readonly InMemoryClientStore _clientStore;
        private readonly IUserService _userService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ICapPublisher _publisher;
        private readonly IdentityDbContext _context;
        public AccountController(IIdentityServerInteractionService interaction,
            InMemoryClientStore clientStore,
            IUserService userService,
            UserManager<ApplicationUser> userManager,
            ICapPublisher publisher,
            IdentityDbContext context,
            SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userService = userService;
            _clientStore = clientStore;
            _interaction = interaction;
            _userManager = userManager;
            _publisher = publisher;
            _signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl)
        {
            var context = await _interaction.GetAuthorizationContextAsync(returnUrl);
            if (context?.IdP != null)
            {
                return ExternalLogin(context.IdP, returnUrl);
            }
            var vm = await BuildLoginViewModelAsync(returnUrl, context);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.FindByEmail(model.Email);

                if (await _userService.ValidateCredentials(user, model.Password))
                {
                    if (!await _userManager.IsEmailConfirmedAsync(user))
                    {
                        await SendConfirmEmaileAddressNotice(user, model.ReturnUrl);

                        // ModelState.AddModelError(string.Empty, "You must have a confirmed email to log in.");
                        return RedirectToAction(nameof(Verification));
                    }

                    AuthenticationProperties props = null;
                    if (model.RememberMe)
                    {
                        props = new AuthenticationProperties
                        {
                            IsPersistent = true,
                            ExpiresUtc = DateTimeOffset.UtcNow.AddYears(10)
                        };
                    };

                    await _userService.SignIn(user, props);

                    // make sure the returnUrl is still valid, and if yes - redirect back to authorize endpoint
                    if (_interaction.IsValidReturnUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }


                    return Redirect("~/");
                }

                ModelState.AddModelError("", "用户名或密码错误");
            }

            // something went wrong, show form with error
            var vm = await BuildLoginViewModelAsync(model);

            ViewData["ReturnUrl"] = model.ReturnUrl;

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> SignOut(string returnUrl)
        {
            await _signInManager.SignOutAsync();
            if (_interaction.IsValidReturnUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return Redirect("~/");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    Nickname = model.Nickname,
                    UserName = model.Email,
                    Email = model.Email,
                };
                using (var trans = _context.Database.BeginTransaction())
                {
                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Errors.Count() > 0)
                    {
                        AddErrors(result);
                        // If we got this far, something failed, redisplay form
                        return View(model);
                    }
                    if (result.Succeeded)
                    {
                        // 发送验证邮箱地址的邮件
                        await SendConfirmEmaileAddressNotice(user);

                        // 创建账户成功，发送消息给用户服务初始化用户信息
                        await AccountCreatedEvent(new AccountCreatedIntegrationEvent
                        {
                            Id = user.Id,
                            Nickname = user.Nickname,
                            Email = user.Email
                        });
                        trans.Commit();
                    }
                }
            }

            if (returnUrl != null)
            {
                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    return Redirect(returnUrl);
                }
                else if (ModelState.IsValid)
                {
                    return RedirectToAction("login", "account", new { returnUrl });
                }
                else
                {
                    return View(model);
                }
            }

            return RedirectToAction("index", "home");
        }

        /// <summary>
        /// initiate roundtrip to external authentication provider
        /// </summary>
        [HttpGet]
        public IActionResult ExternalLogin(string provider, string returnUrl)
        {
            if (returnUrl != null)
            {
                returnUrl = UrlEncoder.Default.Encode(returnUrl);
            }
            returnUrl = "/account/externallogincallback?returnUrl=" + returnUrl;

            // start challenge and roundtrip the return URL
            var props = new AuthenticationProperties
            {
                RedirectUri = returnUrl,
                Items = { { "scheme", provider } }
            };
            return new ChallengeResult(provider, props);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Verification()
        {
            return View();
        }

        /// <summary>
        /// 确认邮箱地址
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> ConfirmEmail(string userId, string code, string returnUrl = "")
        {
            if (string.IsNullOrEmpty(userId) ||
                string.IsNullOrEmpty(code))
            {
                return View("Error");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View("Error");
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (string.IsNullOrEmpty(returnUrl))
            {
                returnUrl = "/";
            }
            ViewData["ReturnUrl"] = returnUrl;

            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        private async Task<LoginViewModel> BuildLoginViewModelAsync(string returnUrl, AuthorizationRequest context)
        {
            var allowLocal = true;
            if (context?.ClientId != null)
            {
                var client = await _clientStore.FindEnabledClientByIdAsync(context.ClientId);
                if (client != null)
                {
                    allowLocal = client.EnableLocalLogin;
                }
            }

            return new LoginViewModel
            {
                ReturnUrl = returnUrl,
                Email = context?.LoginHint,
            };
        }
        private async Task<LoginViewModel> BuildLoginViewModelAsync(LoginViewModel model)
        {
            var context = await _interaction.GetAuthorizationContextAsync(model.ReturnUrl);
            var vm = await BuildLoginViewModelAsync(model.ReturnUrl, context);
            vm.Email = model.Email;
            vm.RememberMe = model.RememberMe;
            return vm;
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        /// <summary>
        /// 发布注册用户的消息
        /// </summary>
        /// <param name="user"></param>
        private async Task AccountCreatedEvent(AccountCreatedIntegrationEvent @evnet)
        {
            await _publisher.PublishAsync("Identity.API.AccountCreatedEvent", @evnet);
        }
        private async Task SendConfirmEmaileAddressNotice(ApplicationUser user, string returnUrl = "")
        {
            // 发送确认邮箱的邮件
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.Action(nameof(ConfirmEmail), "Account",
                new { userId = user.Id, code, returnUrl }, protocol: HttpContext.Request.Scheme);

            await _publisher.PublishAsync("Together.Notice.Email.Confirm.EmailAddress", new VerifyAccountEmailNoticeEvent { To = user.Email, Link = callbackUrl });
        }
    }
}