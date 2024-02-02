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

			builder.HasKey(cu => new { cu.CardId, cu.UserId });

			builder.HasOne(cu => cu.Card)
				.WithMany(c => c.CardUsers)
				.HasForeignKey(cu => cu.CardId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
