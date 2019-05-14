using System;
using System.Linq;
using System.Threading.Tasks;
using ApartmentRentalWebApi.Business.Core.Enums;
using Xunit;

using ApartmentRentalWebApi.Business.Core.Exceptions;
using ApartmentRentalWebApi.Business.Impl.Services;
using ApartmentRentalWebApi.Data;
using ApartmentRentalWebApi.TestModelBuilders.Builders;
using FluentAssertions;

namespace ApartmentRentalWebApi.Business.Tests.Services.Apartment
{
	public class DeleteApartmentTests : ApartmentServiceTest
	{
		[Fact]
		public async Task DeleteApartment_NotFound_ThrowsNotFoundException()
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				var apartmentService = new ApartmentService(context, ErrorMessages.Object);
				await Assert.ThrowsAsync<NotFoundException>(() => apartmentService.Delete(Guid.Empty));
				context.Database.EnsureDeleted();
			}
		}

		[Fact]
		public async Task DeleteApartment_Success()
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				var user = new UserBuilder().WithRole(RoleEnum.Realtor).Build();
				await context.Users.AddAsync(user);
				var apartment = new ApartmentBuilder().WithRealtorId(user.Id).Build();
				await context.Apartments.AddAsync(apartment);
				await context.SaveChangesAsync();

				var apartmentService = new ApartmentService(context, ErrorMessages.Object);
				await apartmentService.Delete(apartment.Id);
				context.Apartments.Count().Equals(0).Should().BeTrue();

				context.Database.EnsureDeleted();
			}
		}
	}
}