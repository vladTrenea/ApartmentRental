using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

using ApartmentRentalWebApi.Business.Core.Dto;
using ApartmentRentalWebApi.Business.Core.Enums;
using ApartmentRentalWebApi.Business.Impl.Services;
using ApartmentRentalWebApi.Data;
using ApartmentRentalWebApi.TestModelBuilders.Builders;

namespace ApartmentRentalWebApi.Business.Tests.Services.User
{
	public class GetUsersTests : UserServiceTest
	{
		[Fact]
		public async Task GetUsers_EmptyResult()
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				var userService = new UserService(context, MockEmailService.Object, ErrorMessages.Object);

				var result = await userService.GetUsers();
				result.Should().NotBeNull();
				result.Should().BeAssignableTo<IEnumerable<UserDto>>();
				result.Count().Should().Be(0);
				context.Database.EnsureDeleted();
			}
		}

		[Fact]
		public async Task GetUsers_Success()
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				await context.Roles.AddAsync(new RoleBuilder().WithRole(RoleEnum.Client).Build());
				await context.Roles.AddAsync(new RoleBuilder().WithRole(RoleEnum.Realtor).Build());
				await context.Roles.AddAsync(new RoleBuilder().WithRole(RoleEnum.Admin).Build());
				await context.Users.AddAsync(new UserBuilder().WithRole(RoleEnum.Client).Build());
				await context.Users.AddAsync(new UserBuilder().WithRole(RoleEnum.Realtor).Build());
				await context.Users.AddAsync(new UserBuilder().WithRole(RoleEnum.Admin).Build());
				await context.SaveChangesAsync();

				var userService = new UserService(context, MockEmailService.Object, ErrorMessages.Object);
				var result = await userService.GetUsers();
				result.Should().NotBeNull();
				result.Should().BeAssignableTo<IEnumerable<UserDto>>();
				result.Count().Should().Be(2);
				context.Database.EnsureDeleted();
			}
		}
	}
}