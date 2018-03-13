using System.Collections.Generic;
using System.Linq;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repository
{
	public class RecordRepository : Repository<Record>, IRecordRepository
	{
		public RecordRepository(ApplicationDbContext db) : base(db)
		{

		}

		public IEnumerable<Record> GetRecordsByType(string type)
		{
			return db.Records.Where(r => r.Type == type);
		}
	}
}
