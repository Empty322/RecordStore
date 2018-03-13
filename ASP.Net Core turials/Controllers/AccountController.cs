using System.Threading.Tasks;
using Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ASP.Net_Core_turials.Controllers
{
	public class AccountController : Controller
	{
		private UserManager<ApplicationUser> userManager;
		private SignInManager<ApplicationUser> signInManager;

		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
		{
			this.signInManager = signInManager;
			this.userManager = userManager;
		}

		public IActionResult Index()
		{
			return View();
		}

		public async Task<IActionResult> Register()
		{
			var result = await userManager.CreateAsync(new ApplicationUser
			{
				UserName = "Empty",
				Email = "myemail@yandex.ru"
			}, "password");

			if(result.Succeeded)
				return Content("Suceeded");
			else
				return Content("Failed");
		}

		public async Task<IActionResult> Login(string returnUrl = "")
		{
			await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);

			var result = await signInManager.PasswordSignInAsync("Empty", "password", true, true);

			if(result.Succeeded)
			{
				if(string.IsNullOrEmpty(returnUrl))
					Redirect("");
				else
					Redirect(returnUrl);	
			}

			return Content("Failed to login");
		}

		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
			return Content("Done");
		}

		[Authorize]
		public IActionResult Private()
		{
			return Content($"Welcome {HttpContext.User.Identity.Name}");
		}
	}
}