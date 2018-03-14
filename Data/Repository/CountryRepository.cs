using Data.Entities;
using Data.Interfaces;

namespace Data.Repository
{
	public class CountryRepository : Repository<Country>, ICountryRepository
	{
		public CountryRepository(ApplicationDbContext db) : base(db)
		{
		}
	}
}
