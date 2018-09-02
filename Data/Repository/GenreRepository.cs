using System;
using System.Collections.Generic;
using System.Text;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repository
{
	public class GenreRepository : Repository<Genre>, IGenreRepository
	{
		public GenreRepository(ApplicationDbContext db) : base(db)
		{

		}
	}
}
