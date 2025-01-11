using Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configuration
{
    internal class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(a => a.Name)
                   .IsRequired()
                   .HasMaxLength(256);

            builder.HasMany(d => d.Musics)
                   .WithMany(d => d.Genres);
        }
    }
}
