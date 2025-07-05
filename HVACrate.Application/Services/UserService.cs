using HVACrate.Application.Interfaces;
using HVACrate.Application.Mappers;
using HVACrate.Application.Models;
using HVACrate.Application.Models.HVACUsers;
using HVACrate.Domain.Entities;
using HVACrate.Domain.Repositories.Users;
using HVACrate.Domain.ValueObjects;

namespace HVACrate.Application.Services
{
    public class UserService(IUserRepository userRepository) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<Result<HVACUserModel>> GetAllAsync(BaseQueryModel query, CancellationToken cancellationToken = default)
        {
            Result<HVACUser> users = await this._userRepository.GetAllAsync(query.ToQuery(), cancellationToken);
            return new Result<HVACUserModel>(users.Count, [.. users.Items.Select(x => x.ToModel())]);
        }

        public async Task<Dictionary<Guid, string>> GetRolesAsync(Guid[] ids, CancellationToken cancellationToken = default)
            => await this._userRepository.GetRolesByIdsAsync(ids, cancellationToken);

        public async Task<HVACUserModel> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            HVACUser user = await this._userRepository.GetByIdAsync(id, cancellationToken) 
                ?? throw new Exception("User not found");

            return user.ToModel();
        }

        public async Task RemoveAsync(Guid id, CancellationToken cancellationToken = default)
        {
            HVACUser user = await this._userRepository.GetByIdAsync(id, cancellationToken) 
                ?? throw new Exception("User not found");

            _userRepository.Remove(user);
            await _userRepository.SaveChangesAsync(cancellationToken);
        }

        public async Task<string> GetRoleAsync(Guid id, CancellationToken cancellationToken = default)
            => await this._userRepository.GetUserRoleAsync(id, cancellationToken) 
            ?? throw new Exception("User not found");
    }
}
