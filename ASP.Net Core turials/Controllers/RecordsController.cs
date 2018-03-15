using System.Collections.Generic;
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
			List<RecordListViewModel> recordsVM = new List<RecordListViewModel>();
			foreach(Record record in records)
			{
				recordsVM.Add(new RecordListViewModel { Record = record, ArtistName = artistRepository.GetById(record.ArtistId).Name});
			}
            return View(recordsVM);
        }

		[Route("Record/{id}")]
		public IActionResult Record(int id)
		{
			Record record = recordRepository.GetById(id);
			if(record != null)
			{
				Artist artist = artistRepository.GetById(record.ArtistId);
				return View(new RecordViewModel { Record = record, Artist = artist });
			}
			else
				return RedirectToAction(nameof(Error));
		}

		public FileContentResult GetImage(int id)
		{
			Record record = recordRepository.GetById(id);
			if(record != null)
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
