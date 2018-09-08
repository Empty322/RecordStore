using System;
using System.Collections.Generic;
using Data.Entities;
using Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace RecordStore.Controllers
{
    public class ArtistsController : Controller
    {
		private readonly IArtistRepository artistRepository;

		public ArtistsController(IArtistRepository artistRepository)
		{
			this.artistRepository = artistRepository;
		}

		public IActionResult List()
        {
			IEnumerable<Artist> artists = artistRepository.GetAll();
            return View(artists);
        }

		[Route("Artist/{id}")]
		public IActionResult Artist(int id)
		{
			Artist artist = artistRepository.GetById(id);
			if(artist != null)
				return View(artist);
			else
				return RedirectToAction("Error", "Record");
		}

		public IActionResult Search(string text) {
			throw new NotImplementedException();
		}

		public FileContentResult GetImage(int id)
		{
			if(artistRepository.GetImageById(id, out byte[] bytes, out string contentType))
				return new FileContentResult(bytes, contentType);
			else
				return new FileContentResult(null, "");
		}
	}
}