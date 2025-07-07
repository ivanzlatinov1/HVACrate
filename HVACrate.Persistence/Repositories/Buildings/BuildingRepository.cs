using HVACrate.Domain.Repositories.Buildings;
using HVACrate.Domain.Repositories.Rooms;

namespace HVACrate.Persistence.Repositories.Buildings
{
    public class BuildingRepository(HVACrateDbContext context, IRoomRepository roomRepository) 
        : BaseRepository<Building>(context), IBuildingRepository
    {
        private readonly IRoomRepository _roomRepository = roomRepository;
        public async Task<double> CalculateTotalHeatInfiltration(Guid id, CancellationToken cancellationToken)
        {
            Building building = await this.GetByIdAsReadOnlyAsync(id, cancellationToken);

            double totalHeatInfiltration = 0;

            foreach(var room in building.Rooms)
            {
                totalHeatInfiltration += 
                    await this._roomRepository.CalculateTotalHeatInfiltration(room.Id, cancellationToken);
            }

            return totalHeatInfiltration;
        }

        public async Task<double> CalculateTotalHeatTransmission(Guid id, CancellationToken cancellationToken)
        {
            Building building = await this.GetByIdAsReadOnlyAsync(id, cancellationToken);

            double totalHeatTransmission = 0;

            foreach(var room in building.Rooms)
            {
                totalHeatTransmission += 
                    await this._roomRepository.CalculateTotalHeatTransmission(room.Id, cancellationToken);
            }

            return totalHeatTransmission;
        }
    }
}
