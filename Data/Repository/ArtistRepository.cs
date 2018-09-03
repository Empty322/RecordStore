using System.Collections.Generic;
using System.Linq;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Data.Repository
{
	public class ArtistRepository : Repository<Artist, int>, IArtistRepository
	{
		public ArtistRepository(ApplicationDbContext db) : base(db)
		{

		}

		public override Artist GetById(int id)
		{
			return db.Artists.Include(a => a.Records).FirstOrDefault(a => a.ArtistId == id);
		}

		public IEnumerable<Artist> GetByCountry(Country country)
		{
			return db.Artists.Where(a => a.Country == country);
		}

		public IEnumerable<Record> GetRecordsByArtist(Artist artist)
		{
			return db.Records.Where(r => r.Artist == artist);
		}

		public bool GetImageById(int id, out byte[] bytes, out string contentType)
		{
			bytes = null;
			contentType = null;

			// if artist does not exist, then return false;
			Artist artist = db.Artists.FirstOrDefault(a => a.ArtistId == id);
			if(artist == null)
				return false;

			// if artist does not have image, then return false
			bytes = artist.ImageData;
			contentType = artist.ImageMimeType;
			if(bytes == null || contentType == null)
				return false;

			// return true
			return true;
		}
	}
}
