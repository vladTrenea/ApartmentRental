using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ApartmentRentalWebApi.Domain.Entities
{
	public class User
	{
		public User()
		{
			ManagedApartments = new Collection<Apartment>();
		}

		public Guid Id { get; set; }

		public string Email { get; set; }

		public string Password { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public bool EmailConfirmed { get; set; }

		public string EmailConfirmationToken { get; set; }

		public string PasswordResetToken { get; set; }

		public DateTime? PasswordResetEndDate { get; set; }

		public DateTime CreatedAt { get; set; }

		public DateTime? ConfirmedAt { get; set; }

		public int RoleId { get; set; }

		public Role Role { get; set; }

		public ICollection<Apartment> ManagedApartments { get; set; }
	}
}