using CramickHomework.Application.Features.Users.Domain;
using System.IdentityModel.Tokens.Jwt;

namespace CramickHomework.Application.Interfaces
{
	public interface IUserSecurityTokenProvider
	{
		JwtSecurityToken GetToken(ApplicationUser user);
	}
}
