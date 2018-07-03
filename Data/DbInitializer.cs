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
					Artist = Entombed
				};
				Record KhaosLegions = new Record
				{
					Title = "Khaos legions",
					Amount = 10,
					Description = "",
					Type = "Vinyl",
					Artist = ArchEnemy
				};
				Record WarEternal = new Record
				{
					Title = "War eternal",
					Amount = 6,
					Description = "",
					Type = "CD",
					Artist = ArchEnemy
				};
				Record EndlessPain = new Record
				{
					Title = "Endless pain",
					Amount = 7,
					Description = "",
					Type = "CD",
					Artist = Kreator
				};
				Record PleasureToKill = new Record
				{
					Title = "Pleasure to kill",
					Amount = 3,
					Description = "",
					Type = "CD",
					Artist = Kreator
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
