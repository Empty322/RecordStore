using System.Collections.Generic;
using System.IO;
using ASP.Net_Core_turials.Models;
using Data.Entities;
using Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ASP.Net_Core_turials.Controllers
{
	public class RecordsController : Controller
    {
		private readonly IRecordRepository recordRepository;
		private readonly IArtistRepository artistRepository;

		public RecordsController(IRecordRepository recordRepository, IArtistRepository artistRepository)
		{
			this.recordRepository = recordRepository;
			this.artistRepository = artistRepository;
		}

		public IActionResult List()
		{
			IEnumerable<Record> records = recordRepository.GetAll();
            return View(records);
        }

		[Route("Record/{id}")]
		public IActionResult Record(int id)
		{
			Record record = recordRepository.GetById(id);
			if(record != null)
				return View(record);
			else
				return RedirectToAction(nameof(Error));
		}

		public FileContentResult GetImage(int id)
		{
			Record record = recordRepository.GetById(id);
			if(record.ImageData != null && record.ImageMimeType != null)
				return new FileContentResult(record.ImageData, record.ImageMimeType);
			else
				return null;
		}

        public IActionResult Error()
        {
            return View();
        }
	}
}
