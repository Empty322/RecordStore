using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Entities;

namespace ASP.Net_Core_turials.Models
{
    public class AdminViewModel
    {
		public IEnumerable<Country> Countries { get; set; }
		public IEnumerable<Artist> Artists { get; set; }
		public IEnumerable<Record> Records { get; set; }
	}
}
