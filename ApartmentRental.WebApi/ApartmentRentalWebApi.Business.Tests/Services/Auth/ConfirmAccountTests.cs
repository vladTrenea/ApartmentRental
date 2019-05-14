using System;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

using ApartmentRentalWebApi.Business.Core.Exceptions;
using ApartmentRentalWebApi.Business.Impl.Services;
using ApartmentRentalWebApi.Data;
using ApartmentRentalWebApi.TestModelBuilders.Builders;

namespace ApartmentRentalWebApi.Business.Tests.Services.Auth
{
	public class ConfirmAccountTests : AuthServiceTest
	{
		[Fact]
		public async Task ConfirmAccount_InvalidEmailToken_ThrowsNotFoundException()
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				var authService = new AuthService(context, MockEmailService.Object, HashService.Object, ErrorMessages.Object);

				var emailToken = Guid.Empty.ToString();
				await Assert.ThrowsAsync<NotFoundException>(() => authService.ConfirmAccount(emailToken));

				context.Database.EnsureDeleted();
			}
		}

		[Fact]
		public async Task ConfirmAccount_Success()
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				var user = new UserBuilder().WithEmailConfirmationToken(Guid.NewGuid().ToString()).Build();
				await context.Users.AddAsync(user);
				await context.SaveChangesAsync();

				var authService = new AuthService(context, MockEmailService.Object, HashService.Object, ErrorMessages.Object);
				await authService.ConfirmAccount(user.EmailConfirmationToken);

				var dbUser = await context.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
				dbUser.EmailConfirmationToken.Should().BeNull();
				dbUser.EmailConfirmed.Should().BeTrue();
				dbUser.ConfirmedAt.HasValue.Should().BeTrue();

				context.Database.EnsureDeleted();
			}
		}
	}
}