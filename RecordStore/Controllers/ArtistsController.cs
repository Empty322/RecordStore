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

		public FileContentResult GetImage(int id)
		{
			if(artistRepository.GetImageById(id, out byte[] bytes, out string contentType))
				return new FileContentResult(bytes, contentType);
			else
				return new FileContentResult(null, "");
		}
	}
}