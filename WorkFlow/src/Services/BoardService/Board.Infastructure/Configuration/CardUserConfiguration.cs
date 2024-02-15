using Board.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Board.Infrastructure.Configuration
{
	public class CardUserConfiguration : IEntityTypeConfiguration<CardUser>
	{
		public void Configure(EntityTypeBuilder<CardUser> builder)
		{
			builder.ToTable("CardUsers");

			builder.HasKey(cardUser => new { cardUser.CardId, cardUser.UserId });

			builder.HasOne(cardUser => cardUser.Card)
				.WithMany(card => card.CardUsers)
				.HasForeignKey(cardUser => cardUser.CardId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
