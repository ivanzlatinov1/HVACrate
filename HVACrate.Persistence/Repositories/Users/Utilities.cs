using HVACrate.Domain.Enums;
using HVACrate.Domain.ValueObjects;

namespace HVACrate.Persistence.Repositories.Users
{
    public static class Utilities
    {
        public static IQueryable<HVACUser> WithSorting(this IQueryable<HVACUser> query, HVACUserSorting? sorting = null)
            => sorting switch
            {
                { Type: HVACUserSortingType.RegisteredOn, Direction: SortingDirection.Ascending } => query.OrderBy(u => u.RegisteredOn),
                { Type: HVACUserSortingType.RegisteredOn, Direction: SortingDirection.Descending } => query.OrderByDescending(u => u.RegisteredOn),
                { Type: HVACUserSortingType.Username, Direction: SortingDirection.Ascending } => query.OrderBy(u => u.Username),
                { Type: HVACUserSortingType.Username, Direction: SortingDirection.Descending } => query.OrderByDescending(u => u.Username),
                _ => query,
            };

        public static IQueryable<HVACUser> WithSearch(this IQueryable<HVACUser> query, string? searchParam)
        {
            if (!string.IsNullOrEmpty(searchParam))
            {
                string loweredSearchParam = searchParam.ToLower();
                query = query.Where(x => x.UserName != null && x.UserName.ToLower().Contains(loweredSearchParam));
            }

            return query;
        }

        public static IQueryable<HVACUser> WithPagination(this IQueryable<HVACUser> query, Pagination pagination)
            => query.Skip((pagination.Page - 1) * pagination.Limit).Take(pagination.Limit);
    }
}
