using System;
using ApartmentRentalWebApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApartmentRentalWebApi.Data.Configurations
{
	internal sealed class ApartmentConfiguration : IEntityTypeConfiguration<Apartment>
	{
		public void Configure(EntityTypeBuilder<Apartment> builder)
		{
			builder.ToTable(DbConstants.ApartmentsTable);
			builder.Property<Guid>(e => e.Id)
				.HasColumnName("id")
				.ValueGeneratedNever();
			builder.HasKey(e => e.Id);

			builder.Property<string>(e => e.Title)
				.HasColumnName("title")
				.IsRequired();

			builder.Property<string>(e => e.Description)
				.HasColumnName("description");

			builder.Property<double>(e => e.Area)
				.HasColumnName("area")
				.IsRequired();

			builder.Property<int>(e => e.NrOfRooms)
				.HasColumnName("nr_of_rooms")
				.IsRequired();

			builder.Property<double>(e => e.PricePerMonth)
				.HasColumnName("price_per_month")
				.IsRequired();

			builder.Property<DateTime>(e => e.CreatedAt)
				.HasColumnName("created_at")
				.IsRequired();

			builder.Property<double>(e => e.Latitude)
				.HasColumnName("latitude")
				.IsRequired();

			builder.Property<double>(e => e.Longitude)
				.HasColumnName("longitude")
				.IsRequired();

			builder.Property<bool>(e => e.IsRented)
				.HasColumnName("is_rented")
				.IsRequired();

			builder.Property(e => e.RealtorId)
				.HasColumnName("realtor_id")
				.IsRequired();

			builder.HasOne<User>(e => e.Realtor)
				.WithMany(a => a.ManagedApartments)
				.HasForeignKey(e => e.RealtorId)
				.IsRequired();
		}
	}
}