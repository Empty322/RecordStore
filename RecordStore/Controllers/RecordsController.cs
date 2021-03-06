﻿using System;
using System.Collections.Generic;
using System.Linq;
using Data.Entities;
using Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace RecordStore.Controllers
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

		[Route("Records/Genre/{genreId}")]
		public IActionResult Genre(string genreId)
		{
			IEnumerable<Record> records = recordRepository.GetAll().Where(r => r.GenreId == genreId);
			return View("List", records);
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

		public IActionResult Search(string text) {
			if(text == null)
				text = "";
			List<Record> records = new List<Record>(
				recordRepository.Find(
					a => a.Title.ToUpper().Contains(text.ToUpper())
				)
			);
			return View("List", records);
		}

		public FileContentResult GetImage(int id)
		{
			if(recordRepository.GetImageById(id, out byte[] bytes, out string contentType))
				return new FileContentResult(bytes, contentType);
			else
				return new FileContentResult(null, "");
		}

        public IActionResult Error()
        {
            return View();
        }
	}
}