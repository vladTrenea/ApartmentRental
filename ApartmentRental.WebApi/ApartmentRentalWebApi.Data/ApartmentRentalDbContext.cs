using Microsoft.EntityFrameworkCore;

using ApartmentRentalWebApi.Data.Configurations;
using ApartmentRentalWebApi.Domain.Entities;

namespace ApartmentRentalWebApi.Data
{
	public class ApartmentRentalDbContext : DbContext
	{
		public virtual DbSet<User> Users { get; set; }
		public virtual DbSet<Role> Roles { get; set; }
		public virtual DbSet<Apartment> Apartments { get; set; }

		public ApartmentRentalDbContext(DbContextOptions<ApartmentRentalDbContext> options)
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new UserConfiguration());
			modelBuilder.ApplyConfiguration(new RoleConfiguration());
			modelBuilder.ApplyConfiguration(new ApartmentConfiguration());
		}
	}
}