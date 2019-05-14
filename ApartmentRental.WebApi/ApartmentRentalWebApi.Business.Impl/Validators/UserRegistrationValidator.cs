using FluentValidation;

using ApartmentRentalWebApi.Business.Core.Dto;
using ApartmentRentalWebApi.Localization.Resources;

namespace ApartmentRentalWebApi.Business.Impl.Validators
{
	public class UserRegistrationValidator : BaseValidator<UserRegistrationDto>
	{
		public UserRegistrationValidator(IErrorMessages errorMessages)
		{
			RuleFor(registration => registration.FirstName)
				.NotEmpty()
				.WithMessage(errorMessages.FirstNameRequired);

			RuleFor(registration => registration.FirstName)
				.Matches(ValidationConstants.OnlyLettersRegex)
				.WithMessage(errorMessages.FirstNameOnlyLetters);

			RuleFor(registration => registration.LastName)
				.NotEmpty()
				.WithMessage(errorMessages.LastNameRequired);

			RuleFor(registration => registration.LastName)
				.Matches(ValidationConstants.OnlyLettersRegex)
				.WithMessage(errorMessages.LastNameOnlyLetters);

			RuleFor(registration => registration.Email)
				.NotEmpty()
				.WithMessage(errorMessages.EmailRequired);

			RuleFor(registration => registration.Email)
				.EmailAddress()
				.WithMessage(errorMessages.InvalidEmail);

			RuleFor(registration => registration.Password)
				.NotEmpty()
				.WithMessage(errorMessages.PasswordRequired);

			RuleFor(registration => registration.Password)
				.Matches(ValidationConstants.PasswordRegex)
				.WithMessage(errorMessages.InvalidPassword);

			RuleFor(registration => registration.ConfirmPassword)
				.NotEmpty()
				.WithMessage(errorMessages.ConfirmPasswordRequired);

			RuleFor(registration => registration.ConfirmPassword)
				.Equal(registration => registration.Password)
				.WithMessage(errorMessages.ConfirmPasswordMustMatchPassword);
		}
	}
}