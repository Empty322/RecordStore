using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
	public class Artist
    {
		[Key]
		public int ArtistId { get; set; }

		[Required]
		[MaxLength(150)]
		public string Name { get; set; }

		[Required]
		public string Description { get; set; }

		public Country Country { get; set; }

		[Required]
		public string CountryName { get; set; }

		public List<Record> Records { get; set; }

		public byte[] ImageData { get; set; }

		public string ImageMimeType { get; set; }
	}
}
