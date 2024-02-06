using Identity.Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Identity.API.Extensions
{
	public class JwtBearerConfig : IConfigureOptions<JwtBearerOptions>
	{
		private readonly JwtSettings _jwtSettings;

		public JwtBearerConfig(IOptions<JwtSettings> jwtSettings)
		{
			_jwtSettings = jwtSettings.Value;
		}

		public void Configure(JwtBearerOptions options)
		{
			options.TokenValidationParameters = new TokenValidationParameters()
			{
				ValidateIssuer = false,
				ValidateAudience = false,
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key)),
				ValidateLifetime = true
			};
		}
	}
}
