using HVACrate.Application.Models;
using HVACrate.Application.Models.Materials;

namespace HVACrate.Application.Interfaces
{
    public interface IMaterialService
    {
        Task<List<MaterialModel>> GetAllAsReadOnlyAsync(BaseQueryModel query, CancellationToken cancellationToken = default);
        Task<MaterialModel> GetByIdAsReadOnlyAsync(Guid id, CancellationToken cancellationToken = default);
        Task<MaterialModel> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task CreateAsync(MaterialModel model, CancellationToken cancellationToken = default);
        Task UpdateAsync(MaterialModel model, CancellationToken cancellationToken = default);
        Task SoftDeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
