using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

using ApartmentRentalWebApi.Business.Core.Exceptions;
using ApartmentRentalWebApi.Business.Impl.Services;
using ApartmentRentalWebApi.Data;
using ApartmentRentalWebApi.TestModelBuilders.Builders;
using ApartmentRentalWebApi.TestModelBuilders.Constants;

namespace ApartmentRentalWebApi.Business.Tests.Services.Auth
{
	public class LoginTests : AuthServiceTest
	{
		[Theory]
		[InlineData(null)]
		[InlineData("")]
		public async Task Login_InvalidUsername_ThrowsValidationException(string value)
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				var authService = new AuthService(context, MockEmailService.Object, HashService.Object, ErrorMessages.Object);

				var login = new LoginBuilder().WithEmail(value).Build();
				await Assert.ThrowsAsync<ValidationException>(() => authService.Login(login));
			}
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		public async Task Login_InvalidPassword_ThrowsValidationException(string value)
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				var authService = new AuthService(context, MockEmailService.Object, HashService.Object, ErrorMessages.Object);

				var login = new LoginBuilder().WithPassword(value).Build();
				await Assert.ThrowsAsync<ValidationException>(() => authService.Login(login));
			}
		}

		[Fact]
		public async Task Login_UserNotFound_ThrowsUnauthorizedException()
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				var authService = new AuthService(context, MockEmailService.Object, HashService.Object, ErrorMessages.Object);

				var login = new LoginBuilder().Build();
				await Assert.ThrowsAsync<UnauthorizedException>(() => authService.Login(login));
			}
		}

		[Fact]
		public async Task Login_UserNotConfirmed_ThrowsUnauthorizedException()
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				var user = new UserBuilder()
					.WithPassword(HashService.Object.EncodeString(UserTestConstants.ValidPassword))
					.WithEmailConfirmed(false)
					.Build();
				await context.Users.AddAsync(user);
				await context.SaveChangesAsync();

				var authService = new AuthService(context, MockEmailService.Object, HashService.Object, ErrorMessages.Object);

				var login = new LoginBuilder().WithEmail(user.Email).WithPassword(UserTestConstants.ValidPassword).Build();
				await Assert.ThrowsAsync<UnauthorizedException>(() => authService.Login(login));
			}
		}

		[Fact]
		public async Task Login_Success()
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				var user = new UserBuilder().WithPassword(HashService.Object.EncodeString(UserTestConstants.ValidPassword)).Build();
				await context.Users.AddAsync(user);
				await context.SaveChangesAsync();

				var authService = new AuthService(context, MockEmailService.Object, HashService.Object, ErrorMessages.Object);

				var login = new LoginBuilder().WithEmail(user.Email).WithPassword(UserTestConstants.ValidPassword).Build();
				var result = await authService.Login(login);
				result.UserId.Equals(user.Id).Should().BeTrue();
				result.Email.Equals(user.Email).Should().BeTrue();
				result.FirstName.Equals(user.FirstName).Should().BeTrue();
				result.LastName.Equals(user.LastName).Should().BeTrue();
				user.RoleId.Equals((int)result.RoleId).Should().BeTrue();

				context.Database.EnsureDeleted();
			}
		}
	}
}