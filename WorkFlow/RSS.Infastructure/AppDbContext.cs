using Microsoft.EntityFrameworkCore;
using RSS.Core.Entities;
using System.Reflection;

namespace RSS.Infastructure
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
			Database.EnsureCreated();
		}

		public DbSet<Feed> Feeds { get; set; }
		public DbSet<FeedUser> FeedsUsers { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}

	}
}
