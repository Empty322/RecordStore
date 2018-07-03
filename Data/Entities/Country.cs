using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.Entities
{
    public class Country
    {
		[Key]
		[Required]
		public string CountryName { get; set; }

		public List<Artist> Artists { get; set; }
	}
}
