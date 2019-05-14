using System;
using System.Collections.Generic;
using System.Linq;

using ApartmentRentalWebApi.Domain.Entities;

namespace ApartmentRentalWebApi.Data
{
	public static class DbInitializer
	{
		public const string TestPassword = "Admin123";

		public static void Seed(ApartmentRentalDbContext context)
		{
			context.Database.EnsureCreated();

			if (!context.Roles.Any())
			{
				context.Roles.AddRange(Roles);
				context.SaveChanges();
			}

			if (!context.Users.Any(u => u.RoleId == 3))
			{
				context.Add(Admin);
				context.SaveChanges();
			}

			if (!context.Users.Any(u => u.RoleId == 2))
			{
				context.Add(Realtor);
				context.SaveChanges();
			}

			if (!context.Users.Any(u => u.RoleId == 1))
			{
				context.Add(User);
				context.SaveChanges();
			}
		}

		public static readonly User Admin = new User
		{
			Id = Guid.NewGuid(),
			Email = "adminApRental@yopmail.com",
			Password = "68-D2-CB-B7-F1-5A-EC-34-5F-BF-42-A6-32-6D-DF-EE-03-E6-17-08", //Admin123
			FirstName = "Admin",
			LastName = "ApRental",
			CreatedAt = DateTime.Now,
			EmailConfirmed = true,
			EmailConfirmationToken = null,
			ConfirmedAt = DateTime.Now,
			RoleId = 3
		};

		public static readonly User Realtor = new User
		{
			Id = Guid.NewGuid(),
			Email = "realtorApRental@yopmail.com",
			Password = "68-D2-CB-B7-F1-5A-EC-34-5F-BF-42-A6-32-6D-DF-EE-03-E6-17-08", //Admin123
			FirstName = "Realtor",
			LastName = "ApRental",
			CreatedAt = DateTime.Now,
			EmailConfirmed = true,
			EmailConfirmationToken = null,
			ConfirmedAt = DateTime.Now,
			RoleId = 2
		};

		public static readonly User User = new User
		{
			Id = Guid.NewGuid(),
			Email = "userApRental@yopmail.com",
			Password = "68-D2-CB-B7-F1-5A-EC-34-5F-BF-42-A6-32-6D-DF-EE-03-E6-17-08", //Admin123
			FirstName = "User",
			LastName = "ApRental",
			CreatedAt = DateTime.Now,
			EmailConfirmed = true,
			EmailConfirmationToken = null,
			ConfirmedAt = DateTime.Now,
			RoleId = 1
		};

		private static readonly IEnumerable<Role> Roles = new List<Role>
		{
			new Role
			{
				Id = 1,
				Name = "Client"
			},
			new Role
			{
				Id = 2,
				Name = "Realtor"
			},
			new Role
			{
				Id = 3,
				Name = "Admin"
			}
		};
	}
}