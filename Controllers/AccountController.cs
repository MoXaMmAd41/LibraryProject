using Library.Models;
using Library.ModelView;
using Library.Resource;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Threading.Tasks;

namespace Library.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IStringLocalizer<SharedResources> _localizer;
        public AccountController(IStringLocalizer<SharedResources> localizer,IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
            _localizer = localizer;
        }


        [HttpGet]
        public IActionResult Login()
        {
            ViewBag.LoginTitle = _localizer["LibraryLogin"];
            ViewBag.Email = _localizer["Email"];
            ViewBag.Password = _localizer["Password"];
            ViewBag.LoginButton = _localizer["Login"];
            ViewBag.CreateAccount = _localizer["CreateAccount"];
            ViewBag.DontHaveAccount = _localizer["Dont Have Account?"];
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LogInViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _accountRepository.GetUserByEmail(model.Email);

                if (user != null)
                {
                    bool isPasswordValid = await _accountRepository.CheckPassword(user, model.Password);

                    if (isPasswordValid)
                    {
                        var result = await _accountRepository.SignInUser(user, model.Password);

                        if (result)
                        {
                            return RedirectToAction("HomePage", "HomePage");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Invalid password.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid password.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "User not found.");
                }
            }

            return View(model);
        }

        
        [HttpGet]
        public IActionResult Register()
        {
            ViewBag.RegisterTitle = _localizer["Register"];
            ViewBag.Email = _localizer["Email"];
            ViewBag.Password = _localizer["Password"];
            ViewBag.Username = _localizer["Username"];
            ViewBag.AlreadyAccount = _localizer["Already Have Account?"];
            ViewBag.LoginLink = _localizer["Login"];
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _accountRepository.GetUserByEmail(model.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError("", "Email or Username is already taken.");
                    return View(model);
                }

                var user = new User
                {
                    UserName = model.Username,
                    Email = model.Email
                };

                var result = await _accountRepository.CreateUserAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return View(model);
        }
        [HttpGet]
        public IActionResult SetLanguage(string culture, string returnUrl = null)
        {
            if (!string.IsNullOrWhiteSpace(culture))
            {
                Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                    new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
                );
            }

            return LocalRedirect(returnUrl ?? "/");
        }
    }
}