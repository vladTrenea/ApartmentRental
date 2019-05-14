using System;

namespace ApartmentRentalWebApi.Domain.Entities
{
	public class Apartment
	{
		public Guid Id { get; set; }

		public string Title { get; set; }

		public string Description { get; set; }

		public double Area { get; set; }

		public double PricePerMonth { get; set; }

		public int NrOfRooms { get; set; }

		public double Latitude { get; set; }

		public double Longitude { get; set; }

		public bool IsRented { get; set; }

		public DateTime CreatedAt { get; set; }

		public Guid RealtorId { get; set; }

		public User Realtor { get; set; }
	}
}