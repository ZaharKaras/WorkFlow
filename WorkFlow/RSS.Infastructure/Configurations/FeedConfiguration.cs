using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RSS.Core.Entities;

namespace RSS.Infastructure.Configurations
{
	public class FeedConfiguration : IEntityTypeConfiguration<Feed>
	{
		public void Configure(EntityTypeBuilder<Feed> builder)
		{
			builder.ToTable("Feeds");

			builder.HasKey(f => f.Id);

			builder.Property(f => f.Title)
				.IsRequired();

			builder.Property(f => f.Link)
				.IsRequired();

			builder.HasMany(feed => feed.Users)
				.WithOne()
				.HasForeignKey(user => user.FeedId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
