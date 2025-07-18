using HVACrate.Application.Interfaces;
using HVACrate.Application.Mappers;
using HVACrate.Application.Models;
using HVACrate.Application.Models.Buildings;
using HVACrate.Domain.Entities;
using HVACrate.Domain.Repositories.Buildings;
using HVACrate.Domain.ValueObjects;

namespace HVACrate.Application.Services
{
    public class BuildingService(IBuildingRepository buildingRepository, IRoomService roomService) : IBuildingService
    {
        private readonly IBuildingRepository _buildingRepository = buildingRepository;
        private readonly IRoomService _roomService = roomService;

        public async Task<double> CalculateTotalHeatInfiltration(Guid id, CancellationToken cancellationToken)
        {
            Building building = await this._buildingRepository.GetByIdAsync(id, cancellationToken);

            double totalHeatInfiltration = 0;

            foreach(var room in building.Rooms)
            {
                totalHeatInfiltration += 
                    await this._roomService.CalculateTotalHeatInfiltration(room.Id, cancellationToken);
            }

            return totalHeatInfiltration;
        }

        public async Task<double> CalculateTotalHeatTransmission(Guid id, CancellationToken cancellationToken)
        {
            Building building = await this._buildingRepository.GetByIdAsync(id, cancellationToken);

            double totalHeatTransmission = 0;

            foreach(var room in building.Rooms)
            {
                totalHeatTransmission += 
                    await this._roomService.CalculateTotalHeatTransmission(room.Id, cancellationToken);
            }

            return totalHeatTransmission;
        }

        public async Task CreateAsync(BuildingModel model, CancellationToken cancellationToken = default)
        {
            await this._buildingRepository
                .CreateAsync(model.ToEntity(false), cancellationToken)
                .ConfigureAwait(false);

            await this._buildingRepository.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<Result<BuildingModel>> GetAllAsReadOnlyAsync(BaseQueryModel query, Guid? projectId, CancellationToken cancellationToken = default)
        {
            Result<Building> projects = 
                await this._buildingRepository
                .GetAllAsReadOnlyAsync(query.ToQuery(),projectId, cancellationToken)
                .ConfigureAwait(false);

            return new Result<BuildingModel>(projects.Count, [.. projects.Items.Select(x => x.ToModel())]);
        }

        public async Task<BuildingModel> GetByIdAsReadOnlyAsync(Guid id, CancellationToken cancellationToken = default)
        {
            Building building = await this._buildingRepository
                .GetByIdAsReadOnlyAsync(id, cancellationToken)
                .ConfigureAwait(false);

            return building.ToModel();
        }

        public async Task<BuildingModel> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            Building building = await this._buildingRepository
                .GetByIdAsync(id, cancellationToken)
                .ConfigureAwait(false);

            return building.ToModel();
        }

        public async Task SoftDeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            Building building = await this._buildingRepository.GetByIdAsync(id, cancellationToken)
                ?? throw new Exception("Building not found");

            this._buildingRepository.SoftDelete(building);
            await this._buildingRepository.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task UpdateAsync(BuildingModel model, CancellationToken cancellationToken = default)
        {
            this._buildingRepository.Update(model.ToEntity());
            await this._buildingRepository.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
