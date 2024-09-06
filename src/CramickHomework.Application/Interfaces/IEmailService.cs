namespace CramickHomework.Application.Interfaces
{
	public interface IEmailService
	{
		Task<bool> SendConfirmationEmail(string email);
		Task<bool> SendResetPasswordEmail(string email);
	}
}
