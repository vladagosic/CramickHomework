using Newtonsoft.Json;
using CramickHomework.Application.Features.Authentication.Commands;

namespace CramickHomework.Infrastructure.Logging
{
	public static class RequestLogging
	{
		private static readonly Type[] NotLoggedRequestTypes =
		{
			typeof(Login.Request),
			typeof(Register.Request)
		};

		public static bool ShouldRequestTypeBeLogged(Type requestType)
		{
			return NotLoggedRequestTypes.All(x => x != requestType);
		}

		public static string SerializeRequest(object? request)
		{
			return JsonConvert.SerializeObject(request,
				new JsonSerializerSettings()
				{
					ReferenceLoopHandling = ReferenceLoopHandling.Ignore
				});
		}
	}
}
