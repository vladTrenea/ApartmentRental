namespace ApartmentRentalWebApi.Localization.Resources
{
	public interface IErrorMessages
	{
		string EmailRequired { get; }
		string PasswordRequired { get; }
		string InvalidLogin { get; }
		string EmailAlreadyExists { get; }
		string FirstNameRequired { get; }
		string FirstNameOnlyLetters { get; }
		string LastNameRequired { get; }
		string LastNameOnlyLetters { get; }
		string InvalidEmail { get; }
		string InvalidPassword { get; }
		string ConfirmPasswordRequired { get; }
		string ConfirmPasswordMustMatchPassword { get; }
		string RoleIdMandatory { get; }
		string InvalidRole { get; }
		string CannotDeleteActiveRealtor { get; }
		string PasswordTokenExpired { get; }
		string TitleRequired { get; }
		string DescriptionRequired { get; }
		string AreaGreaterThan0 { get; }
		string PricePerMonthGreaterThan0 { get; }
		string NrOfRoomsGreaterThan0 { get; }
		string InvalidLatitude { get; }
		string InvalidLongitude { get; }
		string RealtorIdMandatory { get; }
		string CannotChangeRealtorWithApartments { get; }
	}
}