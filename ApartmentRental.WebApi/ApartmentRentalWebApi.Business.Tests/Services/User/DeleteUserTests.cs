using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

using ApartmentRentalWebApi.Business.Core.Enums;
using ApartmentRentalWebApi.Business.Core.Exceptions;
using ApartmentRentalWebApi.Business.Impl.Services;
using ApartmentRentalWebApi.Data;
using ApartmentRentalWebApi.TestModelBuilders.Builders;

namespace ApartmentRentalWebApi.Business.Tests.Services.User
{
	public class DeleteUserTests : UserServiceTest
	{
		[Fact]
		public async Task DeleteUser_NotFound_ThrowsNotFoundException()
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				var userService = new UserService(context, MockEmailService.Object, ErrorMessages.Object);
				await Assert.ThrowsAsync<NotFoundException>(() => userService.DeleteUser(Guid.Empty));
				context.Database.EnsureDeleted();
			}
		}

		[Fact]
		public async Task DeleteUser_HasManagedApartments_ThrowsConflictException()
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				var user = new UserBuilder().WithRole(RoleEnum.Realtor).Build();
				await context.Users.AddAsync(user);
				await context.SaveChangesAsync();

				var apartment = new ApartmentBuilder().WithRealtorId(user.Id).Build();
				await context.Apartments.AddAsync(apartment);
				await context.SaveChangesAsync();

				var userService = new UserService(context, MockEmailService.Object, ErrorMessages.Object);
				await Assert.ThrowsAsync<ConflictException>(() => userService.DeleteUser(user.Id));
				context.Database.EnsureDeleted();
			}
		}

		[Fact]
		public async Task DeleteUser_Success()
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				var user = new UserBuilder().Build();
				await context.Users.AddAsync(user);
				await context.SaveChangesAsync();

				var userService = new UserService(context, MockEmailService.Object, ErrorMessages.Object);
				await userService.DeleteUser(user.Id);
				context.Users.Count().Equals(0).Should().BeTrue();

				context.Database.EnsureDeleted();
			}
		}
	}
}