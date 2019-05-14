using System;
using System.Linq;
using Xunit;

using System.Threading.Tasks;
using ApartmentRentalWebApi.Business.Core.Enums;
using ApartmentRentalWebApi.Business.Core.Exceptions;
using ApartmentRentalWebApi.Business.Impl.Services;
using ApartmentRentalWebApi.Data;
using ApartmentRentalWebApi.TestModelBuilders.Builders;
using ApartmentRentalWebApi.TestModelBuilders.Constants;
using FluentAssertions;

namespace ApartmentRentalWebApi.Business.Tests.Services.Apartment
{
	public class UpdateApartmentTests : ApartmentServiceTest
	{
		[Fact]
		public async Task UpdateApartment_InvalidTitle_ThrowsValidationException()
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				var apartment = new ApartmentBuilder().Build();
				await context.Apartments.AddAsync(apartment);
				await context.SaveChangesAsync();

				var apartmentService = new ApartmentService(context, ErrorMessages.Object);

				var apartmentWithTitleNull = new ApartmentAddUpdateDtoBuilder().WithTitle(null).Build();
				await Assert.ThrowsAsync<ValidationException>(() => apartmentService.Update(apartment.Id, apartmentWithTitleNull));

				var apartmentWithTitleEmpty = new ApartmentAddUpdateDtoBuilder().WithTitle(string.Empty).Build();
				await Assert.ThrowsAsync<ValidationException>(() => apartmentService.Update(apartment.Id, apartmentWithTitleEmpty));

