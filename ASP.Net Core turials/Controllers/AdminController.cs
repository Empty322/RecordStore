using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASP.Net_Core_turials.Controllers
{
	[Authorize(Roles = "Admin")]
	public class AdminController : Controller
    {		
        public IActionResult Index()
        {
            return View();
        }
    }
}