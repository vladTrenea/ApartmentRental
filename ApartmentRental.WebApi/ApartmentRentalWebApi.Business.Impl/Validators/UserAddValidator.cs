using FluentValidation;

using ApartmentRentalWebApi.Business.Core.Dto;
using ApartmentRentalWebApi.Business.Core.Enums;
using ApartmentRentalWebApi.Localization.Resources;

namespace ApartmentRentalWebApi.Business.Impl.Validators
{
	public class UserAddValidator : BaseValidator<UserAddDto>
	{
		public UserAddValidator(IErrorMessages errorMessages)
		{
			RuleFor(model => model.FirstName)
				.NotEmpty()
				.WithMessage(errorMessages.FirstNameRequired);

			RuleFor(model => model.FirstName)
				.Matches(ValidationConstants.OnlyLettersRegex)
				.WithMessage(errorMessages.FirstNameOnlyLetters);

			RuleFor(model => model.LastName)
				.NotEmpty()
				.WithMessage(errorMessages.LastNameRequired);

			RuleFor(model => model.LastName)
				.Matches(ValidationConstants.OnlyLettersRegex)
				.WithMessage(errorMessages.LastNameOnlyLetters);

			RuleFor(model => model.Email)
				.NotEmpty()
				.WithMessage(errorMessages.EmailRequired);

			RuleFor(model => model.Email)
				.EmailAddress()
				.WithMessage(errorMessages.InvalidEmail);

			RuleFor(model => model.RoleId)
				.NotNull()
				.WithMessage(errorMessages.RoleIdMandatory);

			RuleFor(model => model.RoleId)
				.NotEqual(RoleEnum.Admin)
				.WithMessage(errorMessages.InvalidRole);
		}
	}
}