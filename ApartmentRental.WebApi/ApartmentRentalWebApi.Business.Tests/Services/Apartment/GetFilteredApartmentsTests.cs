using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

using System.Threading.Tasks;
using ApartmentRentalWebApi.Business.Core.Dto;
using ApartmentRentalWebApi.Business.Impl.Services;
using ApartmentRentalWebApi.Data;
using ApartmentRentalWebApi.TestModelBuilders.Builders;

namespace ApartmentRentalWebApi.Business.Tests.Services.Apartment
{
	public class GetFilteredApartmentsTests : ApartmentServiceTest
	{
		public async Task GetFilteredApartments_Success()
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				var apartment1 = new ApartmentBuilder()
					.WithLatitude(45)
					.WithLongitude(32)
					.WithPricePerMonth(20)
					.WithArea(100)
					.WithNrOfRooms(3)
					.Build();
				var apartment2 = new ApartmentBuilder()
					.WithLatitude(45.5)
					.WithLongitude(32.5)
					.WithPricePerMonth(30)
					.WithArea(150)
					.WithNrOfRooms(4)
					.Build();
				var apartment3 = new ApartmentBuilder()
					.WithLatitude(45.75)
					.WithLongitude(32.75)
					.WithPricePerMonth(40)
					.WithArea(200)
					.WithNrOfRooms(5)
					.Build();
				var apartment4 = new ApartmentBuilder()
					.WithLatitude(46)
					.WithLongitude(33)
					.WithPricePerMonth(50)
					.WithArea(250)
					.WithNrOfRooms(6)
					.Build();
				await context.Apartments.AddAsync(apartment1);
				await context.Apartments.AddAsync(apartment2);
				await context.Apartments.AddAsync(apartment3);
				await context.Apartments.AddAsync(apartment4);
				await context.SaveChangesAsync();

				var apartmentService = new ApartmentService(context, ErrorMessages.Object);

				var filter = new ApartmentFilterDtoBuilder()
					.WithMinNrOfRooms(4)
					.WithMaxArea(230)
					.WithMaxPrice(50)
					.WithNorthEastLatitude(56)
					.WithNorthEastLongitude(43)
					.WithSouthWestLatitude(31)
					.WithSouthWestLongitude(7)
					.Build();
				var result = await apartmentService.GetFiltered(filter);
				result.Should().NotBeNull();
				result.Should().BeAssignableTo<IEnumerable<ApartmentDto>>();
				result.Count().Should().Be(2);

				context.Database.EnsureDeleted();
			}
		}
	}
}