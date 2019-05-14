using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ApartmentRentalWebApi.Business.Core.Settings;
using Microsoft.IdentityModel.Tokens;

namespace ApartmentRentalWebApi.Presentation.Utils.Jwt
{
	public static class JwtUtils
	{
		public static string CreateToken(Guid userId, int roleId, AuthenticationSettings settings)
		{
			var claims = new[]
			{
				new Claim(JwtConstants.UserIdClaim, userId.ToString()),
				new Claim(ClaimTypes.Role, roleId.ToString())
			};

			var key = JwtSecurityKey.Create(settings.SigningKey);
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
				issuer: settings.ValidIssuer,
				audience: settings.ValidAudience,
				claims: claims,
				expires: DateTime.Now.AddMinutes(settings.TokenDuration),
				signingCredentials: creds
			);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}