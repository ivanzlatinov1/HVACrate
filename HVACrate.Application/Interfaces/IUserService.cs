using HVACrate.Application.Models.HVACUsers;

namespace HVACrate.Application.Interfaces
{
    public interface IUserService
    {
        Task<ICollection<HVACUserModel>> GetAllAsync(HVACUserQueryModel query, CancellationToken cancellationToken = default);
        Task<HVACUserModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Guid> AddAsync(HVACUserModel userModel, string password, CancellationToken cancellationToken = default);
        Task RemoveAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Dictionary<Guid, string>> GetRolesAsync(Guid[] ids, CancellationToken cancellationToken = default);
    }
}
