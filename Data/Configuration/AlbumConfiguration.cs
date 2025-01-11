using Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configuration
{
    internal class AlbumConfiguration : IEntityTypeConfiguration<Album>
    {
        public void Configure(EntityTypeBuilder<Album> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(a => a.Name)
                   .IsRequired()
                   .HasMaxLength(256);

            builder.Property(a => a.ReleasedAt).IsRequired();

            builder.HasMany(d => d.Artists)
                   .WithMany(d => d.Albums);

            builder.HasMany(d => d.Musics)
                   .WithMany(d => d.Albums);
        }
    }
}
