﻿using HVACrate.Domain.Entities;

namespace HVACrate.Domain.Repositories.Materials
{
    public interface IMaterialRepository : IBaseRepository<Material>
    {
        Task<Material[]> GetAllAsReadOnlyAsync(CancellationToken cancellationToken = default);
    }
}
