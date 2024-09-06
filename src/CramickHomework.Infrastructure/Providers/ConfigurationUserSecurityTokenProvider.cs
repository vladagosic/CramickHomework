using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using CramickHomework.Application;
using CramickHomework.Application.Features.Users.Domain;
using CramickHomework.Application.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CramickHomework.Infrastructure.Providers
{
	public class ConfigurationUserSecurityTokenProvider : IUserSecurityTokenProvider
	{
		private readonly IConfiguration _configuration;

        public ConfigurationUserSecurityTokenProvider(
            IConfiguration configuration)
        {
            _configuration = configuration;
		}
		public JwtSecurityToken GetToken(ApplicationUser user)
			=> GetToken(GetUserClaims(user));

		private JwtSecurityToken GetToken(IEnumerable<Claim> claims)
		{
			var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]!));

			var token = new JwtSecurityToken(
				issuer: _configuration["JWT:ValidIssuer"],
				audience: _configuration["JWT:ValidAudience"],
				expires: DateTime.Now.AddMinutes(int.Parse(_configuration["JWT:TokenExpirationMinutes"]!)),
				claims: claims,
				signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

			return token;
		}

		private static IEnumerable<Claim> GetUserClaims(ApplicationUser user)
				=>
				[
					new(Constants.ClaimTypes.Sub, user.Id.ToString()),
					new(ClaimTypes.Name, user.FullName!),
					new(ClaimTypes.Email, user.Email!),
					new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
				];
	}
}
