using HVACrate.Domain.Repositories.BuildingEnvelopes;
using HVACrate.Domain.Repositories.Rooms;

namespace HVACrate.Persistence.Repositories.Rooms
{
    public class RoomRepository(HVACrateDbContext context, IBuildingEnvelopeRepository
        buildingEnvelopeRepository): BaseRepository<Room>(context), IRoomRepository
    {
        private readonly IBuildingEnvelopeRepository _buildingEnvelopeRepository = buildingEnvelopeRepository;
        public async Task<double> CalculateTotalHeatInfiltration(Guid id, CancellationToken cancellationToken)
        {
            Room room = await this.GetByIdAsReadOnlyAsync(id, cancellationToken);

            double heatInfiltration = 0;
            foreach(var buildingEnvelope in room.BuildingEnvelopes)
            {
                heatInfiltration += this._buildingEnvelopeRepository
                    .CalculateHeatInfiltration(buildingEnvelope);
            }

            return heatInfiltration;
        }

        public async Task<double> CalculateTotalHeatTransmission(Guid id, CancellationToken cancellationToken)
        {
            Room room = await this.GetByIdAsReadOnlyAsync(id, cancellationToken);

            double heatTransmission = 0;
            foreach(var buildingEnvelope in room.BuildingEnvelopes)
            {
                heatTransmission += this._buildingEnvelopeRepository
                    .CalculateHeatTransmission(buildingEnvelope);
            }

            return heatTransmission;
        }
    }
}
