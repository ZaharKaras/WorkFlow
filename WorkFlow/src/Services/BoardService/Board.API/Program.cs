using Board.Infrastructure;
using Board.Application;
using Microsoft.EntityFrameworkCore;
using Board.API.Middlewares;
using Hangfire;
using StackExchange.Redis;
using Hangfire.Redis.StackExchange;
using Board.API.Email.Interfaces;
using Board.API.Email;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseNpgsql(
		builder.Configuration.GetConnectionString("DefaultConnection")));

// Hangfire client
builder.Services.AddHangfire(configuration => configuration
	.SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
	.UseSimpleAssemblyNameTypeSerializer()
	.UseRecommendedSerializerSettings()
	.UseRedisStorage(builder.Configuration.GetSection("Redis:ConnectionString").Value!));

//Hangfire server
builder.Services.AddHangfireServer();

builder.Services
	.AddInfrastructure()
	.AddApplication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseHangfireDashboard();
app.MapHangfireDashboard("/hangfire");

app.UseAuthorization();

app.MapControllers();

app.Run();
