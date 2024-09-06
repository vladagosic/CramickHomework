using System.Text;

namespace CramickHomework.Application.Helpers
{
	public static class TokenHelper
	{
		public static string TokenEncode(string token) =>
			Convert.ToBase64String(Encoding.UTF8.GetBytes(token));

		public static string TokenDecode(string token) =>
			Encoding.UTF8.GetString(Convert.FromBase64String(token));
	}
}
