using Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configuration
{
    internal class RatingConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(d => d.User)
                   .WithMany(d => d.Ratings)
                   .HasForeignKey(d => d.UserId);

            builder.HasOne(d => d.Music)
                   .WithMany(d => d.Ratings)
                   .HasForeignKey(d => d.MusicId);
        }
    }
}
