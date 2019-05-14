using System;

namespace ApartmentRentalWebApi.Business.Impl.Utils
{
	public static class SecurityUtils
	{
		public const int PasswordResetTokenDuration = 24; //hours

		public static Guid GenerateUserId()
		{
			return Guid.NewGuid();
		}

		public static Guid GenerateApartmentId()
		{
			return Guid.NewGuid();
		}

		public static string GenerateEmailConfirmationToken()
		{
			return Guid.NewGuid().ToString();
		}

		public static string GeneratePasswordResetToken()
		{
			return Guid.NewGuid().ToString();
		}
	}
}