using FluentAssertions;
using Xunit;

using System;
using System.Threading.Tasks;
using ApartmentRentalWebApi.Business.Core.Exceptions;
using ApartmentRentalWebApi.Business.Impl.Services;
using ApartmentRentalWebApi.Data;
using ApartmentRentalWebApi.TestModelBuilders.Builders;

namespace ApartmentRentalWebApi.Business.Tests.Services.Auth
{
	public class GetPasswordTokenValidityTests : AuthServiceTest
	{
		[Fact]
		public async Task GetPasswordTokenValidity_NotFoundToken_ThrowsNotFoundException()
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				var user = new UserBuilder()
					.WithEmailConfirmed(true)
					.WithPasswordResetToken(Guid.NewGuid().ToString()).Build();
				await context.Users.AddAsync(user);
				await context.SaveChangesAsync();

				var authService = new AuthService(context, MockEmailService.Object, HashService.Object, ErrorMessages.Object);

				await Assert.ThrowsAsync<NotFoundException>(() => authService.GetPasswordTokenValidity(string.Empty));

				context.Database.EnsureDeleted();
			}
		}

		[Fact]
		public async Task GetPasswordTokenValidity_NotEmailConfirmedUser_ThrowsNotFoundException()
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				var user = new UserBuilder()
					.WithEmailConfirmed(false)
					.WithPasswordResetToken(Guid.NewGuid().ToString()).Build();
				await context.Users.AddAsync(user);
				await context.SaveChangesAsync();

				var authService = new AuthService(context, MockEmailService.Object, HashService.Object, ErrorMessages.Object);

				await Assert.ThrowsAsync<NotFoundException>(() => authService.GetPasswordTokenValidity(user.PasswordResetToken));

				context.Database.EnsureDeleted();
			}
		}

		[Fact]
		public async Task GetPasswordTokenValidity_TokenExpired_Success()
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				var user = new UserBuilder()
					.WithEmailConfirmed(true)
					.WithPasswordResetToken(Guid.NewGuid().ToString())
					.WithPasswordResetEndDate(DateTime.Now.AddHours(-2))
					.Build();
				await context.Users.AddAsync(user);
				await context.SaveChangesAsync();

				var authService = new AuthService(context, MockEmailService.Object, HashService.Object, ErrorMessages.Object);
				var response = await authService.GetPasswordTokenValidity(user.PasswordResetToken);
				response.Valid.Should().BeFalse();

				context.Database.EnsureDeleted();
			}
		}

		[Fact]
		public async Task GetPasswordTokenValidity_TokenValid_Success()
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				var user = new UserBuilder()
					.WithEmailConfirmed(true)
					.WithPasswordResetToken(Guid.NewGuid().ToString())
					.WithPasswordResetEndDate(DateTime.Now.AddHours(2))
					.Build();
				await context.Users.AddAsync(user);
				await context.SaveChangesAsync();

				var authService = new AuthService(context, MockEmailService.Object, HashService.Object, ErrorMessages.Object);
				var response = await authService.GetPasswordTokenValidity(user.PasswordResetToken);
				response.Valid.Should().BeTrue();

				context.Database.EnsureDeleted();
			}
		}
	}
}