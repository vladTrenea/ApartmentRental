using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ApartmentRentalWebApi.Business.Core.Dto;
using ApartmentRentalWebApi.Business.Core.Enums;
using ApartmentRentalWebApi.Business.Core.Exceptions;
using ApartmentRentalWebApi.Business.Core.Services;
using ApartmentRentalWebApi.Business.Impl.Mappers;
using ApartmentRentalWebApi.Business.Impl.Utils;
using ApartmentRentalWebApi.Business.Impl.Validators;
using ApartmentRentalWebApi.Data;
using ApartmentRentalWebApi.Localization.Resources;

namespace ApartmentRentalWebApi.Business.Impl.Services
{
	public class AuthService : IAuthService
	{
		private readonly ApartmentRentalDbContext _dbContext;

		private readonly IEmailService _emailService;

		private readonly IHashService _hashService;

		private readonly IErrorMessages _errorMessages;

		public AuthService(ApartmentRentalDbContext dbContext, IEmailService emailService, IHashService hashService,
			IErrorMessages errorMessages)
		{
			_dbContext = dbContext;
			_emailService = emailService;
			_hashService = hashService;
			_errorMessages = errorMessages;
		}

		public async Task<AuthenticationDto> Login(LoginDto loginModel)
		{
			var validator = new LoginValidator(_errorMessages);
			validator.Validate(loginModel);

			var user = await _dbContext.Users
				.AsNoTracking()
				.FirstOrDefaultAsync(u =>
					u.Email.Equals(loginModel.Email) &&
					u.Password.Equals(_hashService.EncodeString(loginModel.Password)) && u.EmailConfirmed);

			if (user == null)
			{
				throw new UnauthorizedException(_errorMessages.InvalidLogin);
			}

			return new AuthenticationDto
			{
				UserId = user.Id,
				FirstName = user.FirstName,
				LastName = user.LastName,
				Email = user.Email,
				RoleId = (RoleEnum) Enum.ToObject(typeof(RoleEnum), user.RoleId)
			};
		}

		public async Task Register(UserRegistrationDto registrationModel)
		{
			var validator = new UserRegistrationValidator(_errorMessages);
			validator.Validate(registrationModel);

			var userWithEmail =
				await _dbContext.Users.FirstOrDefaultAsync(u => u.Email.Equals(registrationModel.Email));
			if (userWithEmail != null)
			{
				throw new ConflictException(_errorMessages.EmailAlreadyExists);
			}

			var newUser = UserMapper.ToUser(registrationModel, _hashService.EncodeString(registrationModel.Password));
			await _dbContext.Users.AddAsync(newUser);
			await _dbContext.SaveChangesAsync();

			await _emailService.SendAccountConfirmationEmail(registrationModel.Email, newUser.EmailConfirmationToken);
		}

		public async Task ConfirmAccount(string confirmationToken)
		{
			var user = await _dbContext.Users
				.FirstOrDefaultAsync(u => u.EmailConfirmationToken.Equals(confirmationToken));

			if (user == null)
			{
				throw new NotFoundException();
			}

			user.EmailConfirmed = true;
			user.ConfirmedAt = DateTime.Now;
			user.EmailConfirmationToken = null;
			await _dbContext.SaveChangesAsync();
		}

		public async Task ResetPassword(PasswordResetDto model)
		{
			var validator = new PasswordResetValidator(_errorMessages);
			validator.Validate(model);

			var user = await _dbContext.Users
				.FirstOrDefaultAsync(u => u.Email.Equals(model.Email) && u.EmailConfirmed);

			if (user == null)
			{
				throw new NotFoundException();
			}

			user.PasswordResetToken = SecurityUtils.GeneratePasswordResetToken();
			user.PasswordResetEndDate = DateTime.Now.AddHours(SecurityUtils.PasswordResetTokenDuration);
			await _dbContext.SaveChangesAsync();

			await _emailService.SendPasswordResetEmail(user.Email, user.PasswordResetToken);
		}

		public async Task<PasswordTokenValidityDto> GetPasswordTokenValidity(string passwordToken)
		{
			var user = await _dbContext.Users
				.AsNoTracking()
				.FirstOrDefaultAsync(u => u.PasswordResetToken == passwordToken && u.EmailConfirmed);
			if (user == null)
			{
				throw new NotFoundException();
			}

			var passwordTokenValidityModel = new PasswordTokenValidityDto();

			if (user.PasswordResetEndDate > DateTime.Now)
			{
				passwordTokenValidityModel.Valid = true;
			}

			return passwordTokenValidityModel;
		}

		public async Task ChangePassword(string passwordResetToken, PasswordChangeDto model)
		{
			var validator = new PasswordChangeValidator(_errorMessages);
			validator.Validate(model);

			var user = await _dbContext.Users
				.FirstOrDefaultAsync(u => u.PasswordResetToken.Equals(passwordResetToken));

			if (user == null)
			{
				throw new NotFoundException();
			}

			if (user.PasswordResetEndDate < DateTime.Now)
			{
				throw new ConflictException(_errorMessages.PasswordTokenExpired);
			}

			user.Password = _hashService.EncodeString(model.Password);
			user.PasswordResetToken = null;
			user.PasswordResetEndDate = null;
			await _dbContext.SaveChangesAsync();
		}

		public async Task UpdateAccount(Guid id, AccountDto model)
		{
			var validator = new AccountValidator(_errorMessages);
			validator.Validate(model);

			var user = await _dbContext.Users
				.FirstOrDefaultAsync(u => u.Id == id);

			if (user == null)
			{
				throw new NotFoundException();
			}

			user.FirstName = model.FirstName;
			user.LastName = model.LastName;
			await _dbContext.SaveChangesAsync();
		}
	}
}