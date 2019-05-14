using System;

using ApartmentRentalWebApi.Business.Core.Dto;
using ApartmentRentalWebApi.Business.Core.Enums;
using ApartmentRentalWebApi.Business.Impl.Utils;
using ApartmentRentalWebApi.Domain.Entities;

namespace ApartmentRentalWebApi.Business.Impl.Mappers
{
	public static class UserMapper
	{
		public static UserDto ToUserDto(User user)
		{
			if (user == null)
			{
				return null;
			}

			return new UserDto
			{
				Id = user.Id,
				FirstName = user.FirstName,
				LastName = user.LastName,
				Email = user.Email,
				HasEmailConfirmed = user.EmailConfirmed,
				RoleId = (RoleEnum) Enum.ToObject(typeof(RoleEnum), user.RoleId),
				RoleName = user.Role.Name
			};
		}

		public static User ToUser(UserAddDto model)
		{
			if (model == null)
			{
				return null;
			}

			return new User
			{
				Id = SecurityUtils.GenerateUserId(),
				Email = model.Email,
				FirstName = model.FirstName,
				LastName = model.LastName,
				RoleId = (int)model.RoleId.Value,
				CreatedAt = DateTime.Now,
				PasswordResetToken = SecurityUtils.GeneratePasswordResetToken(),
				PasswordResetEndDate = DateTime.Now.AddHours(SecurityUtils.PasswordResetTokenDuration)
			};
		}

		public static User ToUser(UserRegistrationDto model, string hashedPassword)
		{
			if (model == null)
			{
				return null;
			}

			return new User
			{
				Id = SecurityUtils.GenerateUserId(),
				Email = model.Email,
				Password = hashedPassword,
				FirstName = model.FirstName,
				LastName = model.LastName,
				RoleId = (int) RoleEnum.Client,
				CreatedAt = DateTime.Now,
				EmailConfirmationToken = SecurityUtils.GenerateEmailConfirmationToken()
			};
		}

		public static RealtorDto ToRealtorDto(User user)
		{
			if (user == null)
			{
				return null;
			}

			return new RealtorDto
			{
				Id = user.Id,
				Email = user.Email,
				LastName = user.LastName,
				FirstName = user.FirstName
			};
		}
	}
}