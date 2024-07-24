using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TestTask.Application.Implementation.Services;

namespace TestTask.WebApi.Extensions;

public static class Registrator
{
	private static readonly string Description =
		$"JWT Authorization header using the {JwtBearerDefaults.AuthenticationScheme} scheme. \r\n\r\n Enter '{JwtBearerDefaults.AuthenticationScheme}' [space] [your token value].";

	public static IServiceCollection AddSwaggerWithJwt(this IServiceCollection collection)
	{
		return 
		collection.AddSwaggerGen(swagger =>
		{
			swagger.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
			{
				Name = "Authorization",
				Type = SecuritySchemeType.ApiKey,
				Scheme = JwtBearerDefaults.AuthenticationScheme,
				BearerFormat = "JWT",
				In = ParameterLocation.Header,
				Description = Description
			});

			swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
			{
				{
					new OpenApiSecurityScheme
					{
						Reference = new OpenApiReference
						{
							Type = ReferenceType.SecurityScheme,
							Id = JwtBearerDefaults.AuthenticationScheme
						}
					},
					Array.Empty<string>()
				}
			});
		});
	}

	public static IServiceCollection AddAuthenticationWithJwtBearer(this IServiceCollection collection, IConfiguration configuration)
	{
		collection.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));
		var jwtOptions = configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>()!;

		var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey));

		collection.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
		.AddJwtBearer(options =>
		{
			options.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuer = true,
				ValidateAudience = true,
				ValidateLifetime = true,
				ValidateIssuerSigningKey = true,
				ValidIssuer = jwtOptions.Issuer,
				ValidAudience = jwtOptions.Audience,
				IssuerSigningKey = signingKey,
				ClockSkew = TimeSpan.FromSeconds(5),
			};
		});

		return collection;
	}
}