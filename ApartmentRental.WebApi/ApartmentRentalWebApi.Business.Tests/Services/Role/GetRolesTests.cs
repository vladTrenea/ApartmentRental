using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApartmentRentalWebApi.Data;
using FluentAssertions;
using Xunit;

using ApartmentRentalWebApi.Business.Core.Dto;
using ApartmentRentalWebApi.Business.Core.Enums;
using ApartmentRentalWebApi.Business.Impl.Services;
using ApartmentRentalWebApi.TestModelBuilders.Builders;

namespace ApartmentRentalWebApi.Business.Tests.Services.Role
{
	public class GetRolesTests : RoleServiceTest
	{
		[Fact]
		public async Task GetRoles_EmptyResult()
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				var roleService = new RoleService(context);

				var result = await roleService.GetRoles();
				result.Should().NotBeNull();
				result.Should().BeAssignableTo<IEnumerable<RoleDto>>();
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
				await context.SaveChangesAsync();

				var roleService = new RoleService(context);

				var result = await roleService.GetRoles();
				result.Should().NotBeNull();
				result.Should().BeAssignableTo<IEnumerable<RoleDto>>();
				result.Count().Should().Be(2);
				context.Database.EnsureDeleted();
			}
		}
	}
}