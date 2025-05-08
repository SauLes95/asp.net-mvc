using Microsoft.EntityFrameworkCore;
using Vjezba.Model;

namespace Vjezba.DAL
{
	public class ClientManagerDbContext : DbContext
	{
		public ClientManagerDbContext(DbContextOptions<ClientManagerDbContext> options)
			: base(options)
		{

		}

		public DbSet<Client> Clients { get; set; }
		public DbSet<City> Cities { get; set; }
		public DbSet<Meeting> Meetings { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<City>().HasData(
				new City { ID = 1, Name = "Zagreb" },
				new City { ID = 2, Name = "Split" },
				new City { ID = 3, Name = "Rijeka" },
				new City { ID = 4, Name = "Osijek" },
				new City { ID = 5, Name = "Zadar" },
				new City { ID = 6, Name = "Velika Gorica" },
				new City { ID = 7, Name = "Pula" },
				new City { ID = 8, Name = "Slavonski Brod" },
				new City { ID = 9, Name = "Karlovac" },
				new City { ID = 10, Name = "Varaždin" });
		}

	}
}
