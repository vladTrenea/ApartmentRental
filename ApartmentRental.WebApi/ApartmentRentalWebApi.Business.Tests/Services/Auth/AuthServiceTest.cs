using System;
using Microsoft.EntityFrameworkCore;
using Moq;

using ApartmentRentalWebApi.Business.Core.Services;
using ApartmentRentalWebApi.Data;
using ApartmentRentalWebApi.Localization.Resources;
using ApartmentRentalWebApi.TestModelBuilders.Constants;

namespace ApartmentRentalWebApi.Business.Tests.Services.Auth
{
	public class AuthServiceTest
	{
		protected readonly DbContextOptions<ApartmentRentalDbContext> DbContextOptions;
		protected readonly Mock<IEmailService> MockEmailService;
		protected readonly Mock<IHashService> HashService;
		protected readonly Mock<IErrorMessages> ErrorMessages;

		public AuthServiceTest()
		{
			DbContextOptions = new DbContextOptionsBuilder<ApartmentRentalDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
			MockEmailService = new Mock<IEmailService>();
			HashService = new Mock<IHashService>();
			ErrorMessages = new Mock<IErrorMessages>();

			HashService.Setup(h => h.EncodeString(It.IsAny<string>())).Returns((string @val) => val);
			ErrorMessages.Setup(e => e.EmailRequired).Returns(UserTestConstants.ValidationMessage).Verifiable();
			ErrorMessages.Setup(e => e.PasswordRequired).Returns(UserTestConstants.ValidationMessage).Verifiable();
			ErrorMessages.Setup(e => e.InvalidLogin).Returns(UserTestConstants.ValidationMessage).Verifiable();
			ErrorMessages.Setup(e => e.InvalidEmail).Returns(UserTestConstants.ValidationMessage).Verifiable();
			ErrorMessages.Setup(e => e.InvalidPassword).Returns(UserTestConstants.ValidationMessage).Verifiable();
			ErrorMessages.Setup(e => e.PasswordRequired).Returns(UserTestConstants.ValidationMessage).Verifiable();
			ErrorMessages.Setup(e => e.ConfirmPasswordMustMatchPassword).Returns(UserTestConstants.ValidationMessage).Verifiable();
			ErrorMessages.Setup(e => e.EmailAlreadyExists).Returns(UserTestConstants.ValidationMessage).Verifiable();
			ErrorMessages.Setup(e => e.FirstNameOnlyLetters).Returns(UserTestConstants.ValidationMessage).Verifiable();
			ErrorMessages.Setup(e => e.LastNameOnlyLetters).Returns(UserTestConstants.ValidationMessage).Verifiable();
			ErrorMessages.Setup(e => e.FirstNameRequired).Returns(UserTestConstants.ValidationMessage).Verifiable();
			ErrorMessages.Setup(e => e.LastNameRequired).Returns(UserTestConstants.ValidationMessage).Verifiable();
			ErrorMessages.Setup(e => e.ConfirmPasswordRequired).Returns(UserTestConstants.ValidationMessage).Verifiable();
		}
	}
}