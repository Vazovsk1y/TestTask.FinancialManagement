using System.Globalization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TestTask.Application.Services;
using TestTask.Domain.Entities;

namespace TestTask.Application.Implementation.Services;

internal class JwtTokenProvider(IOptions<JwtOptions> jwtOptions) : ITokenProvider
{
	private readonly JwtOptions _jwtOptions = jwtOptions.Value;

	public string GenerateToken(User user)
	{
		var expiredDate = DateTime.UtcNow.AddHours(_jwtOptions.LifetimeHoursCount);
		var roles = user.Roles.Select(e => new Claim(ClaimTypes.Role, e.Role!.Title));

		var claims = new List<Claim>(roles)
		{
			new(JwtRegisteredClaimNames.Email, user.Email),
			new(JwtRegisteredClaimNames.Sub, user.Id.Value.ToString()),
			new(JwtRegisteredClaimNames.Exp, expiredDate.ToString(CultureInfo.InvariantCulture)),
		};

		var signingCredentials = new SigningCredentials(
			new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)),
			SecurityAlgorithms.HmacSha256
			);

		var token = new JwtSecurityToken(
			_jwtOptions.Issuer,
			_jwtOptions.Audience,
			claims,
			null,
			expiredDate,
			signingCredentials
			);

		string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

		return tokenValue;
	}
}

public record JwtOptions
{
	public const string SectionName = "Jwt";
	public required string Audience { get; init; }
	public required string Issuer { get; init; }
	public required string SecretKey { get; init; }
	public required int LifetimeHoursCount { get; init; }
}