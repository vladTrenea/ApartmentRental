using ApartmentRentalWebApi.Business.Core.Dto;
using ApartmentRentalWebApi.TestModelBuilders.Constants;

namespace ApartmentRentalWebApi.TestModelBuilders.Builders
{
	public class AccountDtoBuilder
	{
		private string _firstName = UserTestConstants.ValidFirstName;

		private string _lastName = UserTestConstants.ValidLastName;

		public AccountDto Build()
		{
			return new AccountDto
			{
				LastName = _lastName,
				FirstName = _firstName
			};
		}

		public AccountDtoBuilder WithFirstName(string firstName)
		{
			_firstName = firstName;

			return this;
		}

		public AccountDtoBuilder WithLastName(string lastName)
		{
			_lastName = lastName;

			return this;
		}
	}
}