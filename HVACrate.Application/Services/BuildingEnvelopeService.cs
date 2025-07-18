using HVACrate.Application.Interfaces;
using HVACrate.Application.Mappers;
using HVACrate.Application.Models;
using HVACrate.Application.Models.BuildingEnvelopes;
using HVACrate.Domain.Entities;
using HVACrate.Domain.Enums;
using HVACrate.Domain.Repositories.BuildingEnvelopes;
using HVACrate.Domain.ValueObjects;

namespace HVACrate.Application.Services
{
    public class BuildingEnvelopeService(IBuildingEnvelopeRepository buildingEnvelopeRepository) : IBuildingEnvelopeService
    {
        private readonly IBuildingEnvelopeRepository _buildingEnvelopeRepository = buildingEnvelopeRepository;

        public async Task<Result<BuildingEnvelopeModel>> GetAllAsReadOnlyAsync(BaseQueryModel query, Guid? buildingId, CancellationToken cancellationToken = default)
        {
            var buildingEnvelopes = await this._buildingEnvelopeRepository
                .GetAllAsReadOnlyAsync(query.ToQuery(), buildingId, cancellationToken)
                .ConfigureAwait(false);

            return new Result<BuildingEnvelopeModel>(buildingEnvelopes.Count, [.. buildingEnvelopes.Items.Select(x => x.ToModel())]);
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
        }

        public async Task UpdateAsync(BuildingEnvelopeModel model, CancellationToken cancellationToken = default)
        {
            this._buildingEnvelopeRepository.Update(model.ToEntity());
            await this._buildingEnvelopeRepository.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task SoftDeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            BuildingEnvelope room = await this._buildingEnvelopeRepository.GetByIdAsync(id, cancellationToken)
                ?? throw new Exception("Building Envelope not found");

            this._buildingEnvelopeRepository.SoftDelete(room);
            await this._buildingEnvelopeRepository.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public double CalculateHeatInfiltration(BuildingEnvelopeModel buildingEnvelope)
        {
            throw new NotImplementedException();
        }

        public double CalculateHeatTransmission(BuildingEnvelopeModel buildingEnvelope)
        {
            double area = buildingEnvelope.Width * buildingEnvelope.Height;
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
    }
}
