namespace ApartmentRentalWebApi.TestModelBuilders.Constants
{
	public static class ApartmentTestConstants
	{
		public const string ValidTitle = "Apartment for rent";
		public const string ValidDescription = "Super apartment situated in the center of the city";
		public const double ValidLatitude = 45.21;
		public const double ValidLongitude = 93;
		public const int ValidNrOfRooms = 3;
		public const double ValidArea = 121.50;
		public const double ValidPricePerMonth = 210;
		public const double InvalidLatitudeLessThanMinValue = -100;
		public const double InvalidLatitudeGreaterThanMaxValue = 100;
		public const double InvalidLongitudeGreaterThanMaxValue = 200;
		public const double InvalidLongitudeLessThanMinValue = -200;
		public const string ValidationMessage = "Validation message";
	}
}