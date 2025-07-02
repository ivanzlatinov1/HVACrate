using HVACrate.Application.Models;

namespace HVACrate.Application.Interfaces
{
    public interface IUserService
    {
        Task<ICollection<HVACUserModel>> GetAllAsync(CancellationToken cancellationToken);
        Task<HVACUserModel?> GetByIdAsync(string id, CancellationToken cancellationToken);
        Task<string> AddAsync(HVACUserModel userModel, string password, CancellationToken cancellationToken);
        Task RemoveAsync(string id, CancellationToken cancellationToken);
        Task<ICollection<HVACUserModel>?> FindByUsernameAsync(string userName, CancellationToken cancellationToken);
        Task<bool> IsAdmin(HVACUserModel userModel);
    }
}
