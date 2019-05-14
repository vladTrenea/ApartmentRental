using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ApartmentRentalWebApi.Business.Core.Dto;
using ApartmentRentalWebApi.Business.Core.Enums;
using ApartmentRentalWebApi.Business.Core.Exceptions;
using ApartmentRentalWebApi.Business.Core.Services;
using ApartmentRentalWebApi.Business.Impl.Mappers;
using ApartmentRentalWebApi.Business.Impl.Validators;
using ApartmentRentalWebApi.Data;
using ApartmentRentalWebApi.Localization.Resources;

namespace ApartmentRentalWebApi.Business.Impl.Services
{
	public class UserService : IUserService
	{
		private readonly ApartmentRentalDbContext _dbContext;

		private readonly IEmailService _emailService;

		private readonly IErrorMessages _errorMessages;

		public UserService(ApartmentRentalDbContext dbContext, IEmailService emailService, IErrorMessages errorMessages)
		{
			_dbContext = dbContext;
			_emailService = emailService;
			_errorMessages = errorMessages;
		}

		public async Task<IEnumerable<UserDto>> GetUsers()
		{
			var users = await _dbContext.Users
				.Where(u => u.RoleId != (int) RoleEnum.Admin)
				.OrderBy(r => r.Email)
				.Include(u => u.Role)
				.ToListAsync();

			return users.Select(UserMapper.ToUserDto);
		}

		public async Task<IEnumerable<RealtorDto>> GetRealtors()
		{
			var realtors = await _dbContext.Users
				.Where(u => u.RoleId == (int) RoleEnum.Realtor && u.EmailConfirmed)
				.OrderBy(r => r.LastName).ThenBy(r => r.FirstName)
				.AsNoTracking()
				.ToListAsync();

			return realtors.Select(UserMapper.ToRealtorDto);
		}

		public async Task<UserDto> GetById(Guid id)
		{
			var user = await _dbContext.Users
				.Include(u => u.Role)
				.AsNoTracking()
				.FirstOrDefaultAsync(u => u.Id == id && u.RoleId != (int) RoleEnum.Admin);

			if (user == null)
			{
				throw new NotFoundException();
			}

			return UserMapper.ToUserDto(user);
		}

		public async Task AddUser(UserAddDto model)
		{
			var validator = new UserAddValidator(_errorMessages);
			validator.Validate(model);

			var userWithEmail = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email.Equals(model.Email));
			if (userWithEmail != null)
			{
				throw new ConflictException(_errorMessages.EmailAlreadyExists);
			}

			var newUser = UserMapper.ToUser(model);
			await _dbContext.Users.AddAsync(newUser);
			await _dbContext.SaveChangesAsync();

			await _emailService.SendAccountCreationEmail(newUser.Email, newUser.PasswordResetToken);
		}

		public async Task UpdateUser(Guid id, UserUpdateDto model)
		{
			var validator = new UserUpdateValidator(_errorMessages);
			validator.Validate(model);

			var user = await _dbContext.Users
				.Include(m => m.ManagedApartments)
				.FirstOrDefaultAsync(u => u.Id == id && u.RoleId != (int) RoleEnum.Admin);
			if (user == null)
			{
				throw new NotFoundException();
			}

			if (user.RoleId == (int) RoleEnum.Realtor && model.RoleId.Value == RoleEnum.Client && user.ManagedApartments.Any())
			{
				throw new ConflictException(_errorMessages.CannotChangeRealtorWithApartments);
			}

			user.FirstName = model.FirstName;
			user.LastName = model.LastName;
			user.RoleId = (int) model.RoleId.Value;
			await _dbContext.SaveChangesAsync();
		}

		public async Task DeleteUser(Guid id)
		{
			var user = await _dbContext.Users
				.Include(a => a.ManagedApartments)
				.FirstOrDefaultAsync(u => u.Id == id && u.RoleId != (int) RoleEnum.Admin);

			if (user == null)
			{
				throw new NotFoundException();
			}

			if (user.ManagedApartments.Any())
			{
				throw new ConflictException(_errorMessages.CannotDeleteActiveRealtor);
			}

			_dbContext.Users.Remove(user);
			await _dbContext.SaveChangesAsync();
		}
	}
}