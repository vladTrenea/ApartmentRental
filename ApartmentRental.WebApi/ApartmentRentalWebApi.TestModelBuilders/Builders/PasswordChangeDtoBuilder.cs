using ApartmentRentalWebApi.Business.Core.Dto;
using ApartmentRentalWebApi.TestModelBuilders.Constants;

namespace ApartmentRentalWebApi.TestModelBuilders.Builders
{
	public class PasswordChangeDtoBuilder
	{
		private string _password = UserTestConstants.ValidPassword;
		private string _confirmPassword = UserTestConstants.ValidPassword;

		public PasswordChangeDto Build()
		{
			return new PasswordChangeDto
			{
				Password = _password,
				ConfirmPassword = _confirmPassword
			};
		}

		public PasswordChangeDtoBuilder WithPassword(string password)
		{
			_password = password;

			return this;
		}

		public PasswordChangeDtoBuilder WithConfirmPassword(string confirmPassword)
		{
			_confirmPassword = confirmPassword;

			return this;
		}
	}
}