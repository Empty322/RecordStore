using System.Linq;
using Data.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Data
{
	public static class DbInitializer
	{
		public static void Seed(IApplicationBuilder app)
		{
			using(var serviceScoupe = app.ApplicationServices.CreateScope())
			{
				var context = serviceScoupe.ServiceProvider.GetService<ApplicationDbContext>();

				if(context.Countries.Any())
					return;

				Country Sweden = new Country { CountryName = "Sweden" };
				Country Germany = new Country { CountryName = "Germany" };

				Genre Death = new Genre { Id = "Death" };
				Genre Thrash = new Genre { Id = "Thrash" };

				Artist Entombed = new Artist
				{
					Name = "Entombed",
					Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit.",
					Country = Sweden
				};
				Artist ArchEnemy = new Artist
				{
					Name = "Arch Enemy",
					Description = "In enim justo, rhoncus ut, imperdiet a, venenatis vitae, justo.",
					Country = Sweden
				};
				Artist Kreator = new Artist
				{
					Name = "Kreator",
					Description = "Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu.",
					Country = Germany
				};

				Record LeftHandPath = new Record
				{
					Title = "Left hand path",
					Amount = 5,
					Description = "Quisque rutrum. Aenean imperdiet. Etiam ultricies nisi vel augue.",
					Type = "CD",
					Artist = Entombed,
					Genre = Death
				};
				Record KhaosLegions = new Record
				{
					Title = "Khaos legions",
					Amount = 10,
					Description = "",
					Type = "Vinyl",
					Artist = ArchEnemy,
					Genre = Death
				};
				Record WarEternal = new Record
				{
					Title = "War eternal",
					Amount = 6,
					Description = "",
					Type = "CD",
					Artist = ArchEnemy,
					Genre = Death
				};
				Record EndlessPain = new Record
				{
					Title = "Endless pain",
					Amount = 7,
					Description = "",
					Type = "CD",
					Artist = Kreator,
					Genre = Thrash
				};
				Record PleasureToKill = new Record
				{
					Title = "Pleasure to kill",
					Amount = 3,
					Description = "",
					Type = "CD",
					Artist = Kreator,
					Genre = Thrash
				};

				context.Countries.Add(Sweden);
				context.Countries.Add(Germany);

				context.Artists.Add(Entombed);
				context.Artists.Add(ArchEnemy);
				context.Artists.Add(Kreator);

				context.Records.Add(LeftHandPath);
				context.Records.Add(KhaosLegions);
				context.Records.Add(WarEternal);
				context.Records.Add(EndlessPain);
				context.Records.Add(PleasureToKill);

				context.SaveChanges();
			}
		}
	}
}
