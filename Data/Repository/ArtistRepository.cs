using System.Collections.Generic;
using System.Linq;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repository
{
	public class ArtistRepository : Repository<Artist>, IArtistRepository
	{
		public ArtistRepository(ApplicationDbContext db) : base(db)
		{

		}

		public IEnumerable<Artist> GetByCountry(Country country)
		{
			return db.Artists.Where(a => a.Country == country);
		}

		public IEnumerable<Record> GetRecordsByArtist(Artist artist)
		{
			return db.Records.Where(r => r.Artist == artist);
		}
	}
}
