using System.Linq;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

using ApartmentRentalWebApi.Business.Core.Enums;
using ApartmentRentalWebApi.Business.Core.Exceptions;
using ApartmentRentalWebApi.Business.Impl.Services;
using ApartmentRentalWebApi.Data;
using ApartmentRentalWebApi.TestModelBuilders.Builders;
using ApartmentRentalWebApi.TestModelBuilders.Constants;

namespace ApartmentRentalWebApi.Business.Tests.Services.User
{
	public class AddUserTests : UserServiceTest
	{
		[Fact]
		public async Task AddUser_InvalidEmail_ThrowsValidationException()
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				var userService = new UserService(context, MockEmailService.Object, ErrorMessages.Object);

				var userWithEmailNull = new UserAddDtoBuilder().WithEmail(null).Build();
				await Assert.ThrowsAsync<ValidationException>(() => userService.AddUser(userWithEmailNull));

				var userWithEmailEmpty = new UserAddDtoBuilder().WithEmail(string.Empty).Build();
				await Assert.ThrowsAsync<ValidationException>(() => userService.AddUser(userWithEmailEmpty));

				var userWithInvalidEmail = new UserAddDtoBuilder().WithEmail(UserTestConstants.InvalidEmail).Build();
				await Assert.ThrowsAsync<ValidationException>(() => userService.AddUser(userWithInvalidEmail));

				context.Database.EnsureDeleted();
			}
		}

		[Fact]
		public async Task AddUser_InvalidFirstName_ThrowsValidationException()
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				var userService = new UserService(context, MockEmailService.Object, ErrorMessages.Object);

				var userWithFirstNameNull = new UserAddDtoBuilder().WithFirstName(null).Build();
				await Assert.ThrowsAsync<ValidationException>(() => userService.AddUser(userWithFirstNameNull));

				var userWithFirstNameEmpty = new UserAddDtoBuilder().WithFirstName(string.Empty).Build();
				await Assert.ThrowsAsync<ValidationException>(() => userService.AddUser(userWithFirstNameEmpty));

				var userWithInvalidFirstName = new UserAddDtoBuilder().WithFirstName(UserTestConstants.InvalidFirstName).Build();
				await Assert.ThrowsAsync<ValidationException>(() => userService.AddUser(userWithInvalidFirstName));

				context.Database.EnsureDeleted();
			}
		}

		[Fact]
		public async Task AddUser_InvalidLastName_ThrowsValidationException()
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				var userService = new UserService(context, MockEmailService.Object, ErrorMessages.Object);

				var userWithLastNameNull = new UserAddDtoBuilder().WithLastName(null).Build();
				await Assert.ThrowsAsync<ValidationException>(() => userService.AddUser(userWithLastNameNull));

				var userWithLastNameEmpty = new UserAddDtoBuilder().WithLastName(string.Empty).Build();
				await Assert.ThrowsAsync<ValidationException>(() => userService.AddUser(userWithLastNameEmpty));

				var userWithInvalidLastName = new UserAddDtoBuilder().WithLastName(UserTestConstants.InvalidLastName).Build();
				await Assert.ThrowsAsync<ValidationException>(() => userService.AddUser(userWithInvalidLastName));

				context.Database.EnsureDeleted();
			}
		}

		[Fact]
		public async Task AddUser_InvalidRole_ThrowsValidationException()
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				var userService = new UserService(context, MockEmailService.Object, ErrorMessages.Object);

				var userWithRoleNull = new UserAddDtoBuilder().WithRoleId(null).Build();
				await Assert.ThrowsAsync<ValidationException>(() => userService.AddUser(userWithRoleNull));

				var userWithRoleAdmin = new UserAddDtoBuilder().WithRoleId(RoleEnum.Admin).Build();
				await Assert.ThrowsAsync<ValidationException>(() => userService.AddUser(userWithRoleAdmin));

				context.Database.EnsureDeleted();
			}
		}

		[Fact]
		public async Task AddUser_EmailAlreadyExists_ThrowsConflictException()
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				var user = new UserBuilder().Build();
				await context.Users.AddAsync(user);
				await context.SaveChangesAsync();

				var userService = new UserService(context, MockEmailService.Object, ErrorMessages.Object);

				var userToAdd = new UserAddDtoBuilder().WithEmail(user.Email).Build();
				await Assert.ThrowsAsync<ConflictException>(() => userService.AddUser(userToAdd));

				context.Database.EnsureDeleted();
			}
		}

		[Fact]
		public async Task AddUser_Success()
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				var userService = new UserService(context, MockEmailService.Object, ErrorMessages.Object);
				var userToAdd = new UserAddDtoBuilder().Build();
				await userService.AddUser(userToAdd);

				context.Users.Count().Equals(1).Should().BeTrue();
				var user = context.Users.FirstOrDefault();
				user.FirstName.Equals(userToAdd.FirstName).Should().BeTrue();
				user.LastName.Equals(userToAdd.LastName).Should().BeTrue();
				user.RoleId.Equals((int)userToAdd.RoleId.Value).Should().BeTrue();

				context.Database.EnsureDeleted();
			}
		}
	}
}