using System.Collections.Generic;
using System.Linq;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

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

		public override IEnumerable<Record> GetAll()
		{
			return db.Records.Include(r => r.Artist);
		}

		public override Record GetById(int id)
		{
			return db.Records.Include(r => r.Artist).FirstOrDefault(r => r.RecordId == id);
		}
	}
}
