using System.Collections.Generic;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
	public class CountryRepository : Repository<Country, string>, ICountryRepository
	{
		public CountryRepository(ApplicationDbContext db) : base(db)
		{
		}

		public override IEnumerable<Country> GetAll()
		{
			return db.Countries.Include(c => c.Artists).ThenInclude(a => a.Records);
		}
	}
}
