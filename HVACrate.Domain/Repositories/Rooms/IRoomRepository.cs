using HVACrate.Domain.Entities;
using HVACrate.Domain.ValueObjects;

namespace HVACrate.Domain.Repositories.Rooms
{
    public interface IRoomRepository : IBaseRepository<Room>
    {
        Task<Result<Room>> GetAllAsReadOnlyAsync(BaseQuery query, Guid? filterId = null, CancellationToken cancellationToken = default);
    }
}
