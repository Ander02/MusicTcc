using Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configuration
{
    internal class MusicConfiguration : IEntityTypeConfiguration<Music>
    {
        public void Configure(EntityTypeBuilder<Music> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(m => m.Title)
               .IsRequired()
               .HasMaxLength(256);

            builder.HasMany(m => m.Albums)
                   .WithMany(a => a.Musics);

            builder.HasMany(m => m.Genres)
                   .WithMany(g => g.Musics);
        }
    }
}
