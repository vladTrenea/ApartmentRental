using System.Threading.Tasks;
using Xunit;

using ApartmentRentalWebApi.Business.Core.Exceptions;
using ApartmentRentalWebApi.Business.Impl.Services;
using ApartmentRentalWebApi.Data;
using ApartmentRentalWebApi.TestModelBuilders.Builders;
using ApartmentRentalWebApi.TestModelBuilders.Constants;
using Castle.Core.Internal;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace ApartmentRentalWebApi.Business.Tests.Services.Auth
{
	public class RegisterTests : AuthServiceTest
	{
		[Fact]
		public async Task Register_InvalidEmail_ThrowsValidationException()
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				var authService = new AuthService(context, MockEmailService.Object, HashService.Object, ErrorMessages.Object);

				var userWithEmailNull = new UserRegistrationDtoBuilder().WithEmail(null).Build();
				await Assert.ThrowsAsync<ValidationException>(() => authService.Register(userWithEmailNull));

				var userWithEmailEmpty = new UserRegistrationDtoBuilder().WithEmail(string.Empty).Build();
				await Assert.ThrowsAsync<ValidationException>(() => authService.Register(userWithEmailEmpty));

				var userWithInvalidEmail = new UserRegistrationDtoBuilder().WithEmail(UserTestConstants.InvalidEmail).Build();
				await Assert.ThrowsAsync<ValidationException>(() => authService.Register(userWithInvalidEmail));

				context.Database.EnsureDeleted();
			}
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData("asdas")]
		[InlineData("12312")]
		[InlineData("ab123")]
		[InlineData("AA")]
		[InlineData("Test123")]
		public async Task Register_InvalidPassword_ThrowsValidationException(string value)
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				var authService = new AuthService(context, MockEmailService.Object, HashService.Object, ErrorMessages.Object);

				var registration = new UserRegistrationDtoBuilder().WithPassword(value).Build();
				await Assert.ThrowsAsync<ValidationException>(() => authService.Register(registration));

				context.Database.EnsureDeleted();
			}
		}

		[Fact]
		public async Task Register_InvalidConfirmPassword_ThrowsValidationException()
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				var authService = new AuthService(context, MockEmailService.Object, HashService.Object, ErrorMessages.Object);

				var userWithConfirmPasswordNull = new UserRegistrationDtoBuilder().WithConfirmPassword(null).Build();
				await Assert.ThrowsAsync<ValidationException>(() => authService.Register(userWithConfirmPasswordNull));

				var userWithConfirmPasswordEmpty = new UserRegistrationDtoBuilder().WithConfirmPassword(string.Empty).Build();
				await Assert.ThrowsAsync<ValidationException>(() => authService.Register(userWithConfirmPasswordEmpty));

				var userWithMismatchingPasswords = new UserRegistrationDtoBuilder()
					.WithConfirmPassword(UserTestConstants.ValidPassword + 'a')
					.Build();
				await Assert.ThrowsAsync<ValidationException>(() => authService.Register(userWithMismatchingPasswords));

				context.Database.EnsureDeleted();
			}
		}

		[Fact]
		public async Task Register_InvalidFirstName_ThrowsValidationException()
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				var authService = new AuthService(context, MockEmailService.Object, HashService.Object, ErrorMessages.Object);

				var userWithFirstNameNull = new UserRegistrationDtoBuilder().WithFirstName(null).Build();
				await Assert.ThrowsAsync<ValidationException>(() => authService.Register(userWithFirstNameNull));

				var userWithFirstNameEmpty = new UserRegistrationDtoBuilder().WithFirstName(string.Empty).Build();
				await Assert.ThrowsAsync<ValidationException>(() => authService.Register(userWithFirstNameEmpty));

				var userWithInvalidFirstName = new UserRegistrationDtoBuilder().WithFirstName(UserTestConstants.InvalidFirstName).Build();
				await Assert.ThrowsAsync<ValidationException>(() => authService.Register(userWithInvalidFirstName));

				context.Database.EnsureDeleted();
			}
		}

		[Fact]
		public async Task Register_InvalidLastName_ThrowsValidationException()
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				var authService = new AuthService(context, MockEmailService.Object, HashService.Object, ErrorMessages.Object);

				var userWithLastNameNull = new UserRegistrationDtoBuilder().WithFirstName(null).Build();
				await Assert.ThrowsAsync<ValidationException>(() => authService.Register(userWithLastNameNull));

				var userWithLastNameEmpty = new UserRegistrationDtoBuilder().WithFirstName(string.Empty).Build();
				await Assert.ThrowsAsync<ValidationException>(() => authService.Register(userWithLastNameEmpty));

				var userWithInvalidLastName = new UserRegistrationDtoBuilder().WithFirstName(UserTestConstants.InvalidLastName).Build();
				await Assert.ThrowsAsync<ValidationException>(() => authService.Register(userWithInvalidLastName));

				context.Database.EnsureDeleted();
			}
		}

		[Fact]
		public async Task Register_DuplicateEmail_ThrowsConflictException()
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				var user = new UserBuilder().WithEmail(UserTestConstants.ValidEmail).Build();
				await context.Users.AddAsync(user);
				await context.SaveChangesAsync();

				var authService = new AuthService(context, MockEmailService.Object, HashService.Object, ErrorMessages.Object);

				var registration = new UserRegistrationDtoBuilder().WithEmail(user.Email).Build();
				await Assert.ThrowsAsync<ConflictException>(() => authService.Register(registration));

				context.Database.EnsureDeleted();
			}
		}

		[Fact]
		public async Task Register_Success()
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				var authService = new AuthService(context, MockEmailService.Object, HashService.Object, ErrorMessages.Object);
				var registration = new UserRegistrationDtoBuilder().Build();

				await authService.Register(registration);
				var user = await context.Users.FirstOrDefaultAsync(u => u.Email == registration.Email);
				user.Email.Equals(registration.Email).Should().BeTrue();
				user.FirstName.Equals(registration.FirstName).Should().BeTrue();
				user.LastName.Equals(registration.LastName).Should().BeTrue();
				user.Password.Equals(HashService.Object.EncodeString(registration.Password)).Should().BeTrue();
				user.EmailConfirmationToken.IsNullOrEmpty().Should().BeFalse();
				user.EmailConfirmed.Equals(false).Should().BeTrue();
			}
		}
	}
}