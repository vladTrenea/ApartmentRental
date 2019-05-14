using ApartmentRentalWebApi.Business.Core.Dto;
using ApartmentRentalWebApi.TestModelBuilders.Constants;

namespace ApartmentRentalWebApi.TestModelBuilders.Builders
{
	public class PasswordResetDtoBuilder
	{
		private string _email = UserTestConstants.ValidEmail;

		public PasswordResetDto Build()
		{
			return new PasswordResetDto
			{
				Email = _email
			};
		}

		public PasswordResetDtoBuilder WithEmail(string email)
		{
			_email = email;

			return this;
		}
	}
}