using System.Threading.Tasks;
using Data;
using Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ASP.Net_Core_turials.Controllers
{
    public class HomeController : Controller
    {
		private IRecordRepository recordRepository;
		
		public HomeController(IRecordRepository recordRepository)
		{
			this.recordRepository = recordRepository;
		}

		public IActionResult Index()
		{
			var result = recordRepository.GetAll();
            return View(result);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
