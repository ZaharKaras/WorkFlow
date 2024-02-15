using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Board.Infrastructure.Configuration
{
	public class BoardConfiguration : IEntityTypeConfiguration<Core.Entities.Board>
	{
		public void Configure(EntityTypeBuilder<Core.Entities.Board> builder)
		{
			builder.ToTable("Boards");

			builder.HasKey(entity => entity.Id);

			builder.Property(entity => entity.Name)
				.IsRequired()
				.HasMaxLength(255);

			builder.HasMany(board => board.BoardUsers)
				.WithOne()
				.HasForeignKey(boardUser => boardUser.BoardId)
				.OnDelete(DeleteBehavior.Cascade); 
		}
	}
	
}
