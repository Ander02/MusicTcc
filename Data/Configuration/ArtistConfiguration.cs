using Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configuration
{
    internal class ArtistConfiguration : IEntityTypeConfiguration<Artist>
    {
        public void Configure(EntityTypeBuilder<Artist> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(a => a.Name)
                   .IsRequired()
                   .HasMaxLength(256);

            builder.HasMany(d => d.Albums)
                   .WithMany(d => d.Artists);
        }
    }
}
