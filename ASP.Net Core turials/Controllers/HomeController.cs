using System.Collections.Generic;
using ASP.Net_Core_turials.Models;
using Data.Entities;
using Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ASP.Net_Core_turials.Controllers
{
	public class HomeController : Controller
    {
		private readonly IRecordRepository recordRepository;
		private readonly IArtistRepository artistRepository;

		public HomeController(IRecordRepository recordRepository, IArtistRepository artistRepository)
		{
			this.recordRepository = recordRepository;
			this.artistRepository = artistRepository;
		}

		public IActionResult Index()
		{
			IEnumerable<Record> records = recordRepository.GetAll();
			List<RecordViewModel> recordsVM = new List<RecordViewModel>();
			foreach(Record record in records)
			{
				recordsVM.Add(new RecordViewModel { Record = record, ArtistName = artistRepository.GetById(record.ArtistId).Name});
			}
            return View(recordsVM);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
