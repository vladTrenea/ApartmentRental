using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace ApartmentRentalWebApi.Presentation.Tests.Tests
{
	public class UnauthorizedIntegrationTest : IClassFixture<CustomWebApplicationFactory<Startup>>
	{
		private readonly HttpClient _client;

		public UnauthorizedIntegrationTest(CustomWebApplicationFactory<Startup> factory)
		{
			_client = factory.CreateClient();
		}

		[Theory]
		[InlineData(Constants.RolesUrl)]
		[InlineData(Constants.UsersUrl)]
		[InlineData(Constants.UserUrl)]
		[InlineData(Constants.ApartmentsUrl)]
		[InlineData(Constants.RentableApartmentsUrl)]
		[InlineData(Constants.ApartmentUrl)]
		public async Task NotAuthenticatedGetTests_NoToken_ReturnsUnauthorized(string path)
		{
			var httpResponse = await _client.GetAsync(path);
			httpResponse.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
		}

		[Theory]
		[InlineData(Constants.ApartmentsUrl)]
		[InlineData(Constants.UsersUrl)]
		public async Task NotAuthenticatedPostTests_No_Token_ReturnsUnauthorized(string path)
		{
			var httpResponse = await _client.PostAsync(path, null);
			httpResponse.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
		}

		[Theory]
		[InlineData(Constants.ApartmentUrl)]
		[InlineData(Constants.UserUrl)]
		public async Task NotAuthenticatedPutTests_No_Token_ReturnsUnauthorized(string path)
		{
			var httpResponse = await _client.PutAsync(path, null);
			httpResponse.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
		}

		[Theory]
		[InlineData(Constants.ApartmentUrl)]
		[InlineData(Constants.UserUrl)]
		public async Task NotAuthenticatedDeleteTests_NoToken_ReturnsUnauthorized(string path)
		{
			var httpResponse = await _client.DeleteAsync(path);
			httpResponse.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
		}

		[Fact]
		public async Task NotAuthenticated_InvalidToken_ReturnsUnauthorized()
		{
			var newToken = "asgasga";
			_client.DefaultRequestHeaders.Add(Constants.AuthenticationHeader, Constants.GetTokenHeaderValue(newToken));

			var httpResponse = await _client.GetAsync(Constants.RolesUrl);
			httpResponse.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
		}
	}
}