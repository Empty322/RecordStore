using System.Collections.Generic;
using System.Linq;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
	public class RecordRepository : Repository<Record, int>, IRecordRepository
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

		public bool GetImageById(int id, out byte[] bytes, out string contentType)
		{
			bytes = null;
			contentType = null;

			// if artist does not exist, then return false;
			Record record = db.Records.FirstOrDefault(r => r.RecordId == id);
			if(record == null)
				return false;

			// if artist does not have image, then return false
			bytes = record.ImageData;
			contentType = record.ImageMimeType;
			if(bytes == null || contentType == null)
				return false;

			// return true
			return true;
		}
	}
}
