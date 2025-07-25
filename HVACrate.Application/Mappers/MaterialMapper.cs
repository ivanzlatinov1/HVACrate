﻿using HVACrate.Application.Models.Materials;
using HVACrate.Domain.Entities;

namespace HVACrate.Application.Mappers
{
    internal static class MaterialMapper
    {
        public static MaterialModel ToModel(this Material entity, bool firstTime = true)
            => new()
            {
                Id = entity.Id,
                Type = entity.Type,
                ThermalConductivity = entity.ThermalConductivity,
                IsDeleted = entity.IsDeleted,
                BuildingEnvelopes = firstTime ? entity.BuildingEnvelopes.Select(x => x.ToModel(false)).ToList() : null!
            };

        public static Material ToEntity(this MaterialModel model, bool firstTime = true)
            => new()
            {
                Id = model.Id,
                Type = model.Type,
                ThermalConductivity = model.ThermalConductivity,
                IsDeleted = model.IsDeleted,
                BuildingEnvelopes = firstTime ? model.BuildingEnvelopes.Select(x => x.ToEntity(false)).ToList() : null!
            };
    }
}
