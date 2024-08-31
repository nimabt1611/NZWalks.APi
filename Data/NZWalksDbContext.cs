using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
	public class NZWalksDbContext : IdentityDbContext
	{
		public NZWalksDbContext(DbContextOptions<NZWalksDbContext> options) : base(options)
		{
		}

		public DbSet<Walk> Walks { get; set; }
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
		public DbSet<Image> Images { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			var Difficulties = new List<Difficulty>()
			{
				new Difficulty
				{
					Id = Guid.Parse("689777aa-93af-4bd6-bbfd-2b028608a7a0"),
					Name = "Easy"
				},
				new Difficulty
				{
					Id = Guid.Parse("e2b02957-d53d-4069-9a77-89077f3d8289"),
					Name = "Medium"
				},
				new Difficulty
				{
					Id = Guid.Parse("073dd94e-50a7-4bc2-89b5-685f11b37890"),
					Name = "Hard"
				}
			};
			modelBuilder.Entity<Difficulty>().HasData(Difficulties);

			var Regioins = new List<Region>()
			{
				new Region
				{
					Id = Guid.Parse("3ed25886-8d16-46e5-8ab7-c35c47a1fb95"),
					Name = "Isfahan",
					Code = "Isf",
					RegionIMageUrl = "Isfahan-Image"
				},

					new Region
				{
					Id = Guid.Parse("4d9d8d2f-dc7c-484d-87c0-6fc5c04d660d"),
					Name = "Shahin Shahr",
					Code = "Sh2",
					RegionIMageUrl = "Shahin Shahr"
				},

						new Region
				{
					Id = Guid.Parse("c5411e30-bdcb-4d7e-acfa-f4095683934c"),
					Name = "Lakan",
					Code = "Lak",
					RegionIMageUrl = "Lakan-Image"
				}


			};
			modelBuilder.Entity<Region>().HasData(Regioins);

			var ReaderRoleId = "b3dcb28e-96a3-4062-ad95-10c0a8ce07bd";
			var WriterRoleId = "f9ff3bf3-cd24-4232-931f-1dc6f460085d";

			var roles = new List<IdentityRole>{

				new IdentityRole
				{
					Id = ReaderRoleId,
					ConcurrencyStamp = ReaderRoleId,
					Name = "Reader",
					NormalizedName = "Reader".ToUpper()
				},
				new IdentityRole
				{
					Id = WriterRoleId,
					ConcurrencyStamp = WriterRoleId,
					Name="Writer",
					NormalizedName ="Writer".ToUpper()
				}



			};
			modelBuilder.Entity<IdentityRole>().HasData(roles);

		}


	}
	

}
