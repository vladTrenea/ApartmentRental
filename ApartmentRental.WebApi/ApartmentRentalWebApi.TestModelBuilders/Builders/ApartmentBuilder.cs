using System;

using ApartmentRentalWebApi.Domain.Entities;
using ApartmentRentalWebApi.TestModelBuilders.Constants;

namespace ApartmentRentalWebApi.TestModelBuilders.Builders
{
	public class ApartmentBuilder
	{
		private Guid _id = Guid.NewGuid();
		private string _title = ApartmentTestConstants.ValidTitle;
		private string _description = ApartmentTestConstants.ValidDescription;
		private double _area = ApartmentTestConstants.ValidArea;
		private int _nrOfRooms = ApartmentTestConstants.ValidNrOfRooms;
		private double _pricePerMonth = ApartmentTestConstants.ValidPricePerMonth;
		private double _latitude = ApartmentTestConstants.ValidLatitude;
		private double _longitude = ApartmentTestConstants.ValidLongitude;
		private bool _isRented;
		private readonly DateTime _createdAt = DateTime.Now;
		private Guid _realtorId;

		public Apartment Build()
		{
			return new Apartment
			{
				Id = _id,
				Title = _title,
				Description = _description,
				CreatedAt =_createdAt,
				Area = _area,
				NrOfRooms = _nrOfRooms,
				PricePerMonth = _pricePerMonth,
				Latitude = _latitude,
				Longitude = _longitude,
				IsRented = _isRented,
				RealtorId = _realtorId
			};
		}

		public ApartmentBuilder WithRealtorId(Guid realtorId)
		{
			_realtorId = realtorId;

			return this;
		}

		public ApartmentBuilder WithIsRented(bool isRented)
		{
			_isRented = isRented;

			return this;
		}

		public ApartmentBuilder WithNrOfRooms(int nrOfRooms)
		{
			_nrOfRooms = nrOfRooms;

			return this;
		}

		public ApartmentBuilder WithPricePerMonth(double pricePerMonth)
		{
			_pricePerMonth = pricePerMonth;

			return this;
		}

		public ApartmentBuilder WithArea(double area)
		{
			_area = area;

			return this;
		}

		public ApartmentBuilder WithLatitude(double latitude)
		{
			_latitude = latitude;

			return this;
		}

		public ApartmentBuilder WithLongitude(double longitude)
		{
			_longitude = longitude;

			return this;
		}
	}
}