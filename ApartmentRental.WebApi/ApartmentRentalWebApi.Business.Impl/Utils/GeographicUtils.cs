namespace ApartmentRentalWebApi.Business.Impl.Utils
{
	public class GeographicUtils
	{
		public static bool IsPointWithin(double southWestLatitude, double southWestLongitude, 
			double northEastLatitude, double northEastLongitude, 
			double newPointLatitude, double newPointLongitude)
		{
			return newPointLatitude >= southWestLatitude &&
			       newPointLatitude <= northEastLatitude &&
			       newPointLongitude >= southWestLongitude &&
			       newPointLongitude <= northEastLongitude;
		}
	}
}