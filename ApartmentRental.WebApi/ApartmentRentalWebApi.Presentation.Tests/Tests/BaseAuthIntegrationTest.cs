using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using ApartmentRentalWebApi.Business.Core.Dto;
using ApartmentRentalWebApi.Data;
using Newtonsoft.Json;
using Xunit;

namespace ApartmentRentalWebApi.Presentation.Tests.Tests
{
	public class BaseAuthIntegrationTest : IClassFixture<CustomWebApplicationFactory<Startup>>
	{
		protected readonly HttpClient Client;

		public BaseAuthIntegrationTest(CustomWebApplicationFactory<Startup> factory)
		{
			Client = factory.CreateClient();
		}

		protected async Task AuthenticateAsUser()
		{
			await Authenticate(DbInitializer.User.Email, DbInitializer.TestPassword);
		}

		protected async Task AuthenticateAsRealtor()
		{
			await Authenticate(DbInitializer.Realtor.Email, DbInitializer.TestPassword);
		}

		protected async Task AuthenticateAsAdmin()
		{
			await Authenticate(DbInitializer.Admin.Email, DbInitializer.TestPassword);
		}

		private async Task Authenticate(string email, string password)
		{
			var loginDto = new LoginDto
			{
				Email = email,
				Password = password
			};
			var response = await Client.PostAsync(Constants.LoginUrl, loginDto, new JsonMediaTypeFormatter());
			response.EnsureSuccessStatusCode();
			var stringResponse = await response.Content.ReadAsStringAsync();
			var authDto = JsonConvert.DeserializeObject<AuthenticationDto>(stringResponse);

			Client.DefaultRequestHeaders.Add("Authorization", "Bearer " + authDto.Token);
		}
	}
}