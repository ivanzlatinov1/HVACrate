using HVACrate.Domain.ValueObjects;

namespace HVACrate.Persistence.Repositories.Projects
{
    internal static class Utilities
    {
        public static IQueryable<Project> WithSearch(this IQueryable<Project> query, string? searchParam)
        {
            if (!string.IsNullOrEmpty(searchParam))
            {
                string loweredSearchParam = searchParam.ToLower();
                query = query.Where(x => x.Name != null && x.Name.ToLower().Contains(loweredSearchParam));
            }

            return query;
        }

        public static IQueryable<Project> WithPagination(this IQueryable<Project> query, Pagination pagination)
            => query.Skip((pagination.Page - 1) * pagination.Limit).Take(pagination.Limit);
    }
}
