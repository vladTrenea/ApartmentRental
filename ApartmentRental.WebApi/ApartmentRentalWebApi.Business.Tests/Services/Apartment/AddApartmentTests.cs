using System;
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

namespace ApartmentRentalWebApi.Business.Tests.Services.Apartment
{
	public class AddApartmentTests : ApartmentServiceTest
	{
		[Fact]
		public async Task AddApartment_InvalidTitle_ThrowsValidationException()
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				var apartmentService = new ApartmentService(context, ErrorMessages.Object);

				var apartmentWithTitleNull = new ApartmentAddUpdateDtoBuilder().WithTitle(null).Build();
				await Assert.ThrowsAsync<ValidationException>(() => apartmentService.Add(apartmentWithTitleNull));

				var apartmentWithTitleEmpty = new ApartmentAddUpdateDtoBuilder().WithTitle(string.Empty).Build();
				await Assert.ThrowsAsync<ValidationException>(() => apartmentService.Add(apartmentWithTitleEmpty));

				context.Database.EnsureDeleted();
			}
		}

		[Fact]
		public async Task AddApartment_InvalidDescription_ThrowsValidationException()
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				var apartmentService = new ApartmentService(context, ErrorMessages.Object);

				var apartmentWithDescriptionNull = new ApartmentAddUpdateDtoBuilder().WithDescription(null).Build();
				await Assert.ThrowsAsync<ValidationException>(() => apartmentService.Add(apartmentWithDescriptionNull));

				var apartmentWithDescriptionEmpty = new ApartmentAddUpdateDtoBuilder().WithDescription(string.Empty).Build();
				await Assert.ThrowsAsync<ValidationException>(() => apartmentService.Add(apartmentWithDescriptionEmpty));

				context.Database.EnsureDeleted();
			}
		}

		[Fact]
		public async Task AddApartment_InvalidArea_ThrowsValidationException()
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				var apartmentService = new ApartmentService(context, ErrorMessages.Object);

				var apartmentWithAreaZero = new ApartmentAddUpdateDtoBuilder().WithArea(0).Build();
				await Assert.ThrowsAsync<ValidationException>(() => apartmentService.Add(apartmentWithAreaZero));

				var apartmentWithAreaNegative = new ApartmentAddUpdateDtoBuilder().WithArea(-10).Build();
				await Assert.ThrowsAsync<ValidationException>(() => apartmentService.Add(apartmentWithAreaNegative));

				context.Database.EnsureDeleted();
			}
		}

		[Fact]
		public async Task AddApartment_InvalidPricePerMonth_ThrowsValidationException()
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				var apartmentService = new ApartmentService(context, ErrorMessages.Object);

				var apartmentWithPricePerMonthZero = new ApartmentAddUpdateDtoBuilder().WithPricePerMonth(0).Build();
				await Assert.ThrowsAsync<ValidationException>(() => apartmentService.Add(apartmentWithPricePerMonthZero));

				var apartmentWithPricePerMonthNegative = new ApartmentAddUpdateDtoBuilder().WithPricePerMonth(-10).Build();
				await Assert.ThrowsAsync<ValidationException>(() => apartmentService.Add(apartmentWithPricePerMonthNegative));

				context.Database.EnsureDeleted();
			}
		}

		[Fact]
		public async Task AddApartment_InvalidNrOfRooms_ThrowsValidationException()
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				var apartmentService = new ApartmentService(context, ErrorMessages.Object);

				var apartmentWithNrOfRoomsZero = new ApartmentAddUpdateDtoBuilder().WithNrOfRooms(0).Build();
				await Assert.ThrowsAsync<ValidationException>(() => apartmentService.Add(apartmentWithNrOfRoomsZero));

				var apartmentWithNrOfRoomsNegative = new ApartmentAddUpdateDtoBuilder().WithNrOfRooms(-10).Build();
				await Assert.ThrowsAsync<ValidationException>(() => apartmentService.Add(apartmentWithNrOfRoomsNegative));

				context.Database.EnsureDeleted();
			}
		}

		[Fact]
		public async Task AddApartment_InvalidLatitude_ThrowsValidationException()
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				var apartmentService = new ApartmentService(context, ErrorMessages.Object);

				var apartmentWithLatitudeGreaterThanMaxValue = new ApartmentAddUpdateDtoBuilder().WithLatitude(ApartmentTestConstants.InvalidLatitudeGreaterThanMaxValue).Build();
				await Assert.ThrowsAsync<ValidationException>(() => apartmentService.Add(apartmentWithLatitudeGreaterThanMaxValue));

				var apartmentWithLongitudeLessThenMinValue = new ApartmentAddUpdateDtoBuilder().WithLatitude(ApartmentTestConstants.InvalidLatitudeLessThanMinValue).Build();
				await Assert.ThrowsAsync<ValidationException>(() => apartmentService.Add(apartmentWithLongitudeLessThenMinValue));

				context.Database.EnsureDeleted();
			}
		}

		[Fact]
		public async Task AddApartment_InvalidLongitude_ThrowsValidationException()
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				var apartmentService = new ApartmentService(context, ErrorMessages.Object);

				var apartmentWithLatitudeGreaterThanMaxValue = new ApartmentAddUpdateDtoBuilder().WithLongitude(ApartmentTestConstants.InvalidLongitudeGreaterThanMaxValue).Build();
				await Assert.ThrowsAsync<ValidationException>(() => apartmentService.Add(apartmentWithLatitudeGreaterThanMaxValue));

				var apartmentWithLongitudeLessThenMinValue = new ApartmentAddUpdateDtoBuilder().WithLongitude(ApartmentTestConstants.InvalidLongitudeLessThanMinValue).Build();
				await Assert.ThrowsAsync<ValidationException>(() => apartmentService.Add(apartmentWithLongitudeLessThenMinValue));

				context.Database.EnsureDeleted();
			}
		}

		[Fact]
		public async Task AddApartment_InvalidRealtorId_ThrowsValidationException()
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				var apartmentService = new ApartmentService(context, ErrorMessages.Object);

				var apartmentWithNoRealtorId = new ApartmentAddUpdateDtoBuilder().WithRealtorId(null).Build();
				await Assert.ThrowsAsync<ValidationException>(() => apartmentService.Add(apartmentWithNoRealtorId));

				context.Database.EnsureDeleted();
			}
		}

		[Fact]
		public async Task AddApartment_InvalidRealtorIdNotFound_ThrowsNotFoundException()
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				var apartmentService = new ApartmentService(context, ErrorMessages.Object);

				var apartmentWithNotFoundRealtor = new ApartmentAddUpdateDtoBuilder().WithRealtorId(Guid.Empty).Build();
				await Assert.ThrowsAsync<NotFoundException>(() => apartmentService.Add(apartmentWithNotFoundRealtor));

				context.Database.EnsureDeleted();
			}
		}

		[Fact]
		public async Task AddApartment_InvalidRealtorIdNotRealtorRole_ThrowsNotFoundException()
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				var user = new UserBuilder().WithRole(RoleEnum.Client).Build();
				await context.Users.AddAsync(user);
				await context.SaveChangesAsync();

				var apartmentService = new ApartmentService(context, ErrorMessages.Object);

				var apartmentWithNotARealtorUser = new ApartmentAddUpdateDtoBuilder().WithRealtorId(user.Id).Build();
				await Assert.ThrowsAsync<NotFoundException>(() => apartmentService.Add(apartmentWithNotARealtorUser));

				context.Database.EnsureDeleted();
			}
		}

		[Fact]
		public async Task AddApartment_Success()
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				var user = new UserBuilder().WithRole(RoleEnum.Realtor).Build();
				await context.Users.AddAsync(user);
				await context.SaveChangesAsync();

				var apartmentService = new ApartmentService(context, ErrorMessages.Object);

				var apartmentToAdd = new ApartmentAddUpdateDtoBuilder().WithRealtorId(user.Id).Build();
				await apartmentService.Add(apartmentToAdd);
				context.Apartments.Count().Equals(1).Should().BeTrue();
				var apartment = context.Apartments.FirstOrDefault();
				apartment.Title.Equals(apartmentToAdd.Title).Should().BeTrue();
				apartment.Description.Equals(apartmentToAdd.Description).Should().BeTrue();
				apartment.Area.Equals(apartmentToAdd.Area).Should().BeTrue();
				apartment.NrOfRooms.Equals(apartmentToAdd.NrOfRooms).Should().BeTrue();
				apartment.PricePerMonth.Equals(apartmentToAdd.PricePerMonth).Should().BeTrue();
				apartment.Latitude.Equals(apartmentToAdd.Latitude).Should().BeTrue();
				apartment.Longitude.Equals(apartmentToAdd.Longitude).Should().BeTrue();
				apartment.IsRented.Equals(apartmentToAdd.IsRented).Should().BeTrue();
				apartment.RealtorId.Equals(apartmentToAdd.RealtorId.Value).Should().BeTrue();

				context.Database.EnsureDeleted();
			}
		}
	}
}