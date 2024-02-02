using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Board.Infrastructure.Configuration
{
	public class BoardConfiguration : IEntityTypeConfiguration<Core.Entities.Board>
	{
		public void Configure(EntityTypeBuilder<Core.Entities.Board> builder)
		{
			builder.ToTable("Boards");

			builder.HasKey(e => e.Id);

			builder.Property(e => e.Name)
				.IsRequired()
				.HasMaxLength(255);

			builder.HasMany(b => b.BoardUsers)
				.WithOne()
				.HasForeignKey(bu => bu.BoardId)
				.OnDelete(DeleteBehavior.Cascade); 
		}
	}
	
}
