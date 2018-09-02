using System.IO;
using System.Linq;
using RecordStore.Models;
using Data.Entities;
using Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace RecordStore.Controllers
{
	[Authorize(Roles = "Admin")]
	public class AdminController : Controller
    {
		private readonly ICountryRepository countryRepository;
		private readonly IArtistRepository artistRepository;
		private readonly IRecordRepository recordRepository;
		private readonly IGenreRepository genreRepository;

		public AdminController(ICountryRepository countryRepository, IArtistRepository artistRepository, IRecordRepository recordRepository, IGenreRepository genreRepository)
		{
			this.countryRepository = countryRepository;
			this.artistRepository = artistRepository;
			this.recordRepository = recordRepository;
			this.genreRepository = genreRepository;
		}		

		public IActionResult Index()
        {
			AdminViewModel model = new AdminViewModel
			{
				Countries = countryRepository.GetAll(),
				Genres = genreRepository.GetAll(),
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

		#endregion

		#region Genre actions

		[HttpGet]
		public IActionResult NewGenre()
		{
			return View();
		}

		[HttpPost]
		public IActionResult NewGenre(Genre genre)
		{
			if(ModelState.IsValid)
			{
				genreRepository.Create(genre);
				return RedirectToAction(nameof(Index));
			}
			return View(genre);
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
				else
				{
					using (BinaryReader reader = new BinaryReader(new FileStream("wwwroot/img/placeholder.jpg", FileMode.Open)))
					{
						FileInfo info = new FileInfo("wwwroot/img/placeholder.jpg");
						artist.ImageData = reader.ReadBytes((int)info.Length);
						artist.ImageMimeType = "image/jpeg";
					}
				}
				artistRepository.Create(artist);
				return RedirectToAction(nameof(Index));
			}
			return View(artist);
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
				else
				{
					using(BinaryReader reader = new BinaryReader(new FileStream("wwwroot/img/placeholder.jpg", FileMode.Open)))
					{
						FileInfo info = new FileInfo("wwwroot/img/placeholder.jpg");
						record.ImageData = reader.ReadBytes((int)info.Length);
						record.ImageMimeType = "image/jpeg";
					}
				}
				recordRepository.Create(record);
				return RedirectToAction(nameof(Index));
			}
			return View(record);
		}

		#endregion
	}
}