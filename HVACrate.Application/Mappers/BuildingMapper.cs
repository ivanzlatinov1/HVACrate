using HVACrate.Application.Models.Buildings;
using HVACrate.Domain.Entities;

namespace HVACrate.Application.Mappers
{
    internal static class BuildingMapper
    {
        public static BuildingModel ToModel(this Building entity, bool firstTime = true)
            => new()
            {
            };

        public static Building ToEntity(this BuildingModel model, bool firstTime = true)
            => new()
            {
            };
    }
}
