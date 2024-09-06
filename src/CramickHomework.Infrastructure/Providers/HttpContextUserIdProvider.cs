using Microsoft.AspNetCore.Http;
using CramickHomework.Application;
using CramickHomework.Application.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace CramickHomework.Infrastructure.Providers
{
	public class HttpContextUserIdProvider : ICurrentUserIdProvider
	{
		private readonly IHttpContextAccessor _httpContextAccessor;

		public HttpContextUserIdProvider(
			IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		public Guid? GetUserId()
		{
			if (Guid.TryParse(GetUserIdClaimValue(), out var userId))
			{
				return userId;
			}

			return null;
		}
		private string? GetUserIdClaimValue()
		{
			if (_httpContextAccessor.HttpContext is null)
				return null;

			if (_httpContextAccessor.HttpContext.Request.Headers.TryGetValue("Authorization", out var authHeaders))
			{
				var token = authHeaders.FirstOrDefault()?.Split(" ").LastOrDefault();

				if (string.IsNullOrEmpty(token))
					return null;

				var securityToken = new JwtSecurityTokenHandler().ReadJwtToken(token);

				return securityToken.Payload.Claims.FirstOrDefault(x => x.Type == Constants.ClaimTypes.Sub)?.Value;
			}

			return null;
		}
	}
}
