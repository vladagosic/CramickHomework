using Microsoft.AspNetCore.Identity;

namespace CramickHomework.Application.Extensions
{
	public static class IdentityResultExtenstions
	{
		public static string GetErrorsText(this IdentityResult? identityResult)
			=>
			identityResult is null ?
			string.Empty :
			string.Join(", ", identityResult.Errors.Select(x => x.Description).ToArray());
	}
}
