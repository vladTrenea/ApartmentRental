using ApartmentRentalWebApi.Business.Core.Dto;
using ApartmentRentalWebApi.TestModelBuilders.Constants;

namespace ApartmentRentalWebApi.TestModelBuilders.Builders
{
	public class LoginBuilder
	{
		private string _email = UserTestConstants.ValidEmail;
		private string _password = UserTestConstants.ValidPassword;

		public LoginDto Build()
		{
			return new LoginDto
			{
				Email = _email,
				Password = _password
			};
		}

		public LoginBuilder WithEmail(string email)
		{
			_email = email;

			return this;
		}

		public LoginBuilder WithPassword(string password)
		{
			_password = password;

			return this;
		}
	}
}