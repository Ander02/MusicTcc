using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Business.Models.Pagination
{
    public static class PagedResultExtensions
    {
        public static IQueryable<T> PaginateBy<T, T2, TPageRequest>(this IQueryable<T> requests, PagedRequest<TPageRequest> pageRequest, Expression<Func<T, T2>> defaultOrdering) where TPageRequest : class
        {
            if (pageRequest.HasSortingData) return requests.PaginateBy(pageRequest);
            pageRequest.SortDirection = SortDirection.Ascending;

            try
            {
                if (defaultOrdering.Body is MemberExpression)
                {
                    pageRequest.Field = (defaultOrdering.Body as MemberExpression).Member.Name;
                }
                else
                {
                    var operand = (defaultOrdering.Body as UnaryExpression).Operand;
                    pageRequest.Field = (operand as MemberExpression).Member.Name;
                }
            }
            catch (Exception) { }

            return requests.OrderBy(defaultOrdering).Skip(pageRequest.PageSize * (pageRequest.Page - 1)).Take(pageRequest.PageSize);
        }

        public static IQueryable<T> PaginateByDescending<T, T2, TPageRequest>(this IQueryable<T> requests, PagedRequest<TPageRequest> pageRequest, Expression<Func<T, T2>> defaultOrdering) where TPageRequest : class
        {
            if (pageRequest.HasSortingData) return requests.PaginateBy(pageRequest);
            pageRequest.SortDirection = SortDirection.Descending;

            return requests.OrderByDescending(defaultOrdering).Skip(pageRequest.PageSize * (pageRequest.Page - 1)).Take(pageRequest.PageSize);
        }

        public static IQueryable<T> PaginateBy<T, TPageRequest>(this IQueryable<T> requests, PagedRequest<TPageRequest> pageRequest) where TPageRequest : class
        {
            if (pageRequest?.Field == null) throw new ArgumentNullException($"{nameof(pageRequest)}.{nameof(pageRequest.Field)}");

            if (pageRequest?.SortDirection.HasValue != true) throw new ArgumentNullException($"{nameof(pageRequest)}.{nameof(pageRequest.SortDirection)}");

            try
            {
                var propInfo = typeof(T).GetProperties().Where(d => d.Name.ToLowerInvariant() == pageRequest?.Field?.ToLowerInvariant()).FirstOrDefault();

                if (propInfo == null) throw new ArgumentNullException($"Invalid sorting field {pageRequest?.Field}");

                ParameterExpression arg = Expression.Parameter(typeof(T), "SortingProperty");
                MemberExpression property = Expression.Property(arg, pageRequest.Field);
                var selector = Expression.Lambda(property, new ParameterExpression[] { arg });

                var methodName = pageRequest.SortDirection.Value == SortDirection.Ascending ? nameof(Enumerable.OrderBy) : nameof(Enumerable.OrderByDescending);

                var method = typeof(Queryable).GetMethods()
                                              .Where(m => m.Name == methodName)
                                              .Where(m => m.GetParameters().Length == 2)
                                              .Single();

                MethodInfo memberInfo = method.MakeGenericMethod(typeof(T), propInfo.PropertyType);

                return ((IOrderedQueryable<T>)memberInfo.Invoke(memberInfo, new object[] { requests, selector }))
                            .Skip(pageRequest.PageSize * (pageRequest.Page - 1)).Take(pageRequest.PageSize);
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException($"Invalid sorting field {pageRequest.Field}", ex);
            }
        }
    }
}
