using Data.Entities;
using Microsoft.EntityFrameworkCore;
using MySql.Data.EntityFrameworkCore.Extensions;

namespace Data
{
	public class ApplicationContext : DbContext
    {
		public DbSet<Vinyl> Vinyls { get; set; }
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseMySQL("Server=localhost;Port=3306;Database=test;UserId=root;Password=root;");
		}
	}
}
