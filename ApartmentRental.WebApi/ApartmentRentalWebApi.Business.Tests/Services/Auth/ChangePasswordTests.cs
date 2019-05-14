using System;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

using ApartmentRentalWebApi.Business.Core.Exceptions;
using ApartmentRentalWebApi.Business.Impl.Services;
using ApartmentRentalWebApi.Data;
using ApartmentRentalWebApi.TestModelBuilders.Builders;
using ApartmentRentalWebApi.TestModelBuilders.Constants;

namespace ApartmentRentalWebApi.Business.Tests.Services.Auth
{
	public class ChangePasswordTests : AuthServiceTest
	{
		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData("asdas")]
		[InlineData("12312")]
		[InlineData("ab123")]
		[InlineData("AA")]
		[InlineData("Test123")]
		public async Task ChangePassword_InvalidPassword_ThrowsValidationException(string value)
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				var authService = new AuthService(context, MockEmailService.Object, HashService.Object, ErrorMessages.Object);

				var registration = new PasswordChangeDtoBuilder().WithPassword(value).Build();
				await Assert.ThrowsAsync<ValidationException>(() => authService.ChangePassword(string.Empty, registration));

				context.Database.EnsureDeleted();
			}
		}

		[Fact]
		public async Task ChangePassword_InvalidConfirmPassword_ThrowsValidationException()
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				var authService = new AuthService(context, MockEmailService.Object, HashService.Object, ErrorMessages.Object);

				var changePasswordDtoWithConfirmPasswordNull = new PasswordChangeDtoBuilder().WithConfirmPassword(null).Build();
				await Assert.ThrowsAsync<ValidationException>(() => authService.ChangePassword(Guid.NewGuid().ToString(), changePasswordDtoWithConfirmPasswordNull));

				var changePasswordDtoWithConfirmPasswordEmpty = new PasswordChangeDtoBuilder().WithConfirmPassword(string.Empty).Build();
				await Assert.ThrowsAsync<ValidationException>(() => authService.ChangePassword(Guid.NewGuid().ToString(), changePasswordDtoWithConfirmPasswordEmpty));

				var changePasswordWithMismatchingPasswords = new PasswordChangeDtoBuilder()
					.WithConfirmPassword(UserTestConstants.ValidPassword + 'a')
					.Build();
				await Assert.ThrowsAsync<ValidationException>(() => authService.ChangePassword(Guid.NewGuid().ToString(), changePasswordWithMismatchingPasswords));

				context.Database.EnsureDeleted();
			}
		}

		[Fact]
		public async Task ChangePassword_InvalidPasswordResetToken_ThrowsNotFoundException()
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				var authService = new AuthService(context, MockEmailService.Object, HashService.Object, ErrorMessages.Object);

				var passwordChangeDto = new PasswordChangeDtoBuilder().Build();
				await Assert.ThrowsAsync<NotFoundException>(() => authService.ChangePassword(Guid.Empty.ToString(), passwordChangeDto));

				context.Database.EnsureDeleted();
			}
		}

		[Fact]
		public async Task ChangePassword_ExpiredPasswordToken_ThrowsConflictException()
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				var user = new UserBuilder()
					.WithPasswordResetToken(Guid.NewGuid().ToString())
					.WithPasswordResetEndDate(DateTime.Now.AddHours(-1))
					.Build();
				await context.Users.AddAsync(user);
				await context.SaveChangesAsync();

				var authService = new AuthService(context, MockEmailService.Object, HashService.Object, ErrorMessages.Object);

				var passwordChangeDto = new PasswordChangeDtoBuilder().Build();
				await Assert.ThrowsAsync<ConflictException>(() => authService.ChangePassword(user.PasswordResetToken, passwordChangeDto));

				context.Database.EnsureDeleted();
			}
		}

		[Fact]
		public async Task ChangePassword_Success()
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				var user = new UserBuilder()
					.WithPasswordResetToken(Guid.NewGuid().ToString())
					.WithPasswordResetEndDate(DateTime.Now.AddHours(1))
					.Build();
				await context.Users.AddAsync(user);
				await context.SaveChangesAsync();

				var authService = new AuthService(context, MockEmailService.Object, HashService.Object, ErrorMessages.Object);

				var passwordChangeDto = new PasswordChangeDtoBuilder().Build();
				await authService.ChangePassword(user.PasswordResetToken, passwordChangeDto);

				var dbUser = await context.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
				HashService.Object.EncodeString(dbUser.Password).Equals(user.Password).Should().BeTrue();
				dbUser.PasswordResetToken.Should().BeNull();
				dbUser.PasswordResetEndDate.Should().BeNull();

				context.Database.EnsureDeleted();
			}
		}
	}
}