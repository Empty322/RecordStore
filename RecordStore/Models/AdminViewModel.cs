using System.Collections.Generic;
using Data.Entities;

namespace RecordStore.Models
{
    public class AdminViewModel
    {
		public IEnumerable<Country> Countries { get; set; }
		public IEnumerable<Genre> Genres { get; set; }
		public IEnumerable<Artist> Artists { get; set; }
		public IEnumerable<Record> Records { get; set; }
	}
}
