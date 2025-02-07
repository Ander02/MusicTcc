using Core;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Data
{
    public class MusicDbContext : DbContext
    {
        public DbSet<Album> Albums { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Music> Musics { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Rating> Ratings { get; set; }

        public MusicDbContext() { }

        public MusicDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MusicDbContext).Assembly);

            ConfigureDefaultProperties(modelBuilder);
        }

        #region Set CreatedAt and UpdatedAt Property

        /// <summary>
        /// Iterate on each IEntity and set default properties
        /// </summary>
        /// <param name="modelBuilder"></param>
        public void ConfigureDefaultProperties(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var entityClrType = entityType.ClrType;

                if (typeof(IEntity).IsAssignableFrom(entityClrType))
                {
                    var createdAtAndUpdatedAtProperty = typeof(MusicDbContext).GetMethods()
                                                         .Single(t => t.Name == nameof(SetCreatedAtAndUpdatedAtPropertyOnAdd));

                    var method = createdAtAndUpdatedAtProperty.MakeGenericMethod(entityClrType);

                    method.Invoke(this, [modelBuilder]);
                }
            }
        }

        /// <summary>
        /// Adds to the entity the behavior of filling date information on create or update
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder"></param>
        public static void SetCreatedAtAndUpdatedAtPropertyOnAdd<T>(ModelBuilder builder) where T : class, IEntity
        {
            builder.Entity<T>().Property(d => d.CreatedAt).HasDefaultValueSql("GetDate()");
            builder.Entity<T>().Property(d => d.UpdatedAt).HasDefaultValueSql("GetDate()");
        }

        #endregion
    }
}
