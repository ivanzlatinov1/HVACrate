﻿using HVACrate.Domain.Entities;
using HVACrate.Domain.ValueObjects;

namespace HVACrate.Domain.Repositories.Users
{
    public interface IUserRepository
    {
        Task<Dictionary<Guid, string>> GetRolesByIdsAsync(Guid[] ids, CancellationToken cancellationToken = default);
        Task<Result<HVACUser>> GetAllAsync(BaseQuery query, CancellationToken cancellationToken = default);
        Task<HVACUser?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        void Remove(HVACUser user);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<string?> GetUserRoleAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
