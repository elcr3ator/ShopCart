using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCProject2.Data.Interfaces;
using MVCProject2.Data.Models;
using MVCProject2.Services;
using MVCProject2.ViewModels;
using System.Security.Claims;
namespace MVCProject2.Controllers
{
    public class HomeController : Controller
    {
        private IAllProducts _productRepository;
        private readonly ILogger<HomeController> _logger;
        private readonly UserService _userService;
        private readonly IUsers _users;
        public HomeController(IUsers users, IAllProducts productRepository, ILogger<HomeController> logger, UserService userService)
        {
            _productRepository = productRepository;
            _logger = logger;
            _userService = userService;
            _users = users;
        }

        public ViewResult Index()
        {
            var homeProducts = new HomeViewModel
            {
                favProducts = _productRepository.getFavProducts
            };
            return View(homeProducts);
        }
        [HttpGet("denied")]
        public IActionResult Denied()
        {
            return View();
        }
        public ViewResult ewq3weweq()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Secured()
        {
            var idToken = await HttpContext.GetTokenAsync("id_token");
            return View();
        }
        [HttpGet("login")]
        public IActionResult Login(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpGet("login/{provider}")]
        public IActionResult LoginExternal([FromRoute] string provider, [FromQuery] string returnUrl)
        {
            if (User != null && User.Identities.Any(identity => identity.IsAuthenticated))
            {
                return RedirectToAction("", "Home");
            }

            // By default the client will be redirect back to the URL that issued the challenge (/login?authtype=foo),
            // send them to the home page instead (/).
            returnUrl = string.IsNullOrEmpty(returnUrl) ? "/" : returnUrl;
            var authenticationProperties = new AuthenticationProperties { RedirectUri = returnUrl };
            // authenticationProperties.SetParameter("prompt", "select_account");
            return new ChallengeResult(provider, authenticationProperties);
        }
        [HttpPost]
        public ActionResult RegisterUser(AppUser appUser)
        {
            _users.AddAppUser(appUser);
            return View();
        }


        [Route("validate")]
        public async Task<IActionResult> ValidateAsync(string username, string password, string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (_userService.TryValidateUser(username, password, out List<Claim> claims))
            {
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                var items = new Dictionary<string, string>();
                items.Add(".AuthScheme", CookieAuthenticationDefaults.AuthenticationScheme);
                var properties = new AuthenticationProperties(items);
                await AuthenticationHttpContextExtensions.SignInAsync(HttpContext, claimsPrincipal, properties);

                return Redirect(returnUrl);
            }
            else
            {
                TempData["Error"] = "Error. Username or Password is invalid";
                return View("login");
            }
            
        }
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            var scheme = User.Claims.FirstOrDefault(c => c.Type == ".AuthScheme").Value;
            string domainUrl = HttpContext.Request.Scheme + "://" + HttpContext.Request.Host;
            switch (scheme)
            {
                case "google":
                    await HttpContext.SignOutAsync();
                    var redirect = $"https://www.google.com/accounts/Logout?continue=https://appengine.google.com/_ah/logout?continue={domainUrl}";
                    return Redirect(redirect);
                case "facebook":
                case "Cookies":
                    await HttpContext.SignOutAsync();
                    return Redirect("/");
                case "microsoft":
                    await HttpContext.SignOutAsync();
                    return Redirect("/");
                default:
                    return new SignOutResult(new[] { CookieAuthenticationDefaults.AuthenticationScheme, scheme });
            }
        }
    }
}
