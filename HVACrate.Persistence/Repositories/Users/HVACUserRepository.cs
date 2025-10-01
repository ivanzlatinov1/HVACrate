using HVACrate.Domain.Repositories;
using HVACrate.Domain.Repositories.Users;
using HVACrate.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace HVACrate.Persistence.Repositories.Users
{
    public class HVACUserRepository : IUserRepository
    {
        private readonly HVACrateDbContext _context;

        public HVACUserRepository(HVACrateDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<Result<HVACUser>> GetAllAsync(BaseQuery query, CancellationToken cancellationToken = default)
        {
            IQueryable<HVACUser> baseQuery = _context.Users
                .WithSearch(query.SearchParam, x => EF.Property<string>(x, query.QueryParam));

            int count = await baseQuery
                .CountAsync(cancellationToken);

            HVACUser[] users = await baseQuery
                .WithPagination(query.Pagination)
                .ToArrayAsync(cancellationToken: cancellationToken);

            return new Result<HVACUser>(count, users);
        }

        public async Task<HVACUser?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
            => await _context.Users
            .SingleOrDefaultAsync(u => u.Id == id, cancellationToken);

        public async Task<Dictionary<Guid, string>> GetRolesByIdsAsync(Guid[] ids, CancellationToken cancellationToken = default)
            => await _context.UserRoles
            .Where(x => ids.Contains(x.UserId))
            .Join(_context.Roles,
                userRoles => userRoles.RoleId,
                role => role.Id,
                (userRoles, role) => new { RoleName = role.Name!, userRoles.UserId })
            .GroupBy(x => x.UserId)
            .ToDictionaryAsync(k => k.Key, v => v.Single().RoleName, cancellationToken);

        public async Task<string?> GetUserRoleAsync(Guid id, CancellationToken cancellationToken = default)
            => (await _context.UserRoles
                .Join(_context.Roles,
                    userRoles => userRoles.RoleId,
                    role => role.Id,
                    (userRoles, role) => new { RoleName = role.Name!, userRoles.UserId })
                .FirstOrDefaultAsync(x => x.UserId == id, cancellationToken))?.RoleName;

        public void Remove(HVACUser user)
        {
            this._context.Users.Remove(user);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await this._context.SaveChangesAsync(cancellationToken);
        }
    }
}