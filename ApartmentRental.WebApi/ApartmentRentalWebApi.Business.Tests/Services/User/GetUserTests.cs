using System;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

using ApartmentRentalWebApi.Business.Core.Dto;
using ApartmentRentalWebApi.Business.Core.Enums;
using ApartmentRentalWebApi.Business.Core.Exceptions;
using ApartmentRentalWebApi.Business.Impl.Services;
using ApartmentRentalWebApi.Data;
using ApartmentRentalWebApi.TestModelBuilders.Builders;

namespace ApartmentRentalWebApi.Business.Tests.Services.User
{
	public class GetUserTests : UserServiceTest
	{
		[Fact]
		public async Task GetUserById_NotAdmin_Success()
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				await context.Roles.AddAsync(new RoleBuilder().WithRole(RoleEnum.Client).Build());
				var user = new UserBuilder().WithRole(RoleEnum.Client).Build();
				await context.Users.AddAsync(user);
				await context.SaveChangesAsync();

				var userService = new UserService(context, MockEmailService.Object, ErrorMessages.Object);
				var result = await userService.GetById(user.Id);
				result.Should().NotBeNull();
				result.Should().BeAssignableTo<UserDto>();
				result.Id.Equals(user.Id).Should().BeTrue();
				result.Email.Equals(user.Email).Should().BeTrue();
				result.FirstName.Equals(user.FirstName).Should().BeTrue();
				result.LastName.Equals(user.LastName).Should().BeTrue();
				user.RoleId.Equals((int)result.RoleId).Should().BeTrue();
				context.Database.EnsureDeleted();
			}
		}

		[Fact]
		public async Task GetUserById_NoUser_ThrowsNotFoundException()
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				var userService = new UserService(context, MockEmailService.Object, ErrorMessages.Object);
				await Assert.ThrowsAsync<NotFoundException>(() => userService.GetById(Guid.Empty));
				context.Database.EnsureDeleted();
			}
		}

		[Fact]
		public async Task GetUserById_Admin_ThrowsNotFoundException()
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				var user = new UserBuilder().WithRole(RoleEnum.Admin).Build();

				await context.Users.AddAsync(user);
				await context.SaveChangesAsync();

				var userService = new UserService(context, MockEmailService.Object, ErrorMessages.Object);
				await Assert.ThrowsAsync<NotFoundException>(() => userService.GetById(user.Id));
				context.Database.EnsureDeleted();
			}
		}
	}
}
