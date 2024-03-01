using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RSS.Core.Entities;

namespace RSS.Infastructure.Configurations
{
	public class FeedUserConfiguration : IEntityTypeConfiguration<FeedUser>
	{
		public void Configure(EntityTypeBuilder<FeedUser> builder)
		{
			builder.ToTable("FeedsUsers");

			builder.HasKey(feedUser => new {feedUser.FeedId, feedUser.UserId});

			builder.HasOne(feedUser => feedUser.Feed)
				.WithMany(feedUser => feedUser.Users)
				.HasForeignKey(feedUser => feedUser.FeedId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
