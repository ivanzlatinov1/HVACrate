using HVACrate.Application.Models.Rooms;
using HVACrate.Domain.Entities;

namespace HVACrate.Application.Mappers
{
    internal static class RoomMapper
    {
        public static RoomModel ToModel(this Room entity, bool firstTime = true)
            => new()
            {
            };

        public static Room ToEntity(this RoomModel model, bool firstTime = true)
            => new()
            {
            };
    }
}
