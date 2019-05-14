using System;

using ApartmentRentalWebApi.Business.Core.Enums;
using ApartmentRentalWebApi.Domain.Entities;
using ApartmentRentalWebApi.TestModelBuilders.Constants;

namespace ApartmentRentalWebApi.TestModelBuilders.Builders
{
	public class UserBuilder
	{
		private readonly Guid _id = Guid.NewGuid();
		private string _email = UserTestConstants.ValidEmail;
		private string _password = UserTestConstants.ValidPassword;
		private readonly string _firstName = UserTestConstants.ValidFirstName;
		private readonly string _lastName = UserTestConstants.ValidFirstName;
		private int _roleId = (int) RoleEnum.Client;
		private bool _emailConfirmed = true;
		private string _emailConfirmationToken;
		private string _passwordResetToken;
		private DateTime? _passwordResetEndDate = null;
		private readonly DateTime _createdAt = DateTime.Now;
		private readonly DateTime? _confirmedAt = DateTime.Now;

		public User Build()
		{
			return new User
			{
				Id = _id,
				Email = _email,
				Password = _password,
				FirstName = _firstName,
				LastName = _lastName,
				EmailConfirmed = _emailConfirmed,
				RoleId = _roleId,
				CreatedAt = _createdAt,
				PasswordResetToken = _passwordResetToken,
				EmailConfirmationToken = _emailConfirmationToken,
				PasswordResetEndDate = _passwordResetEndDate,
				ConfirmedAt = _confirmedAt
			};
		}

		public UserBuilder WithEmail(string email)
		{
			_email = email;

			return this;
		}

		public UserBuilder WithRole(RoleEnum role)
		{
			_roleId = (int) role;

			return this;
		}

		public UserBuilder WithPassword(string password)
		{
			_password = password;

			return this;
		}

		public UserBuilder WithEmailConfirmed(bool emailConfirmed)
		{
			_emailConfirmed = emailConfirmed;

			return this;
		}

		public UserBuilder WithEmailConfirmationToken(string emailConfirmationToken)
		{
			_emailConfirmationToken = emailConfirmationToken;

			return this;
		}

		public UserBuilder WithPasswordResetToken(string passwordResetToken)
		{
			_passwordResetToken = passwordResetToken;

			return this;
		}

		public UserBuilder WithPasswordResetEndDate(DateTime? passwordResetEndDate)
		{
			_passwordResetEndDate = passwordResetEndDate;

			return this;
		}
	}
}