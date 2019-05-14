namespace ApartmentRentalWebApi.Presentation.Tests
{
	public static class Constants
	{
		public const string BaseUrl = "/api";
		public const string RolesUrl = BaseUrl + "/roles";
		public const string UsersUrl = BaseUrl + "/users";
		public const string UserUrl = UsersUrl + "/{0}";
		public const string ApartmentsUrl = BaseUrl + "/apartments";
		public const string RentableApartmentsUrl = ApartmentsUrl + "/rentable";
		public const string ApartmentUrl = ApartmentsUrl + "/{0}";
		public const string DeleteApartmentUrl = ApartmentsUrl + "/{0}";
		public const string LoginUrl = BaseUrl + "/auth/login";
		public const string AuthUrl = BaseUrl + "/auth";

		public const string AuthenticationHeader = "Authorization";

		public static string GetTokenHeaderValue(string token)
		{
			return "Bearer " + token;
		}
	}
}