using System;
using Microsoft.EntityFrameworkCore;
using Moq;

using ApartmentRentalWebApi.Business.Core.Services;
using ApartmentRentalWebApi.Data;
using ApartmentRentalWebApi.Localization.Resources;
using ApartmentRentalWebApi.TestModelBuilders.Constants;

namespace ApartmentRentalWebApi.Business.Tests.Services.User
{
	public class UserServiceTest
	{
		protected readonly DbContextOptions<ApartmentRentalDbContext> DbContextOptions;
		protected readonly Mock<IEmailService> MockEmailService;
		protected readonly Mock<IErrorMessages> ErrorMessages;

		public UserServiceTest()
		{
			DbContextOptions = new DbContextOptionsBuilder<ApartmentRentalDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
			MockEmailService = new Mock<IEmailService>();
			ErrorMessages = new Mock<IErrorMessages>();
			ErrorMessages.Setup(e => e.EmailRequired).Returns(UserTestConstants.ValidationMessage).Verifiable();
			ErrorMessages.Setup(e => e.InvalidEmail).Returns(UserTestConstants.ValidationMessage).Verifiable();
			ErrorMessages.Setup(e => e.EmailAlreadyExists).Returns(UserTestConstants.ValidationMessage).Verifiable();
			ErrorMessages.Setup(e => e.FirstNameRequired).Returns(UserTestConstants.ValidationMessage).Verifiable();
			ErrorMessages.Setup(e => e.FirstNameOnlyLetters).Returns(UserTestConstants.ValidationMessage).Verifiable();
			ErrorMessages.Setup(e => e.LastNameRequired).Returns(UserTestConstants.ValidationMessage).Verifiable();
			ErrorMessages.Setup(e => e.LastNameOnlyLetters).Returns(UserTestConstants.ValidationMessage).Verifiable();
			ErrorMessages.Setup(e => e.RoleIdMandatory).Returns(UserTestConstants.ValidationMessage).Verifiable();
			ErrorMessages.Setup(e => e.InvalidRole).Returns(UserTestConstants.ValidationMessage).Verifiable();
			ErrorMessages.Setup(e => e.CannotDeleteActiveRealtor).Returns(UserTestConstants.ValidationMessage).Verifiable();
		}
	}
}