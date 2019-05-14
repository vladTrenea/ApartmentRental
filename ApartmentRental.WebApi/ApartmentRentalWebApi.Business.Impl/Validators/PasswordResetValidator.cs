using FluentValidation;

using ApartmentRentalWebApi.Business.Core.Dto;
using ApartmentRentalWebApi.Localization.Resources;

namespace ApartmentRentalWebApi.Business.Impl.Validators
{
	public class PasswordResetValidator : BaseValidator<PasswordResetDto>
	{
		public PasswordResetValidator(IErrorMessages errorMessages)
		{
			RuleFor(model => model.Email)
				.NotEmpty()
				.WithMessage(errorMessages.EmailRequired);

			RuleFor(model => model.Email)
				.EmailAddress()
				.WithMessage(errorMessages.InvalidEmail);
		}
	}
}