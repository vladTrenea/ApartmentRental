using System;
using Microsoft.EntityFrameworkCore;
using Moq;

using ApartmentRentalWebApi.Data;
using ApartmentRentalWebApi.Localization.Resources;
using ApartmentRentalWebApi.TestModelBuilders.Constants;

namespace ApartmentRentalWebApi.Business.Tests.Services.Apartment
{
	public class ApartmentServiceTest
	{
		protected readonly DbContextOptions<ApartmentRentalDbContext> DbContextOptions;
		protected readonly Mock<IErrorMessages> ErrorMessages;

		public ApartmentServiceTest()
		{
			DbContextOptions = new DbContextOptionsBuilder<ApartmentRentalDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
			ErrorMessages = new Mock<IErrorMessages>();
			ErrorMessages.Setup(e => e.TitleRequired).Returns(ApartmentTestConstants.ValidationMessage).Verifiable();
			ErrorMessages.Setup(e => e.DescriptionRequired).Returns(ApartmentTestConstants.ValidationMessage).Verifiable();
			ErrorMessages.Setup(e => e.AreaGreaterThan0).Returns(ApartmentTestConstants.ValidationMessage).Verifiable();
			ErrorMessages.Setup(e => e.PricePerMonthGreaterThan0).Returns(ApartmentTestConstants.ValidationMessage).Verifiable();
			ErrorMessages.Setup(e => e.NrOfRoomsGreaterThan0).Returns(ApartmentTestConstants.ValidationMessage).Verifiable();
			ErrorMessages.Setup(e => e.InvalidLatitude).Returns(ApartmentTestConstants.ValidationMessage).Verifiable();
			ErrorMessages.Setup(e => e.InvalidLongitude).Returns(ApartmentTestConstants.ValidationMessage).Verifiable();
			ErrorMessages.Setup(e => e.RealtorIdMandatory).Returns(ApartmentTestConstants.ValidationMessage).Verifiable();

		}
	}
}