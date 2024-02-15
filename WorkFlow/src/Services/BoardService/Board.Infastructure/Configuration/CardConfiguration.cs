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

			builder.HasKey(entity => entity.Id);

			builder.Property(entity => entity.Title)
				.IsRequired()
				.HasMaxLength(255);

			builder.HasMany(card => card.CardUsers)
				.WithOne()
				.HasForeignKey(cardUser => cardUser.CardId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}

}
