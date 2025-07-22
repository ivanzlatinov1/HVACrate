using HVACrate.Application.Interfaces;
using HVACrate.Application.Mappers;
using HVACrate.Application.Models.BuildingEnvelopes;
using HVACrate.Domain.Entities;
using HVACrate.Domain.Entities.BuildingEnvelopes;
using HVACrate.Domain.Enums;
using HVACrate.Domain.Repositories.BuildingEnvelopes;
using HVACrate.Domain.Repositories.Rooms;

namespace HVACrate.Application.Services
{
    public class BuildingEnvelopeService(IBuildingEnvelopeRepository buildingEnvelopeRepository,
        IRoomRepository roomRepository) : IBuildingEnvelopeService
    {
        private readonly IBuildingEnvelopeRepository _buildingEnvelopeRepository = buildingEnvelopeRepository;
        private readonly IRoomRepository _roomRepository = roomRepository;

        public async Task<List<BuildingEnvelopeModel>> GetAllAsReadOnlyAsync(Guid? roomId, CancellationToken cancellationToken = default)
        {
            var buildingEnvelopes = await this._buildingEnvelopeRepository
                .GetAllAsReadOnlyAsync(roomId, cancellationToken)
                .ConfigureAwait(false);

            return [.. buildingEnvelopes.Select(x => x.ToModel(false))];
        }

        public async Task<OuterWallModel?> GetWallByDirectionAsync(Guid roomId, Direction direction, CancellationToken cancellationToken = default)
        {
            OuterWall? wall = await this._buildingEnvelopeRepository
                .GetWallByDirectionAsync(roomId, direction, cancellationToken)
                .ConfigureAwait(false);

            return wall?.ToModel() as OuterWallModel;
        }

        public async Task<BuildingEnvelopeModel> GetByIdAsReadOnlyAsync(Guid id, CancellationToken cancellationToken = default)
        {
            BuildingEnvelope buildingEnvelope = await this._buildingEnvelopeRepository
                .GetByIdAsReadOnlyAsync(id, cancellationToken)
                .ConfigureAwait(false);

            return buildingEnvelope.ToModel();
        }

        public async Task<BuildingEnvelopeModel> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            BuildingEnvelope buildingEnvelope = await this._buildingEnvelopeRepository
                .GetByIdAsync(id, cancellationToken)
                .ConfigureAwait(false);

            return buildingEnvelope.ToModel();
        }

        public async Task CreateAsync(BuildingEnvelopeModel model, CancellationToken cancellationToken = default)
        {
            await this._buildingEnvelopeRepository
                .CreateAsync(model.ToEntity(false), cancellationToken)
                .ConfigureAwait(false);

            await this._buildingEnvelopeRepository.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            await this.TryMakeRoomEnclosed(model.RoomId, cancellationToken).ConfigureAwait(false);
        }

        public async Task UpdateAsync(BuildingEnvelopeModel model, CancellationToken cancellationToken = default)
        {
            this._buildingEnvelopeRepository.Update(model.ToEntity(false));
            await this._buildingEnvelopeRepository.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            await this.TryMakeRoomEnclosed(model.RoomId, cancellationToken).ConfigureAwait(false);
        }

        public async Task SoftDeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            BuildingEnvelope buildingEnvelope = await this._buildingEnvelopeRepository.GetByIdAsync(id, cancellationToken)
                ?? throw new Exception("Building Envelope not found");