				context.Database.EnsureDeleted();
			}
		}

		[Fact]
		public async Task UpdateApartment_InvalidDescription_ThrowsValidationException()
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				var apartment = new ApartmentBuilder().Build();
				await context.Apartments.AddAsync(apartment);
				await context.SaveChangesAsync();

				var apartmentService = new ApartmentService(context, ErrorMessages.Object);

				var apartmentWithDescriptionNull = new ApartmentAddUpdateDtoBuilder().WithDescription(null).Build();
				await Assert.ThrowsAsync<ValidationException>(() => apartmentService.Update(apartment.Id, apartmentWithDescriptionNull));

				var apartmentWithDescriptionEmpty = new ApartmentAddUpdateDtoBuilder().WithDescription(string.Empty).Build();
				await Assert.ThrowsAsync<ValidationException>(() => apartmentService.Update(apartment.Id, apartmentWithDescriptionEmpty));

				context.Database.EnsureDeleted();
			}
		}

		[Fact]
		public async Task UpdateApartment_InvalidArea_ThrowsValidationException()
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				var apartment = new ApartmentBuilder().Build();
				await context.Apartments.AddAsync(apartment);
				await context.SaveChangesAsync();

				var apartmentService = new ApartmentService(context, ErrorMessages.Object);

				var apartmentWithAreaZero = new ApartmentAddUpdateDtoBuilder().WithArea(0).Build();
				await Assert.ThrowsAsync<ValidationException>(() => apartmentService.Update(apartment.Id, apartmentWithAreaZero));

				var apartmentWithAreaNegative = new ApartmentAddUpdateDtoBuilder().WithArea(-10).Build();
				await Assert.ThrowsAsync<ValidationException>(() => apartmentService.Update(apartment.Id, apartmentWithAreaNegative));

				context.Database.EnsureDeleted();
			}
		}

		[Fact]
		public async Task UpdateApartment_InvalidPricePerMonth_ThrowsValidationException()
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				var apartment = new ApartmentBuilder().Build();
				await context.Apartments.AddAsync(apartment);
				await context.SaveChangesAsync();

				var apartmentService = new ApartmentService(context, ErrorMessages.Object);

				var apartmentWithPricePerMonthZero = new ApartmentAddUpdateDtoBuilder().WithPricePerMonth(0).Build();
				await Assert.ThrowsAsync<ValidationException>(() => apartmentService.Update(apartment.Id, apartmentWithPricePerMonthZero));

				var apartmentWithPricePerMonthNegative = new ApartmentAddUpdateDtoBuilder().WithPricePerMonth(-10).Build();
				await Assert.ThrowsAsync<ValidationException>(() => apartmentService.Update(apartment.Id, apartmentWithPricePerMonthNegative));

				context.Database.EnsureDeleted();
			}
		}

		[Fact]
		public async Task UpdateApartment_InvalidNrOfRooms_ThrowsValidationException()
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				var apartment = new ApartmentBuilder().Build();
				await context.Apartments.AddAsync(apartment);
				await context.SaveChangesAsync();

				var apartmentService = new ApartmentService(context, ErrorMessages.Object);

				var apartmentWithNrOfRoomsZero = new ApartmentAddUpdateDtoBuilder().WithNrOfRooms(0).Build();
				await Assert.ThrowsAsync<ValidationException>(() => apartmentService.Update(apartment.Id, apartmentWithNrOfRoomsZero));

				var apartmentWithNrOfRoomsNegative = new ApartmentAddUpdateDtoBuilder().WithNrOfRooms(-10).Build();
				await Assert.ThrowsAsync<ValidationException>(() => apartmentService.Update(apartment.Id, apartmentWithNrOfRoomsNegative));

				context.Database.EnsureDeleted();
			}
		}

		[Fact]
		public async Task UpdateApartment_InvalidLatitude_ThrowsValidationException()
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				var apartment = new ApartmentBuilder().Build();
				await context.Apartments.AddAsync(apartment);
				await context.SaveChangesAsync();

				var apartmentService = new ApartmentService(context, ErrorMessages.Object);

				var apartmentWithLatitudeGreaterThanMaxValue = new ApartmentAddUpdateDtoBuilder().WithLatitude(ApartmentTestConstants.InvalidLatitudeGreaterThanMaxValue).Build();
				await Assert.ThrowsAsync<ValidationException>(() => apartmentService.Update(apartment.Id, apartmentWithLatitudeGreaterThanMaxValue));

				var apartmentWithLongitudeLessThenMinValue = new ApartmentAddUpdateDtoBuilder().WithLatitude(ApartmentTestConstants.InvalidLatitudeLessThanMinValue).Build();
				await Assert.ThrowsAsync<ValidationException>(() => apartmentService.Update(apartment.Id, apartmentWithLongitudeLessThenMinValue));

				context.Database.EnsureDeleted();
			}
		}

		[Fact]
		public async Task UpdateApartment_InvalidLongitude_ThrowsValidationException()
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				var apartment = new ApartmentBuilder().Build();
				await context.Apartments.AddAsync(apartment);
				await context.SaveChangesAsync();

				var apartmentService = new ApartmentService(context, ErrorMessages.Object);

				var apartmentWithLatitudeGreaterThanMaxValue = new ApartmentAddUpdateDtoBuilder().WithLongitude(ApartmentTestConstants.InvalidLongitudeGreaterThanMaxValue).Build();
				await Assert.ThrowsAsync<ValidationException>(() => apartmentService.Update(apartment.Id, apartmentWithLatitudeGreaterThanMaxValue));

				var apartmentWithLongitudeLessThenMinValue = new ApartmentAddUpdateDtoBuilder().WithLongitude(ApartmentTestConstants.InvalidLongitudeLessThanMinValue).Build();
				await Assert.ThrowsAsync<ValidationException>(() => apartmentService.Update(apartment.Id, apartmentWithLongitudeLessThenMinValue));

				context.Database.EnsureDeleted();
			}
		}

		[Fact]
		public async Task UpdateApartment_InvalidRealtorId_ThrowsValidationException()
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				var apartment = new ApartmentBuilder().Build();
				await context.Apartments.AddAsync(apartment);
				await context.SaveChangesAsync();

				var apartmentService = new ApartmentService(context, ErrorMessages.Object);

				var apartmentWithNoRealtorId = new ApartmentAddUpdateDtoBuilder().WithRealtorId(null).Build();
				await Assert.ThrowsAsync<ValidationException>(() => apartmentService.Update(apartment.Id, apartmentWithNoRealtorId));

				context.Database.EnsureDeleted();
			}
		}

		[Fact]
		public async Task UpdateApartment_InvalidRealtorIdNotFound_ThrowsNotFoundException()
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				var apartment = new ApartmentBuilder().Build();
				await context.Apartments.AddAsync(apartment);
				await context.SaveChangesAsync();

				var apartmentService = new ApartmentService(context, ErrorMessages.Object);

				var apartmentWithNotFoundRealtor = new ApartmentAddUpdateDtoBuilder().WithRealtorId(Guid.Empty).Build();
				await Assert.ThrowsAsync<NotFoundException>(() => apartmentService.Update(apartment.Id, apartmentWithNotFoundRealtor));

				context.Database.EnsureDeleted();
			}
		}

		[Fact]
		public async Task UpdateApartment_InvalidRealtorIdNotRealtorRole_ThrowsNotFoundException()
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				var apartment = new ApartmentBuilder().Build();
				await context.Apartments.AddAsync(apartment);
				var user = new UserBuilder().WithRole(RoleEnum.Client).Build();
				await context.Users.AddAsync(user);
				await context.SaveChangesAsync();

				var apartmentService = new ApartmentService(context, ErrorMessages.Object);

				var apartmentWithNotARealtorUser = new ApartmentAddUpdateDtoBuilder().WithRealtorId(user.Id).Build();
				await Assert.ThrowsAsync<NotFoundException>(() => apartmentService.Update(apartment.Id, apartmentWithNotARealtorUser));

				context.Database.EnsureDeleted();
			}
		}

		[Fact]
		public async Task UpdateApartment_InvalidApartmentId_ThrowsNotFoundException()
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				var user = new UserBuilder().WithRole(RoleEnum.Realtor).Build();
				await context.Users.AddAsync(user);
				await context.SaveChangesAsync();

				var apartmentService = new ApartmentService(context, ErrorMessages.Object);

				var apartmentToUpdate = new ApartmentAddUpdateDtoBuilder().WithRealtorId(user.Id).Build();
				await Assert.ThrowsAsync<NotFoundException>(() => apartmentService.Update(Guid.Empty, apartmentToUpdate));

				context.Database.EnsureDeleted();
			}
		}

		[Fact]
		public async Task UpdateApartment_Success()
		{
			using (var context = new ApartmentRentalDbContext(DbContextOptions))
			{
				var user = new UserBuilder().WithRole(RoleEnum.Realtor).Build();
				await context.Users.AddAsync(user);
				var apartment = new ApartmentBuilder().WithRealtorId(user.Id).Build();
				await context.Apartments.AddAsync(apartment);
				await context.SaveChangesAsync();

				var apartmentService = new ApartmentService(context, ErrorMessages.Object);

				var apartmentToUpdate = new ApartmentAddUpdateDtoBuilder().WithRealtorId(user.Id).Build();
				await apartmentService.Update(apartment.Id, apartmentToUpdate);
				context.Apartments.Count().Equals(1).Should().BeTrue();
				var dbApartment = context.Apartments.FirstOrDefault();
				dbApartment.Title.Equals(apartmentToUpdate.Title).Should().BeTrue();
				dbApartment.Description.Equals(apartmentToUpdate.Description).Should().BeTrue();
				dbApartment.Area.Equals(apartmentToUpdate.Area).Should().BeTrue();
				dbApartment.NrOfRooms.Equals(apartmentToUpdate.NrOfRooms).Should().BeTrue();
				dbApartment.PricePerMonth.Equals(apartmentToUpdate.PricePerMonth).Should().BeTrue();
				dbApartment.Latitude.Equals(apartmentToUpdate.Latitude).Should().BeTrue();
				dbApartment.Longitude.Equals(apartmentToUpdate.Longitude).Should().BeTrue();
				dbApartment.IsRented.Equals(apartmentToUpdate.IsRented).Should().BeTrue();
				dbApartment.RealtorId.Equals(apartmentToUpdate.RealtorId.Value).Should().BeTrue();

				context.Database.EnsureDeleted();
			}
		}
	}
}