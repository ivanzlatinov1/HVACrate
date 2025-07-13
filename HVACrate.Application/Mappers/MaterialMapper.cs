using HVACrate.Application.Models.Materials;
using HVACrate.Domain.Entities;

namespace HVACrate.Application.Mappers
{
    internal static class MaterialMapper
    {
        public static MaterialModel ToModel(this Material entity, bool firstTime = true)
            => new()
            {
            };

        public static Material ToEntity(this MaterialModel model, bool firstTime = true)
            => new()
            {
            };
    }
}
