using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

using ApartmentRentalWebApi.Business.Core.Exceptions;
using ApartmentRentalWebApi.Business.Impl.Services;
using ApartmentRentalWebApi.Data;
using ApartmentRentalWebApi.TestModelBuilders.Builders;

namespace ApartmentRentalWebApi.Business.Tests.Services.Auth
{
	public class ResetPasswordTests: AuthServiceTest
	{
		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData("asdasdas@")]
		[InlineData("asdasdas@sdadas")]
		public async Task ResetPassword_InvalidEmail_ThrowsValidationException(string value)
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				var authService = new AuthService(context, MockEmailService.Object, HashService.Object, ErrorMessages.Object);

				var resetPasswordDto = new PasswordResetDtoBuilder().WithEmail(value).Build();
				await Assert.ThrowsAsync<ValidationException>(() => authService.ResetPassword(resetPasswordDto));
			}
		}

		[Fact]
		public async Task ResetPassword_EmailNotFound_ThrowsNotFoundException()
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				var authService = new AuthService(context, MockEmailService.Object, HashService.Object, ErrorMessages.Object);

				var resetPasswordDto = new PasswordResetDtoBuilder().Build();
				await Assert.ThrowsAsync<NotFoundException>(() => authService.ResetPassword(resetPasswordDto));
			}
		}

		[Fact]
		public async Task ResetPassword_EmailNotConfirmed_ThrowsNotFoundException()
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				var user = new UserBuilder()
					.WithEmailConfirmed(false)
					.Build();
				await context.Users.AddAsync(user);
				await context.SaveChangesAsync();

				var authService = new AuthService(context, MockEmailService.Object, HashService.Object, ErrorMessages.Object);

				var resetPasswordDto = new PasswordResetDtoBuilder().WithEmail(user.Email).Build();
				await Assert.ThrowsAsync<NotFoundException>(() => authService.ResetPassword(resetPasswordDto));
			}
		}

		[Fact]
		public async Task ResetPassword_Success()
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				var user = new UserBuilder()
					.WithEmailConfirmed(true)
					.Build();
				await context.Users.AddAsync(user);
				await context.SaveChangesAsync();

				var authService = new AuthService(context, MockEmailService.Object, HashService.Object, ErrorMessages.Object);

				var resetPasswordDto = new PasswordResetDtoBuilder().WithEmail(user.Email).Build();
				await authService.ResetPassword(resetPasswordDto);

				var dbUser = await context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
				dbUser.Email.Equals(user.Email).Should().BeTrue();
				dbUser.PasswordResetToken.Should().NotBeNull();
				dbUser.PasswordResetEndDate.Should().NotBeNull();
			}
		}
	}
}