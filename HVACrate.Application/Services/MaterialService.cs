using HVACrate.Application.Interfaces;
using HVACrate.Application.Mappers;
using HVACrate.Application.Models;
using HVACrate.Application.Models.Materials;
using HVACrate.Domain.Entities;
using HVACrate.Domain.Repositories.Materials;

namespace HVACrate.Application.Services
{
    public class MaterialService(IMaterialRepository materialRepository) : IMaterialService
    {
        private readonly IMaterialRepository _materialRepository = materialRepository;

        public async Task CreateAsync(MaterialModel model, CancellationToken cancellationToken = default)
        {
            await this._materialRepository
                .CreateAsync(model.ToEntity(false), cancellationToken)
                .ConfigureAwait(false);

            await this._materialRepository.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<List<MaterialModel>> GetAllAsReadOnlyAsync(BaseQueryModel query, CancellationToken cancellationToken = default)
        {
            var materials = await this._materialRepository
                .GetAllAsReadOnlyAsync(cancellationToken)
                .ConfigureAwait(false);

            return [.. materials.Select(x => x.ToModel(false))];
        }

        public async Task<MaterialModel> GetByIdAsReadOnlyAsync(Guid id, CancellationToken cancellationToken = default)
        {
            Material material = await this._materialRepository
                 .GetByIdAsReadOnlyAsync(id, cancellationToken)
                 .ConfigureAwait(false);

            return material.ToModel();
        }

        public async Task<MaterialModel> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            Material material = await this._materialRepository
                 .GetByIdAsync(id, cancellationToken)
                 .ConfigureAwait(false);

            return material.ToModel();
        }

        public async Task SoftDeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            Material material = await this._materialRepository.GetByIdAsync(id, cancellationToken)
                ?? throw new Exception("Material not found");

            this._materialRepository.SoftDelete(material);
            await this._materialRepository.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task UpdateAsync(MaterialModel model, CancellationToken cancellationToken = default)
        {
            this._materialRepository.Update(model.ToEntity());
            await this._materialRepository.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
