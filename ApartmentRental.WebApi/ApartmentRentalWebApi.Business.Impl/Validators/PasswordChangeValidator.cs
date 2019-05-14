using FluentValidation;

using ApartmentRentalWebApi.Business.Core.Dto;
using ApartmentRentalWebApi.Localization.Resources;

namespace ApartmentRentalWebApi.Business.Impl.Validators
{
	public class PasswordChangeValidator : BaseValidator<PasswordChangeDto>
	{
		public PasswordChangeValidator(IErrorMessages errorMessages)
		{
			RuleFor(model => model.Password)
				.NotEmpty()
				.WithMessage(errorMessages.PasswordRequired);

			RuleFor(model => model.Password)
				.Matches(ValidationConstants.PasswordRegex)
				.WithMessage(errorMessages.InvalidPassword);

			RuleFor(model => model.ConfirmPassword)
				.NotEmpty()
				.WithMessage(errorMessages.ConfirmPasswordRequired);

			RuleFor(model => model.ConfirmPassword)
				.Equal(model => model.Password)
				.WithMessage(errorMessages.ConfirmPasswordMustMatchPassword);
		}
	}
}