namespace ApartmentRentalWebApi.Business.Core.Dto
{
	public class ApartmentFilterDto
	{
		public double? MinPrice { get; set; }

		public double? MaxPrice { get; set; }

		public double? MinArea { get; set; }

		public double? MaxArea { get; set; }

		public int? MinNrRooms { get; set; }

		public int? MaxNrRooms { get; set; }

		public double NorthEastLatitude { get; set; }

		public double NorthEastLongitude { get; set; }

		public double SouthWestLatitude { get; set; }

		public double SouthWestLongitude { get; set; }
	}
}