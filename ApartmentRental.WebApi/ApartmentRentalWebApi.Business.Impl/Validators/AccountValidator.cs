using ApartmentRentalWebApi.Business.Core.Dto;
using ApartmentRentalWebApi.Localization.Resources;
using FluentValidation;

namespace ApartmentRentalWebApi.Business.Impl.Validators
{
	public class AccountValidator : BaseValidator<AccountDto>
	{
		public AccountValidator(IErrorMessages errorMessages)
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
		}
	}
}