using FluentValidation;

using ApartmentRentalWebApi.Business.Core.Dto;
using ApartmentRentalWebApi.Localization.Resources;

namespace ApartmentRentalWebApi.Business.Impl.Validators
{
	public class LoginValidator : BaseValidator<LoginDto>
	{
		public LoginValidator(IErrorMessages errorMessages)
		{
			RuleFor(login => login.Email)
				.NotEmpty()
				.WithMessage(errorMessages.EmailRequired);

			RuleFor(login => login.Password)
				.NotEmpty()
				.WithMessage(errorMessages.PasswordRequired);
		}
	}
}