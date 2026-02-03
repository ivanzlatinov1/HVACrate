using HVACrate.Application.Interfaces;
using HVACrate.Application.Mappers;
using HVACrate.Application.Models.BuildingEnvelopes;
using HVACrate.Domain.Entities;
using HVACrate.Domain.Entities.BuildingEnvelopes;
using HVACrate.Domain.Enums;
using HVACrate.Domain.Repositories.BuildingEnvelopes;
using HVACrate.Domain.Repositories.Buildings;
using HVACrate.Domain.Repositories.Rooms;

namespace HVACrate.Application.Services
{
    public class BuildingEnvelopeService : IBuildingEnvelopeService
    {
        private readonly IBuildingEnvelopeRepository _buildingEnvelopeRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IBuildingRepository _buildingRepository;
        public BuildingEnvelopeService(IBuildingEnvelopeRepository buildingEnvelopeRepository,
        IRoomRepository roomRepository, IBuildingRepository buildingRepository)
        {
            _buildingEnvelopeRepository = buildingEnvelopeRepository;
            _roomRepository = roomRepository;
            _buildingRepository = buildingRepository;
        }

        public async Task<List<BuildingEnvelopeModel>> GetAllAsReadOnlyAsync(Guid? roomId, CancellationToken cancellationToken = default)
        {
            var buildingEnvelopes = await this._buildingEnvelopeRepository
                .GetAllAsReadOnlyAsync(roomId, cancellationToken)
                .ConfigureAwait(false);

            return buildingEnvelopes.Select(x => x.ToModel()).ToList();
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

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            BuildingEnvelope buildingEnvelope = await this._buildingEnvelopeRepository.GetByIdAsync(id, cancellationToken)
                ?? throw new Exception("Building Envelope not found");

            this._buildingEnvelopeRepository.Delete(buildingEnvelope);
            await this._buildingEnvelopeRepository.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            await this.TryMakeRoomEnclosed(buildingEnvelope.RoomId, cancellationToken).ConfigureAwait(false);
        }

        public double CalculateTemperatureCoefficient(double density, string type)
        {
            if (type == "Floor")
            {
                if (density >= 150 && density < 250)
                    return 1.0;
                else if (density >= 250 && density < 400)
                    return 2.0;
                else if (density >= 400)
                    return 3.0;
            }
            else if (type == "Outer Wall" || type == "Roof")
            {
                if (density < 400)
                {
                    return 1.0;
                }
                else if (density >= 400)
                {
                    return 2.0;
                }
            }

            return 0.0;
        }

        public double CalculateHeatInfiltration(BuildingEnvelopeModel buildingEnvelope)
        {
            throw new NotImplementedException();
        }

        public async Task<double> CalculateHeatTransmission(BuildingEnvelopeModel buildingEnvelope)
        {
            double area = buildingEnvelope.Area;
            double thermalResistance = 1.0 / buildingEnvelope.HeatTransferCoefficient;
            double roomTemperature = buildingEnvelope.Room.Temperature;
            double regionTemperature = await this._buildingRepository.GetProjectRegionTemperature(buildingEnvelope.Room.BuildingId);
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

            if (buildingEnvelope is InternalFenceModel internalFence)
                return area / thermalResistance *
                    (roomTemperature - internalFence.AdjacentRoomTemperature - adjustedTemperature) * orientationCoefficient;

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

            List<OuterWall> outerWalls = room.BuildingEnvelopes.OfType<OuterWall>().ToList();
            List<InternalFence> internalFences = room.BuildingEnvelopes.OfType<InternalFence>().ToList();

            bool isThereFloor = await this._buildingEnvelopeRepository
                .IsThereAFloorInRoomAsync(roomId, cancellationToken)
                .ConfigureAwait(false);

            bool isThereRoof = await this._buildingEnvelopeRepository
                .IsThereARoofInRoomAsync(roomId, cancellationToken)
                .ConfigureAwait(false);

            bool isEnclosed = true;

            if (!isThereFloor || !isThereRoof)
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

        public async Task<OpeningModel[]> GetOpeningsByRoomAndDirectionAsync(Guid roomId, Direction direction, CancellationToken cancellationToken = default)
        {
            Opening[] openings = await this._buildingEnvelopeRepository.GetOpeningsByRoomAndDirectionAsync(roomId, direction, cancellationToken).ConfigureAwait(false);

            return openings.Select(x => (OpeningModel)x.ToModel(false)).ToArray();
        }

        public async Task<OuterWallModel[]> GetOuterWallsByRoomAsync(Guid roomId, CancellationToken cancellationToken = default)
        {
            OuterWall[] outerWalls = await this._buildingEnvelopeRepository.GetOuterWallsByRoomAsync(roomId, cancellationToken).ConfigureAwait(false);

            return outerWalls.Select(x => (OuterWallModel)x.ToModel(false)).ToArray();
        }

        public async Task<OpeningModel[]> GetOpeningsByRoomAsync(Guid roomId, CancellationToken cancellationToken = default)
        {
            Opening[] openings = await this._buildingEnvelopeRepository.GetOpeningsByRoomAsync(roomId, cancellationToken).ConfigureAwait(false);

            return openings.Select(x => (OpeningModel)x.ToModel(false)).ToArray();
        }

        public async Task<InternalFenceModel[]> GetInternalFencesByRoomAsync(Guid roomId, CancellationToken cancellationToken = default)
        {
            InternalFence[] openings =
                await this._buildingEnvelopeRepository.GetInternalFencesByRoomAsync(roomId, cancellationToken).ConfigureAwait(false);

            return openings.Select(x => (InternalFenceModel)x.ToModel(false)).ToArray();
        }

        public async Task<FloorModel[]> GetFloorsByRoomAsync(Guid roomId, CancellationToken cancellationToken = default)
        {
            Floor[] floors = await this._buildingEnvelopeRepository.GetFloorsByRoomAsync(roomId, cancellationToken).ConfigureAwait(false);

            return floors.Select(x => (FloorModel)x.ToModel(false)).ToArray();
        }

        public async Task<RoofModel[]> GetRoofsByRoomAsync(Guid roomId, CancellationToken cancellationToken = default)
        {
            Roof[] roofs = await this._buildingEnvelopeRepository.GetRoofsByRoomAsync(roomId, cancellationToken).ConfigureAwait(false);

            return roofs.Select(x => (RoofModel)x.ToModel(false)).ToArray();
        }

        public async Task<double> GetAreaToBeSubtracted(Guid buildingEnvelopeId, CancellationToken cancellationToken = default)
           => await this._buildingEnvelopeRepository.GetAreaToBeSubtracted(buildingEnvelopeId, cancellationToken).ConfigureAwait(false);
    }
}
