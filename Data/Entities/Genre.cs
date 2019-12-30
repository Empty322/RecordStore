using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.Entities
{
    public class Genre
    {
		[Key]
		[Required]
		public string Id { get; set; }

		public List<Record> Records { get; set; }
	}
}
