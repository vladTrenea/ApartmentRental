using System.Threading.Tasks;

namespace ApartmentRentalWebApi.Business.Core.Services
{
	public interface IEmailService
	{
		Task SendAccountConfirmationEmail(string email, string token);

		Task SendAccountCreationEmail(string email, string token);

		Task SendPasswordResetEmail(string email, string token);
	}
}