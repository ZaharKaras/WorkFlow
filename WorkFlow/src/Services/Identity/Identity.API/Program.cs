using AspNetCore.Identity.MongoDbCore.Extensions;
using AspNetCore.Identity.MongoDbCore.Infrastructure;
using AspNetCore.Identity.MongoDbCore.Models;
using Identity.Core.Entities;
using Identity.Infrastructure.Data;
using Identity.Infrastructure.Services;
using Identity.Infrastructure.Services.Interfaces;
using Identity.Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
// Add services to the container.
BsonSerializer.RegisterSerializer(new GuidSerializer(MongoDB.Bson.BsonType.String));
BsonSerializer.RegisterSerializer(new DateTimeSerializer(MongoDB.Bson.BsonType.String));
BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(MongoDB.Bson.BsonType.String));

builder.Services.AddSingleton<IRefreshTokenService, RefreshTokenService>();

builder.Services.Configure<DataBaseSettings>(
	builder.Configuration.GetSection("MongoDataBase"));

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

builder.Services.ConfigureMongoDbIdentity<User, MongoIdentityRole<Guid>, Guid>(mongoDbIdentityConfig)
	.AddUserManager<UserManager<User>>()
	.AddDefaultTokenProviders();

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

builder.Services.AddAuthentication(x =>
{
	x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
	x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
	{
		ValidIssuer = config["JwtSettings:Issuer"],
		ValidAudience = config["JwtSettings:Audience"],
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:Key"]!)),
		ValidateIssuer = true,
		ValidateAudience = true,
		ValidateIssuerSigningKey = true,
		ValidateLifetime = true
	};
});

builder.Services.AddAuthorization();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
