using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using ApartmentRentalWebApi.Domain.Entities;

namespace ApartmentRentalWebApi.Data.Configurations
{
	internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.ToTable(DbConstants.UsersTable);
			builder.Property<Guid>(e => e.Id)
				.HasColumnName("id")
				.ValueGeneratedNever();
			builder.HasKey(e => e.Id);

			builder.Property<string>(e => e.FirstName)
				.HasColumnName("first_name")
				.IsRequired();

			builder.Property<string>(e => e.LastName)
				.HasColumnName("last_name")
				.IsRequired();

			builder.Property<string>(e => e.Email)
				.HasColumnName("email")
				.IsRequired();

			builder.Property<string>(e => e.Password)
				.HasColumnName("password");

			builder.Property<DateTime>(e => e.CreatedAt)
				.HasColumnName("created_at")
				.IsRequired();

			builder.Property<bool>(e => e.EmailConfirmed)
				.HasColumnName("email_confirmed")
				.IsRequired();

			builder.Property<DateTime?>(e => e.ConfirmedAt)
				.HasColumnName("confirmed_at");

			builder.Property<string>(e => e.EmailConfirmationToken)
				.HasColumnName("email_confirmation_token");

			builder.Property<string>(e => e.PasswordResetToken)
				.HasColumnName("password_reset_token");

			builder.Property<DateTime?>(e => e.PasswordResetEndDate)
				.HasColumnName("password_reset_end_date");

			builder.Property(e => e.RoleId)
				.HasColumnName("role_id")
				.IsRequired();

			builder.HasOne<Role>(e => e.Role)
				.WithMany()
				.HasForeignKey(e => e.RoleId)
				.HasConstraintName("fk_user_role_id")
				.IsRequired();

			builder.HasMany(e => e.ManagedApartments)
				.WithOne(e => e.Realtor)
				.HasForeignKey(u => u.RealtorId)
				.HasConstraintName("fk_user_apartment_id")
				.IsRequired();
		}
	}
}