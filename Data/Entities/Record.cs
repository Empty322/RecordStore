﻿using System.ComponentModel.DataAnnotations;

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
		public int Amount { get; set; }

		[Required]
		[MaxLength(15)]
		public string Type { get; set; }
	}
}