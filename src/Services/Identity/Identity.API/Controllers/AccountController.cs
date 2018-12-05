using DotNetCore.CAP;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
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
        private readonly ILogger<AccountController> _logger;
        private readonly IConfiguration _configuration;
        public AccountController(IIdentityServerInteractionService interaction,
            InMemoryClientStore clientStore,
            IUserService userService,
            UserManager<ApplicationUser> userManager,
            ICapPublisher publisher,
            IdentityDbContext context,
            ILogger<AccountController> logger,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration)
        {
            _context = context;
            _userService = userService;
            _clientStore = clientStore;
            _interaction = interaction;
            _userManager = userManager;
            _publisher = publisher;
            _signInManager = signInManager;
            _logger = logger;
            _configuration = configuration;
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
                        return RedirectToAction(nameof(ConfirmEmail));
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

                    await _userService.SignInAsync(user, props);

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

        /// <summary>
        /// Show logout page
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            var vm = new LogoutViewModel { LogoutId = logoutId };
            if (User.Identity.IsAuthenticated == false)
            {
                // if the user is not authenticated, then just show logged out page
                return await Logout(vm);
            }

            // show the logout prompt. this prevents attacks where the user
            // is automatically signed out by another malicious web page.

            return View(vm);
        }

        /// <summary>
        /// Handle logout page postback
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(LogoutViewModel model)
        {
            var idp = User?.FindFirst(JwtClaimTypes.IdentityProvider)?.Value;

            if (idp != null && idp != IdentityServerConstants.LocalIdentityProvider)
            {
                if (model.LogoutId == null)
                {
                    // if there's no current logout context, we need to create one
                    // this captures necessary info from the current logged in user
                    // before we signout and redirect away to the external IdP for signout
                    model.LogoutId = await _interaction.CreateLogoutContextAsync();
                }

                string url = "/Account/Logout?logoutId=" + model.LogoutId;

                try
                {

                    // hack: try/catch to handle social providers that throw
                    await HttpContext.SignOutAsync(idp, new AuthenticationProperties
                    {
                        RedirectUri = url
                    });
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex.Message);
                }
            }

            // delete authentication cookie
            await _userService.SignOutAsync();

            // set this so UI rendering sees an anonymous user
            HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity());

            // get context information (client name, post logout redirect URI and iframe for federated signout)
            var logout = await _interaction.GetLogoutContextAsync(model.LogoutId);

            return Redirect(logout?.PostLogoutRedirectUri ?? "/");
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
                    Avatar = _configuration.GetValue("OriginalAvatar", string.Empty)
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
                        await SendConfirmEmaileAddressNotice(user, returnUrl);

                        // 创建账户成功，发送消息给用户服务初始化用户信息
                        await AccountCreatedEvent(new AccountCreatedIntegrationEvent
                        {
                            Id = user.Id,
                            Nickname = user.Nickname,
                            Email = user.Email
                        });
                        trans.Commit();

                        return RedirectToAction(nameof(ConfirmEmail));
                    }
                }
            }
            return View(model);
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
        public ActionResult ConfirmEmail()
        {
            return View();
        }

        /// <summary>
        /// 确认邮箱地址
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Verification(string userId, string code, string returnUrl = "")
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
            if (result.Succeeded)
            {
                await _userService.SignInAsync(user, null);
                if (_interaction.IsValidReturnUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                return Redirect("/");
            }
            return View("Error");
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
            var callbackUrl = Url.Action(nameof(Verification), "Account",
                new { userId = user.Id, code, returnUrl }, protocol: HttpContext.Request.Scheme);

            await _publisher.PublishAsync("Together.Notice.Email.Confirm.EmailAddress", new VerifyAccountEmailNoticeEvent { To = user.Email, Link = callbackUrl });
        }
    }
}