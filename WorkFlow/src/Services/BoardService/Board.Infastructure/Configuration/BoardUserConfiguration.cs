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

			builder.HasKey(boardUser => new { boardUser.BoardId, boardUser.UserId });

			builder.HasOne(boardUser => boardUser.Board)
				.WithMany(board => board.BoardUsers)
				.HasForeignKey(boardUser => boardUser.BoardId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}

}
