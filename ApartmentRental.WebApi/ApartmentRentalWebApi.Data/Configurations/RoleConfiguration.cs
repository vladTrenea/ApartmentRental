using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using ApartmentRentalWebApi.Domain.Entities;

namespace ApartmentRentalWebApi.Data.Configurations
{
	internal sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
	{
		public void Configure(EntityTypeBuilder<Role> builder)
		{
			builder.ToTable(DbConstants.RolesTable);
			builder.Property<int>(e => e.Id)
				.HasColumnName("id")
				.ValueGeneratedNever();
			builder.HasKey(e => e.Id);

			builder.Property<string>(e => e.Name)
				.HasColumnName("name")
				.IsRequired();

			builder.HasMany(e => e.Users)
				.WithOne(e => e.Role)
				.HasForeignKey(u => u.RoleId)
				.IsRequired();
		}
	}
}