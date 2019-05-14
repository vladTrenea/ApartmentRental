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

namespace ApartmentRentalWebApi.Business.Tests.Services.Apartment
{
	public class GetApartmentTests : ApartmentServiceTest
	{
		[Fact]
		public async Task GetApartmentById_NotFound_ThrowsNotFoundException()
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				var apartmentService = new ApartmentService(context, ErrorMessages.Object);
				await Assert.ThrowsAsync<NotFoundException>(() => apartmentService.GetById(Guid.Empty));
				context.Database.EnsureDeleted();
			}
		}

		[Fact]
		public async Task GetApartmentById_Success()
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				await context.Roles.AddAsync(new RoleBuilder().WithRole(RoleEnum.Realtor).Build());
				var user = new UserBuilder().WithRole(RoleEnum.Realtor).Build();
				await context.Users.AddAsync(user);
				await context.SaveChangesAsync();

				var apartment = new ApartmentBuilder().WithRealtorId(user.Id).Build();
				await context.Apartments.AddAsync(apartment);
				await context.SaveChangesAsync();

				var apartmentService = new ApartmentService(context, ErrorMessages.Object);
				var result = await apartmentService.GetById(apartment.Id);
				result.Should().NotBeNull();
				result.Should().BeAssignableTo<ApartmentDto>();
				result.Id.Equals(apartment.Id).Should().BeTrue();
				result.Title.Equals(apartment.Title).Should().BeTrue();
				result.Description.Equals(apartment.Description).Should().BeTrue();
				result.Area.Equals(apartment.Area).Should().BeTrue();
				result.NrOfRooms.Equals(apartment.NrOfRooms).Should().BeTrue();
				result.PricePerMonth.Equals(apartment.PricePerMonth).Should().BeTrue();
				result.Latitude.Equals(apartment.Latitude).Should().BeTrue();
				result.Longitude.Equals(apartment.Longitude).Should().BeTrue();
				result.IsRented.Equals(apartment.IsRented).Should().BeTrue();
				context.Database.EnsureDeleted();
			}
		}
	}
}