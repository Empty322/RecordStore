using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
	public class Record
	{
		[Key]
		public int RecordId { get; set; }

		[Required]
		[MaxLength(150)]
		public string Title { get; set; }

		[Required]
		public string Description { get; set; }

		[Required]
		public int Amount { get; set; }

		[Required]
		[MaxLength(15)]
		public string Type { get; set; }

		public Artist Artist { get; set; }

		[Required]
		public int ArtistId { get; set; }

		public byte[] ImageData { get; set; }

		public string ImageMimeType { get; set; }
	}
}
