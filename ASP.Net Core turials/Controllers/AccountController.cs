using System.Threading.Tasks;
using ASP.Net_Core_turials.Models;
using Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ASP.Net_Core_turials.Controllers
{
	public class AccountController : Controller
	{
		private readonly SignInManager<ApplicationUser> signInManager;
		private readonly UserManager<ApplicationUser> userManager;

		public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
		{
			this.signInManager = signInManager;
			this.userManager = userManager;
		}

		public IActionResult Login()
		{
            return View();
        }
		
		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel loginViewModel)
		{
			if(ModelState.IsValid)
			{
				var result = await signInManager.PasswordSignInAsync(loginViewModel.Login, loginViewModel.Password, true, true);
				var res = User.IsInRole("Admin");
				var r = HttpContext.User;
				if(result.Succeeded)
				{
					return RedirectToAction("Index", "Admin", null);
				}
				else
					ModelState.AddModelError("", "Invalid login or password");
			}
			return View();
		}
    }
}