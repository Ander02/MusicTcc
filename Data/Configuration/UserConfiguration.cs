using Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configuration
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(a => a.Name)
                   .IsRequired()
                   .HasMaxLength(256);

            builder.Property(a => a.Email)
                   .IsRequired()
                   .HasMaxLength(256);

            builder.HasMany(d => d.Ratings)
                   .WithOne(d => d.User)
                   .HasForeignKey(d => d.UserId);
        }
    }
}
