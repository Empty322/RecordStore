using System;
using System.ComponentModel.DataAnnotations;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
		public DbSet<Record> Records { get; set; }

		public DbSet<Artist> Artists { get; set; }

		public DbSet<Country> Countries { get; set; }

		public DbSet<Genre> Genres { get; set; }

		public DbSet<Order> Orders { get; set; }

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
			Database.EnsureCreated();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			if(modelBuilder == null)
			{
				throw new ArgumentNullException(nameof(modelBuilder));
			}

			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Artist>().HasOne(a => a.Country).WithMany(c => c.Artists).HasForeignKey(a => a.CountryName);
			modelBuilder.Entity<Record>().HasOne(r => r.Artist).WithMany(a => a.Records).HasForeignKey(r => r.ArtistId);
			modelBuilder.Entity<Record>().HasOne(r => r.Genre).WithMany(g => g.Records).HasForeignKey(r => r.GenreId);

			//modelBuilder.Entity<Artist>().Property(a => a.ImageData).HasColumnType("MEDIUMBLOB");
			//modelBuilder.Entity<Record>().Property(r => r.ImageData).HasColumnType("MEDIUMBLOB");
			modelBuilder.Entity<Artist>().Property(a => a.ImageData).HasColumnType("VARBINARY(MAX)");
			modelBuilder.Entity<Record>().Property(r => r.ImageData).HasColumnType("VARBINARY(MAX)");

			modelBuilder.Entity<ApplicationUser>().Property(u => u.Id).HasMaxLength(128);
			modelBuilder.Entity<IdentityRole>().Property(r => r.Id).HasMaxLength(128);
			modelBuilder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(128));
			modelBuilder.Entity<IdentityUserRole<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(128));
			modelBuilder.Entity<IdentityUserRole<string>>(entity => entity.Property(m => m.RoleId).HasMaxLength(128));
			modelBuilder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(128));
			modelBuilder.Entity<IdentityUserClaim<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(128));
			modelBuilder.Entity<IdentityRoleClaim<string>>(entity => entity.Property(m => m.RoleId).HasMaxLength(128));
			modelBuilder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.LoginProvider).HasMaxLength(128));
			modelBuilder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.ProviderKey).HasMaxLength(128));
			modelBuilder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.Name).HasMaxLength(128));
			modelBuilder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.LoginProvider).HasMaxLength(128));
		}
	}
}
