namespace CramickHomework.Application.Interfaces
{
	public interface IEmailClient
	{
		Task<bool> SendEmailConfirmationEmail(string fullName, string email, string confirmationUrl);
		Task<bool> SendPasswordResetEmail(string fullName, string email, string confirmationUrl);
	}
}
