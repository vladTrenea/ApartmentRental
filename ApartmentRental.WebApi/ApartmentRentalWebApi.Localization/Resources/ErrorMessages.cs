using Microsoft.Extensions.Localization;

namespace ApartmentRentalWebApi.Localization.Resources
{
	public class ErrorMessages : BaseResourceMessages<ErrorMessages>, IErrorMessages
	{
		public ErrorMessages(IStringLocalizer<ErrorMessages> localizer) : base(localizer)
		{
			
		}

		public string EmailRequired => GetString(nameof(EmailRequired));
		public string PasswordRequired => GetString(nameof(PasswordRequired));
		public string InvalidLogin => GetString(nameof(InvalidLogin));
		public string EmailAlreadyExists => GetString(nameof(EmailAlreadyExists));
		public string FirstNameRequired => GetString(nameof(FirstNameRequired));
		public string FirstNameOnlyLetters => GetString(nameof(FirstNameOnlyLetters));
		public string LastNameRequired => GetString(nameof(LastNameRequired));
		public string LastNameOnlyLetters => GetString(nameof(LastNameOnlyLetters));
		public string InvalidEmail => GetString(nameof(InvalidEmail));
		public string InvalidPassword => GetString(nameof(InvalidPassword));
		public string ConfirmPasswordRequired => GetString(nameof(ConfirmPasswordRequired));
		public string ConfirmPasswordMustMatchPassword => GetString(nameof(ConfirmPasswordMustMatchPassword));
		public string RoleIdMandatory => GetString(nameof(RoleIdMandatory));
		public string InvalidRole => GetString(nameof(InvalidRole));
		public string CannotDeleteActiveRealtor => GetString(nameof(CannotDeleteActiveRealtor));
		public string PasswordTokenExpired => GetString(nameof(PasswordTokenExpired));
		public string TitleRequired => GetString(nameof(TitleRequired));
		public string DescriptionRequired => GetString(nameof(DescriptionRequired));
		public string AreaGreaterThan0 => GetString(nameof(AreaGreaterThan0));
		public string PricePerMonthGreaterThan0 => GetString(nameof(PricePerMonthGreaterThan0));
		public string NrOfRoomsGreaterThan0 => GetString(nameof(NrOfRoomsGreaterThan0));
		public string InvalidLatitude => GetString(nameof(InvalidLatitude));
		public string InvalidLongitude => GetString(nameof(InvalidLongitude));
		public string RealtorIdMandatory => GetString(nameof(RealtorIdMandatory));
		public string CannotChangeRealtorWithApartments => GetString(nameof(CannotChangeRealtorWithApartments));
	}
}