            this._buildingEnvelopeRepository.SoftDelete(buildingEnvelope);
            await this._buildingEnvelopeRepository.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            await this.TryMakeRoomEnclosed(buildingEnvelope.RoomId, cancellationToken).ConfigureAwait(false);
        }

        public double CalculateHeatInfiltration(BuildingEnvelopeModel buildingEnvelope)
        {
            throw new NotImplementedException();
        }

        public double CalculateHeatTransmission(BuildingEnvelopeModel buildingEnvelope)
        {
            double area = buildingEnvelope.Area;
            double thermalResistance = 1.0 / buildingEnvelope.HeatTransferCoefficient;
            double roomTemperature = buildingEnvelope.Room.Temperature;
            double regionTemperature = buildingEnvelope.Room.Building.Project.RegionTemperature;
            double adjustedTemperature = buildingEnvelope.AdjustedTemperature;
            double orientationCoefficient = 1.0;

            if (buildingEnvelope is OuterWallModel outerWall)
            {
                orientationCoefficient = GetOrientationCoefficient(outerWall.Direction);
            }
            else if (buildingEnvelope is OpeningModel opening)
            {
                orientationCoefficient = GetOrientationCoefficient(opening.Direction);
            }

            return area / thermalResistance *
                (roomTemperature - regionTemperature - adjustedTemperature) * orientationCoefficient;
        }

        private static double GetOrientationCoefficient(Direction direction)
        {
            return direction switch
            {
                Direction.North or Direction.East or Direction.Northeast or Direction.Northwest => 1.10,
                Direction.South or Direction.Southwest => 1.0,
                Direction.West or Direction.Southeast => 1.05,
                _ => 1.0,
            };
        }

        private async Task TryMakeRoomEnclosed(Guid roomId, CancellationToken cancellationToken = default)
        {
            Room room = await this._roomRepository
                .GetByIdAsync(roomId, cancellationToken)
                .ConfigureAwait(false);

            List<OuterWall> outerWalls = [.. room.BuildingEnvelopes.OfType<OuterWall>()];
            List<InternalFence> internalFences = [.. room.BuildingEnvelopes.OfType<InternalFence>()];

            bool isThereFloor = await this._buildingEnvelopeRepository
                .IsThereAFloorInRoomAsync(roomId, cancellationToken)
                .ConfigureAwait(false);

            bool isThereRoof = await this._buildingEnvelopeRepository
                .IsThereARoofInRoomAsync(roomId, cancellationToken)
                .ConfigureAwait(false);

            bool isEnclosed = true;

            if (!isThereFloor || !isThereRoof || outerWalls.Count == 0)
                isEnclosed = false;

            int totalWalls = outerWalls.Count + internalFences.Sum(x => x.Count);

            if (totalWalls < 4)
                isEnclosed = false;

            if (isEnclosed)
            {
                room.IsEnclosed = true;
            }
            else
            {
                room.IsEnclosed = false;
            }
            await this._roomRepository.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<bool> IsThereAWallOnDirectionAsync(Guid roomId, Direction direction, CancellationToken cancellationToken = default)
            => await this._buildingEnvelopeRepository.IsThereAWallOnDirectionAsync(roomId, direction, cancellationToken).ConfigureAwait(false);

        public async Task<bool> IsThereAnOpeningOnDirectionAsync(Guid roomId, Direction direction, CancellationToken cancellationToken = default)
            => await this._buildingEnvelopeRepository.IsThereAnOpeningOnDirectionAsync(roomId, direction, cancellationToken).ConfigureAwait(false);

        public async Task<bool> IsThereARoofInRoomAsync(Guid roomId, CancellationToken cancellationToken = default)
            => await this._buildingEnvelopeRepository.IsThereARoofInRoomAsync(roomId, cancellationToken).ConfigureAwait(false);

        public async Task<bool> IsThereAFloorInRoomAsync(Guid roomId, CancellationToken cancellationToken = default)
            => await this._buildingEnvelopeRepository.IsThereAFloorInRoomAsync(roomId, cancellationToken).ConfigureAwait(false);

        public async Task<long> GetInternalFencesCountByRoom(Guid roomId, CancellationToken cancellationToken = default)
           => await this._buildingEnvelopeRepository.GetInternalFencesCountByRoom(roomId, cancellationToken).ConfigureAwait(false);

        public async Task<long> GetOpeningsCountByRoom(Guid roomId, CancellationToken cancellationToken = default)
           => await this._buildingEnvelopeRepository.GetOpeningsCountByRoom(roomId, cancellationToken).ConfigureAwait(false);
    }
}
