namespace ApartmentRentalWebApi.Business.Core.Dto
{
	public class ErrorDto
	{
		public ErrorDto(string errorMessage)
		{
			this.ErrorMessage = errorMessage;
		}

		public string ErrorMessage { get; set; }
	}
}