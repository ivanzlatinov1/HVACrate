using HVACrate.Application.Models.HVACUsers;
using HVACrate.Domain.ValueObjects;

namespace HVACrate.Application.Interfaces
{
    public interface IUserService
    {
        Task<Result<HVACUserModel>> GetAllAsync(HVACUserQueryModel query, CancellationToken cancellationToken = default);
        Task<HVACUserModel> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task RemoveAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Dictionary<Guid, string>> GetRolesAsync(Guid[] ids, CancellationToken cancellationToken = default);
        Task<string> GetRoleAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
