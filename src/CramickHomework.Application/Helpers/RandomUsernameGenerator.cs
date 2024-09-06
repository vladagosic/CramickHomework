namespace CramickHomework.Application.Helpers
{
	public static class RandomUsernameGenerator
	{
		public static string Get()
			=> Guid.NewGuid().ToString().Replace('-', '.');
	}
}
