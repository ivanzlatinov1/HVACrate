using HVACrate.Application.Models.Rooms;
using HVACrate.Domain.Entities;

namespace HVACrate.Application.Mappers
{
    internal static class RoomMapper
    {
        public static RoomModel ToModel(this Room entity, bool firstTime = true)
            => new()
            {
                Id = entity.Id,
                Type = entity.Type,
                Number = entity.Number,
                Floor = entity.Floor,
                Temperature = entity.Temperature,
                IsDeleted = entity.IsDeleted,
                BuildingId = entity.BuildingId,
                Building = firstTime ? entity.Building.ToModel(false) : null!,
                BuildingEnvelopes = firstTime ? entity.BuildingEnvelopes.Select(x => x.ToModel(false)).ToList() : null!,
            };

        public static Room ToEntity(this RoomModel model, bool firstTime = true)
            => new()
            {
                Id = model.Id,
                Type = model.Type,
                Number = model.Number,
                Floor = model.Floor,
                Temperature = model.Temperature,
                IsDeleted = model.IsDeleted,
                BuildingId = model.BuildingId,
                Building = firstTime ? model.Building.ToEntity(false) : null!,
                BuildingEnvelopes = firstTime ? model.BuildingEnvelopes.Select(x => x.ToEntity(false)).ToList() : null!
            };
    }
}
