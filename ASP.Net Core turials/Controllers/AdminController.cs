﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using ASP.Net_Core_turials.Models;
using Data.Entities;
using Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ASP.Net_Core_turials.Controllers
{
	[Authorize(Roles = "Admin")]
	public class AdminController : Controller
    {
		private readonly ICountryRepository countryRepository;
		private readonly IArtistRepository artistRepository;
		private readonly IRecordRepository recordRepository;

		public AdminController(ICountryRepository countryRepository, IArtistRepository artistRepository, IRecordRepository recordRepository)
		{
			this.countryRepository = countryRepository;
			this.artistRepository = artistRepository;
			this.recordRepository = recordRepository;
		}		

		public IActionResult Index()
        {
			AdminViewModel model = new AdminViewModel
			{
				Countries = countryRepository.GetAll(),
				Artists = artistRepository.GetAll(),
				Records = recordRepository.GetAll()
			};
            return View(model);
        }

		#region Country actions

		[HttpGet]
		public IActionResult NewCountry()
		{
			return View();
		}

		[HttpPost]
		public IActionResult NewCountry(Country country)
		{
			if(ModelState.IsValid)
			{
				countryRepository.Create(country);
				return RedirectToAction(nameof(Index));
			}
			return View(country);
		}

		public IActionResult DeleteCountry(string countryName)
		{
			Country country = countryRepository.GetAll().FirstOrDefault(c => c.CountryName == countryName);
			countryRepository.Delete(country);
			return RedirectToAction(nameof(Index));
		}

		#endregion

		#region Artist actions

		[HttpGet]
		public IActionResult NewArtist()
		{
			return View();
		}

		[HttpPost]
		public IActionResult NewArtist(Artist artist, IFormFile image)
		{
			if(ModelState.IsValid)
			{
				if(image != null && image.Length < 16777216)
				{
					artist.ImageMimeType = image.ContentType;
					using(BinaryReader reader = new BinaryReader(image.OpenReadStream()))
					{
						artist.ImageData = reader.ReadBytes((int)image.Length);
					}
				}
				artistRepository.Create(artist);
				return RedirectToAction(nameof(Index));
			}
			return View(artist);
		}

		public IActionResult DeleteArtist(int artistId)
		{
			Artist artist = artistRepository.GetById(artistId);
			artistRepository.Delete(artist);
			return RedirectToAction(nameof(Index));
		}

		#endregion

		#region Record actions

		[HttpGet]
		public IActionResult NewRecord()
		{
			return View();
		}

		[HttpPost]
		public IActionResult NewRecord(Record record, IFormFile image)
		{
			if(ModelState.IsValid)
			{
				if(image != null && image.Length < 16777216)
				{
					record.ImageMimeType = image.ContentType;
					using(BinaryReader reader = new BinaryReader(image.OpenReadStream()))
					{
						record.ImageData = reader.ReadBytes((int)image.Length);
					}
				}
				recordRepository.Create(record);
				return RedirectToAction(nameof(Index));
			}
			return View(record);
		}

		public IActionResult DeleteRecord(int recordId)
		{
			Record record = recordRepository.GetById(recordId);
			recordRepository.Delete(record);
			return RedirectToAction(nameof(Index));
		}



		#endregion

		#region API methods

		[Produces("application/json")]
		[HttpGet("/api/GetCountries")]
		public IActionResult GetCountries()
		{
			return Ok(JsonConvert.SerializeObject(countryRepository.GetAll()));
		}

		[Produces("application/json")]
		[HttpGet("/api/GetArtists")]
		public IActionResult GetArtists()
		{
			return Ok(JsonConvert.SerializeObject(artistRepository.GetAll()));
		}

		#endregion
	}
}