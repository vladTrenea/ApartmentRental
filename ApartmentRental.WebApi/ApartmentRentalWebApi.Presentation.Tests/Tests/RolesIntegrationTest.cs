using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ApartmentRentalWebApi.Business.Core.Dto;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

namespace ApartmentRentalWebApi.Presentation.Tests.Tests
{
	public class RolesIntegrationTest: BaseAuthIntegrationTest
	{
		public RolesIntegrationTest(CustomWebApplicationFactory<Startup> factory) : base(factory)
		{
		}

		[Fact]
		public async Task GetRoles_Admin_Success()
		{
			await AuthenticateAsAdmin();
			var httpResponse = await Client.GetAsync(Constants.RolesUrl);
			httpResponse.EnsureSuccessStatusCode();

			var stringResponse = await httpResponse.Content.ReadAsStringAsync();
			var roles = JsonConvert.DeserializeObject<IEnumerable<RoleDto>>(stringResponse);
			roles.Should().NotBeNull();
			roles.Should().BeAssignableTo<IEnumerable<RoleDto>>();
			roles.Count().Should().NotBe(0);
		}

		[Fact]
		public async Task GetRoles_Realtor_Forbidden()
		{
			await AuthenticateAsRealtor();
			var httpResponse = await Client.GetAsync(Constants.RolesUrl);
			httpResponse.StatusCode.Should().Be(HttpStatusCode.Forbidden);
		}

		[Fact]
		public async Task GetRoles_User_Forbidden()
		{
			await AuthenticateAsUser();
			var httpResponse = await Client.GetAsync(Constants.RolesUrl);
			httpResponse.StatusCode.Should().Be(HttpStatusCode.Forbidden);
		}
	}
}