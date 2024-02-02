using Board.Core.Entities;
using Board.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Board.Infrastructure
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
			Database.EnsureCreated();
		}

		public DbSet<Core.Entities.Board> Boards { get; set; }
		public DbSet<Card> Cards { get; set; }
		public DbSet<BoardUser> BoardsUsers { get; set; }
		public DbSet<CardUser> CardUsers { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new BoardConfiguration());
			modelBuilder.ApplyConfiguration(new CardConfiguration());
			modelBuilder.ApplyConfiguration(new BoardUserConfiguration());
			modelBuilder.ApplyConfiguration(new CardUserConfiguration());
		}

	}
}
