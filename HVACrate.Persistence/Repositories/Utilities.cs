using HVACrate.Domain.ValueObjects;
using System.Linq.Expressions;

namespace HVACrate.Persistence.Repositories
{
    internal static class Utilities
    {
        /// <summary>
        /// Applies dynamic ordering to the query based on a property name.
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <param name="query">Queryable data source</param>
        /// <returns>Sorted IQueryable</returns>
        public static IQueryable<T> WithSearch<T>(this IQueryable<T> query,
            string? searchParam, Expression<Func<T, string?>> propertySelector) where T : class
        {
            if (!string.IsNullOrEmpty(searchParam))
            {
                string loweredSearchParam = searchParam.ToLower();

                var parameter = propertySelector.Parameters[0];
                var property = Expression.Invoke(propertySelector, parameter);
                var notNull = Expression.NotEqual(property, Expression.Constant(null, typeof(string)));

                var toLowerCall = Expression.Call(property, nameof(string.ToLower), null);
                var containsCall = Expression.Call(toLowerCall, nameof(string.Contains), null,
                    Expression.Constant(loweredSearchParam));

                var condition = Expression.AndAlso(notNull, containsCall);
                var lambda = Expression.Lambda<Func<T, bool>>(condition, parameter);

                query = query.Where(lambda);
            }

            return query;
        }

        public static IQueryable<T> WithPagination<T>(this IQueryable<T> query, Pagination pagination)
            => query.Skip((pagination.Page - 1) * pagination.Limit).Take(pagination.Limit);
    }
}
