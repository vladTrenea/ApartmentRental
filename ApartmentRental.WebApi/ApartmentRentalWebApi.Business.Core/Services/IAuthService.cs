using System;
using System.Threading.Tasks;

using ApartmentRentalWebApi.Business.Core.Dto;

namespace ApartmentRentalWebApi.Business.Core.Services
{
	public interface IAuthService
	{
		Task<AuthenticationDto> Login(LoginDto loginModel);

		Task Register(UserRegistrationDto registrationModel);

		Task ConfirmAccount(string confirmationToken);

		Task ResetPassword(PasswordResetDto model);

		Task<PasswordTokenValidityDto> GetPasswordTokenValidity(string passwordToken);

		Task ChangePassword(string passwordResetToken, PasswordChangeDto model);

		Task UpdateAccount(Guid id, AccountDto model);
	}
}