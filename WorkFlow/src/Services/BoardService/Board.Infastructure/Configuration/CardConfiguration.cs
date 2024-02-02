using Board.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Board.Infrastructure.Configuration
{
	public class CardConfiguration : IEntityTypeConfiguration<Card>
	{
		public void Configure(EntityTypeBuilder<Card> builder)
		{
			builder.ToTable("Cards");

			builder.HasKey(e => e.Id);

			builder.Property(e => e.Title)
				.IsRequired()
				.HasMaxLength(255);

			builder.HasMany(c => c.CardUsers)
				.WithOne()
				.HasForeignKey(cu => cu.CardId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}

}
