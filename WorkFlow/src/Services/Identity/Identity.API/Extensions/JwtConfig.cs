using Identity.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Identity.API.Extensions
{
	public class JwtConfig : IConfigureOptions<JwtSettings>
	{
		private readonly IConfiguration _config;
		public JwtConfig(IConfiguration config)
		{
			_config = config;
		}
		public void Configure(JwtSettings options)
		{
			_config.GetSection("JwtSettings").Bind(options);
		}
	}
}
