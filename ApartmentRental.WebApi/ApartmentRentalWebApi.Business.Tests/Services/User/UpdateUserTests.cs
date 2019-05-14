using System;
using System.Threading.Tasks;
using ApartmentRentalWebApi.Business.Core.Enums;
using Xunit;
using FluentAssertions;

using ApartmentRentalWebApi.Business.Core.Exceptions;
using ApartmentRentalWebApi.Business.Impl.Services;
using ApartmentRentalWebApi.Data;
using ApartmentRentalWebApi.TestModelBuilders.Builders;
using ApartmentRentalWebApi.TestModelBuilders.Constants;

namespace ApartmentRentalWebApi.Business.Tests.Services.User
{
	public class UpdateUserTests : UserServiceTest
	{
		[Fact]
		public async Task UpdateUser_InvalidFirstName_ThrowsValidationException()
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				var user = new UserBuilder().Build();
				await context.Users.AddAsync(user);
				await context.SaveChangesAsync();

				var userService = new UserService(context, MockEmailService.Object, ErrorMessages.Object);

				var userWithFirstNameNull = new UserUpdateDtoBuilder().WithFirstName(null).Build();
				await Assert.ThrowsAsync<ValidationException>(() => userService.UpdateUser(user.Id, userWithFirstNameNull));

				var userWithFirstNameEmpty = new UserUpdateDtoBuilder().WithFirstName(string.Empty).Build();
				await Assert.ThrowsAsync<ValidationException>(() => userService.UpdateUser(user.Id, userWithFirstNameEmpty));

				var userWithInvalidFirstName = new UserUpdateDtoBuilder().WithFirstName(UserTestConstants.InvalidFirstName).Build();
				await Assert.ThrowsAsync<ValidationException>(() => userService.UpdateUser(user.Id, userWithInvalidFirstName));

				context.Database.EnsureDeleted();
			}
		}

		[Fact]
		public async Task UpdateUser_InvalidLastName_ThrowsValidationException()
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				var user = new UserBuilder().Build();
				await context.Users.AddAsync(user);
				await context.SaveChangesAsync();

				var userService = new UserService(context, MockEmailService.Object, ErrorMessages.Object);

				var userWithLastNameNull = new UserUpdateDtoBuilder().WithLastName(null).Build();
				await Assert.ThrowsAsync<ValidationException>(() => userService.UpdateUser(user.Id, userWithLastNameNull));

				var userWithLastNameEmpty = new UserUpdateDtoBuilder().WithLastName(string.Empty).Build();
				await Assert.ThrowsAsync<ValidationException>(() => userService.UpdateUser(user.Id, userWithLastNameEmpty));

				var userWithInvalidLastName = new UserUpdateDtoBuilder().WithLastName(UserTestConstants.InvalidLastName).Build();
				await Assert.ThrowsAsync<ValidationException>(() => userService.UpdateUser(user.Id, userWithInvalidLastName));

				context.Database.EnsureDeleted();
			}
		}

		[Fact]
		public async Task UpdateUser_InvalidRoleId_ThrowsValidationException()
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				var user = new UserBuilder().Build();
				await context.Users.AddAsync(user);
				await context.SaveChangesAsync();

				var userService = new UserService(context, MockEmailService.Object, ErrorMessages.Object);

				var userWithRoleIdNull = new UserUpdateDtoBuilder().WithRoleId(null).Build();
				await Assert.ThrowsAsync<ValidationException>(() => userService.UpdateUser(user.Id, userWithRoleIdNull));

				var userWithRoleAdmin = new UserUpdateDtoBuilder().WithRoleId(RoleEnum.Admin).Build();
				await Assert.ThrowsAsync<ValidationException>(() => userService.UpdateUser(user.Id, userWithRoleAdmin));

				context.Database.EnsureDeleted();
			}
		}

		[Fact]
		public async Task UpdateUser_NotFound_ThrowsNotFoundException()
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				var userService = new UserService(context, MockEmailService.Object, ErrorMessages.Object);

				var user = new UserUpdateDtoBuilder().Build();
				await Assert.ThrowsAsync<NotFoundException>(() => userService.UpdateUser(Guid.Empty, user));

				context.Database.EnsureDeleted();
			}
		}

		[Fact]
		public async Task UpdateUser_Success()
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				var user = new UserBuilder().Build();
				await context.Users.AddAsync(user);
				await context.SaveChangesAsync();

				var userService = new UserService(context, MockEmailService.Object, ErrorMessages.Object);

				var userToUpdate = new UserUpdateDtoBuilder().Build();
				await userService.UpdateUser(user.Id, userToUpdate);

				user.FirstName.Equals(userToUpdate.FirstName).Should().BeTrue();
				user.LastName.Equals(userToUpdate.LastName).Should().BeTrue();
				user.RoleId.Equals((int)userToUpdate.RoleId.Value).Should().BeTrue();

				context.Database.EnsureDeleted();
			}
		}
	}
}