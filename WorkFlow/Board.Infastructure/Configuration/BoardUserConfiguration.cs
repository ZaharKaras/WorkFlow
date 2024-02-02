using Board.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Board.Infrastructure.Configuration
{
	public class BoardUserConfiguration : IEntityTypeConfiguration<BoardUser>
	{
		public void Configure(EntityTypeBuilder<BoardUser> builder)
		{
			builder.ToTable("BoardUsers");

			builder.HasKey(bu => new { bu.BoardId, bu.UserId });

			builder.HasOne(bu => bu.Board)
				.WithMany(b => b.BoardUsers)
				.HasForeignKey(bu => bu.BoardId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}

}
