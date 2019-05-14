using System;
using System.Threading.Tasks;
using Xunit;

using ApartmentRentalWebApi.Business.Core.Exceptions;
using ApartmentRentalWebApi.Business.Impl.Services;
using ApartmentRentalWebApi.Data;
using ApartmentRentalWebApi.TestModelBuilders.Builders;
using ApartmentRentalWebApi.TestModelBuilders.Constants;
using FluentAssertions;

namespace ApartmentRentalWebApi.Business.Tests.Services.Auth
{
	public class UpdateAccountTests : AuthServiceTest
	{
		[Fact]
		public async Task UpdateAccount_InvalidFirstName_ThrowsValidationException()
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				var user = new UserBuilder().Build();
				await context.Users.AddAsync(user);
				await context.SaveChangesAsync();

				var authService = new AuthService(context, MockEmailService.Object, HashService.Object, ErrorMessages.Object);

				var userWithFirstNameNull = new AccountDtoBuilder().WithFirstName(null).Build();
				await Assert.ThrowsAsync<ValidationException>(() => authService.UpdateAccount(user.Id, userWithFirstNameNull));

				var userWithFirstNameEmpty = new AccountDtoBuilder().WithFirstName(string.Empty).Build();
				await Assert.ThrowsAsync<ValidationException>(() => authService.UpdateAccount(user.Id, userWithFirstNameEmpty));

				var userWithInvalidFirstName = new AccountDtoBuilder().WithFirstName(UserTestConstants.InvalidFirstName).Build();
				await Assert.ThrowsAsync<ValidationException>(() => authService.UpdateAccount(user.Id, userWithInvalidFirstName));

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

				var authService = new AuthService(context, MockEmailService.Object, HashService.Object, ErrorMessages.Object);

				var userWithLastNameNull = new AccountDtoBuilder().WithLastName(null).Build();
				await Assert.ThrowsAsync<ValidationException>(() => authService.UpdateAccount(user.Id, userWithLastNameNull));

				var userWithLastNameEmpty = new AccountDtoBuilder().WithLastName(string.Empty).Build();
				await Assert.ThrowsAsync<ValidationException>(() => authService.UpdateAccount(user.Id, userWithLastNameEmpty));

				var userWithInvalidLastName = new AccountDtoBuilder().WithLastName(UserTestConstants.InvalidLastName).Build();
				await Assert.ThrowsAsync<ValidationException>(() => authService.UpdateAccount(user.Id, userWithInvalidLastName));

				context.Database.EnsureDeleted();
			}
		}

		[Fact]
		public async Task UpdateUser_NotFound_ThrowsNotFoundException()
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				var authService = new AuthService(context, MockEmailService.Object, HashService.Object, ErrorMessages.Object);

				var account = new AccountDtoBuilder().Build();
				await Assert.ThrowsAsync<NotFoundException>(() => authService.UpdateAccount(Guid.Empty, account));

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

				var authService = new AuthService(context, MockEmailService.Object, HashService.Object, ErrorMessages.Object);

				var userToUpdate = new AccountDtoBuilder().Build();
				await authService.UpdateAccount(user.Id, userToUpdate);

				user.FirstName.Equals(userToUpdate.FirstName).Should().BeTrue();
				user.LastName.Equals(userToUpdate.LastName).Should().BeTrue();

				context.Database.EnsureDeleted();
			}
		}
	}
}