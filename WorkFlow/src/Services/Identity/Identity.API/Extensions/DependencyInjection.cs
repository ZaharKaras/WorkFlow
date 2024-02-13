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
using Identity.Infrastructure.Settings;
using Identity.Infrastructure.Services.Interfaces;
using Identity.Infrastructure.Services;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

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

			services.AddSwaggerGen(options =>
			{
				options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
				{
					In = ParameterLocation.Header,
					Name = "Authorization",
					Type = SecuritySchemeType.ApiKey
				});

				options.OperationFilter<SecurityRequirementsOperationFilter>();
			});

			services.AddSingleton(tokenValidationParameter);
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer();

			services.Configure<JwtSettings>(config.GetSection("JwtSettings"));
			services.ConfigureOptions<JwtBearerConfig>();

			return services;
		}

		public static IServiceCollection AddMapper(this IServiceCollection services)
		{
			services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

			return services;
		}

		public static IServiceCollection AddServices(this IServiceCollection services)
		{
			services.AddScoped<IRefreshTokenService, RefreshTokenService>();
			services.AddScoped<ITokenService, TokenService>();
			services.AddScoped<IIdentityService, IdentityService>();
			services.AddScoped<IUserService, UserService>();

			return services;
		}

		public static IServiceCollection AddCache(this IServiceCollection services, IConfiguration config)
		{
			services.AddStackExchangeRedisCache(redisOptions =>
			{
				string connection = config.GetSection("Redis:ConnectionString").Value!;

				redisOptions.Configuration = connection;
			});

			return services;
		}
	}
}
