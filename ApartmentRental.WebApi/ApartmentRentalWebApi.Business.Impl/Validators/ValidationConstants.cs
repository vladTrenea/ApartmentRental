namespace ApartmentRentalWebApi.Business.Impl.Validators
{
	public static class ValidationConstants
	{
		public const string OnlyLettersRegex = @"^[a-zA-Z]+$";
		public const string PasswordRegex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$";
	}
}