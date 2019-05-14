using System;
using Microsoft.EntityFrameworkCore;

using ApartmentRentalWebApi.Data;

namespace ApartmentRentalWebApi.Business.Tests.Services.Role
{
	public class RoleServiceTest
	{
		protected readonly DbContextOptions<ApartmentRentalDbContext> DbContextOptions;

		public RoleServiceTest()
		{
			DbContextOptions = new DbContextOptionsBuilder<ApartmentRentalDbContext>()
				.UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
		}
	}
}