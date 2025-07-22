using HVACrate.Application.Interfaces;
using HVACrate.Application.Mappers;
using HVACrate.Application.Models;
using HVACrate.Application.Models.Rooms;
using HVACrate.Domain.Entities;
using HVACrate.Domain.Repositories.Rooms;
using HVACrate.Domain.ValueObjects;

namespace HVACrate.Application.Services
{
    public class RoomService(IRoomRepository roomRepository, IBuildingEnvelopeService buildingEnvelopeService) : IRoomService
    {
        private readonly IRoomRepository _roomRepository = roomRepository;
        private readonly IBuildingEnvelopeService _buildingEnvelopeService = buildingEnvelopeService;

        public async Task<Result<RoomModel>> GetAllAsReadOnlyAsync(BaseQueryModel query, Guid? buildingId, CancellationToken cancellationToken = default)
        {
            var rooms = await this._roomRepository
                .GetAllAsReadOnlyAsync(query.ToQuery(), buildingId, cancellationToken)
                .ConfigureAwait(false);

            return new Result<RoomModel>(rooms.Count, [.. rooms.Items.Select(x => x.ToModel(false))]);
        }

        public async Task CreateAsync(RoomModel model, CancellationToken cancellationToken = default)
        {
            await this._roomRepository
                .CreateAsync(model.ToEntity(false), cancellationToken)
                .ConfigureAwait(false);

            await this._roomRepository.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<RoomModel> GetByIdAsReadOnlyAsync(Guid id, CancellationToken cancellationToken = default)
        {
            Room room = await this._roomRepository
                .GetByIdAsReadOnlyAsync(id, cancellationToken)
                .ConfigureAwait(false);

            return room.ToModel();
        }

        public async Task<Guid> GetBuildingIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            Room room = await this._roomRepository
                .GetByIdAsReadOnlyAsync(id, cancellationToken)
                .ConfigureAwait(false);

            return room.BuildingId;
        }

        public async Task<RoomModel> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            Room room = await this._roomRepository
                .GetByIdAsync(id, cancellationToken)
                .ConfigureAwait(false);

            return room.ToModel();
        }

        public async Task SoftDeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            Room room = await this._roomRepository.GetByIdAsync(id, cancellationToken)
                ?? throw new Exception("Room not found");

            this._roomRepository.SoftDelete(room);
            await this._roomRepository.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task UpdateAsync(RoomModel model, CancellationToken cancellationToken = default)
        {
            this._roomRepository.Update(model.ToEntity(false));
            await this._roomRepository.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<double> CalculateTotalHeatInfiltration(Guid id, CancellationToken cancellationToken)
        {
            Room room = await this._roomRepository.GetByIdAsReadOnlyAsync(id, cancellationToken);

            double heatInfiltration = 0;
            foreach (var buildingEnvelope in room.BuildingEnvelopes)
            {
                heatInfiltration += this._buildingEnvelopeService
                    .CalculateHeatInfiltration(buildingEnvelope.ToModel(false));
            }

            return heatInfiltration;
        }

        public async Task<double> CalculateTotalHeatTransmission(Guid id, CancellationToken cancellationToken)
        {
            Room room = await this._roomRepository.GetByIdAsReadOnlyAsync(id, cancellationToken);

            double heatTransmission = 0;
            foreach (var buildingEnvelope in room.BuildingEnvelopes)
            {
                heatTransmission += this._buildingEnvelopeService
                    .CalculateHeatTransmission(buildingEnvelope.ToModel(false));
            }

            return heatTransmission;
        }

        public async Task<string> GetRoomNumberAsync(Guid id, CancellationToken cancellationToken = default)
        {
            Room room = await this._roomRepository
                .GetByIdAsReadOnlyAsync(id, cancellationToken)
                .ConfigureAwait(false);

            return room.Number;
        }
    }
}
