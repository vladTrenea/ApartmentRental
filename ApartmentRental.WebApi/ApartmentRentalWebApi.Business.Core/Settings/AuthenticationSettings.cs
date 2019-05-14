namespace ApartmentRentalWebApi.Business.Core.Settings
{
	public class AuthenticationSettings
	{
		public int TokenDuration { get; set; }

		public string ValidIssuer { get; set; }

		public string ValidAudience { get; set; }

		public string SigningKey { get; set; }
	}
}