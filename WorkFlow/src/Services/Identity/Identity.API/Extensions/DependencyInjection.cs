using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using AspNetCore.Identity.MongoDbCore.Infrastructure;
using AspNetCore.Identity.MongoDbCore.Models;
using Identity.Core.Entities;
using Identity.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using AspNetCore.Identity.MongoDbCore.Extensions;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Identity.API.Extensions
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddMongoDb(
			this IServiceCollection services,
			IConfiguration config)
		{
			BsonSerializer.RegisterSerializer(new GuidSerializer(MongoDB.Bson.BsonType.String));
			BsonSerializer.RegisterSerializer(new DateTimeSerializer(MongoDB.Bson.BsonType.String));
			BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(MongoDB.Bson.BsonType.String));

			services.Configure<DataBaseSettings>(
				config.GetSection("MongoDataBase"));

			var mongoDbIdentityConfig = new MongoDbIdentityConfiguration
			{
				MongoDbSettings = new MongoDbSettings
				{
					ConnectionString = config["MongoDataBase:ConnectionString"],
					DatabaseName = config["MongoDataBase:DataBaseName"]
				},
				IdentityOptionsAction = options =>
				{
					options.Password.RequireDigit = false;
					options.Password.RequiredLength = 8;
					options.Password.RequireNonAlphanumeric = true;
					options.Password.RequireLowercase = false;

					//lockout
					options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
					options.Lockout.MaxFailedAccessAttempts = 5;

					options.User.RequireUniqueEmail = true;
				}
			};

			services.ConfigureMongoDbIdentity<User, MongoIdentityRole<Guid>, Guid>(mongoDbIdentityConfig)
				.AddUserManager<UserManager<User>>()
				.AddDefaultTokenProviders();

			return services;
		}

		public static IServiceCollection AddJwtToken(
			this IServiceCollection services, 
			IConfiguration config)
		{
			var tokenValidationParameter = new TokenValidationParameters()
			{
				ValidateIssuer = false,
				ValidateAudience = false,
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:Key"]!)),
				ValidateLifetime = true
			};

			services.AddSingleton(tokenValidationParameter);
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer();

			services.ConfigureOptions<JwtConfig>();
			services.ConfigureOptions<JwtBearerConfig>();

			return services;
		}
	}
}
