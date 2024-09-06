using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace CramickHomework.Infrastructure.Extensions
{
	public static class HttpContextExtensions
	{
		public static T Resolve<T>(this HttpContext httpContext)
		{
			return httpContext.RequestServices.GetService<T>()
				?? throw new InvalidOperationException($"Cannot resolve the requested service {typeof(T).Name} from current HttpContext.");

		}

		public static string? BaseUrl(this HttpRequest req)
		{
			if (req == null) return null;
			var uriBuilder = new UriBuilder(req.Scheme, req.Host.Host, req.Host.Port ?? -1);
			if (uriBuilder.Uri.IsDefaultPort)
			{
				uriBuilder.Port = -1;
			}

			return uriBuilder.Uri.AbsoluteUri;
		}
	}
}
