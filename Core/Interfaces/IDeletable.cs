using System;
using System.Linq;

namespace Core.Interfaces
{
    public interface IDeletable
    {
        DateTime? DeletedAt { get; set; }
    }

    public static class DeletableExtensions
    {
        public static IQueryable<T> OnlyActives<T>(this IQueryable<T> query) where T : IDeletable
            => query.Where(d => !d.DeletedAt.HasValue);

        public static IQueryable<T> OnlyDeleteds<T>(this IQueryable<T> query) where T : IDeletable
            => query.Where(d => d.DeletedAt.HasValue);
    }
}
