using Microsoft.AspNetCore.Mvc;

namespace RecordStore.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
