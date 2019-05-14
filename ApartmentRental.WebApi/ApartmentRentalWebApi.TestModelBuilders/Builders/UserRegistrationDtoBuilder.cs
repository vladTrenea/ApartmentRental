using ApartmentRentalWebApi.Business.Core.Dto;
using ApartmentRentalWebApi.TestModelBuilders.Constants;

namespace ApartmentRentalWebApi.TestModelBuilders.Builders
{
	public class UserRegistrationDtoBuilder
	{
		private string _email = UserTestConstants.ValidEmail;
		private string _password = UserTestConstants.ValidPassword;
		private string _confirmPassword = UserTestConstants.ValidPassword;
		private string _firstName = UserTestConstants.ValidFirstName;
		private string _lastName = UserTestConstants.ValidLastName;

		public UserRegistrationDto Build()
		{
			return new UserRegistrationDto
			{
				Email = _email,
				Password = _password,
				ConfirmPassword = _confirmPassword,
				LastName = _lastName,
				FirstName = _firstName
			};
		}

		public UserRegistrationDtoBuilder WithEmail(string email)
		{
			_email = email;

			return this;
		}

		public UserRegistrationDtoBuilder WithPassword(string password)
		{
			_password = password;

			return this;
		}

		public UserRegistrationDtoBuilder WithConfirmPassword(string confirmPassword)
		{
			_confirmPassword = confirmPassword;

			return this;
		}

		public UserRegistrationDtoBuilder WithFirstName(string firstName)
		{
			_firstName = firstName;

			return this;
		}

		public UserRegistrationDtoBuilder WithLastName(string lastName)
		{
			_lastName = lastName;

			return this;
		}
	}
}