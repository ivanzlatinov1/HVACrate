using HVACrate.Application.Models.BuildingEnvelopes;
using HVACrate.Domain.Entities;
using HVACrate.Domain.Entities.BuildingEnvelopes;

namespace HVACrate.Application.Mappers
{
    internal static class BuildingEnvelopeMapper
    {
        public static BuildingEnvelopeModel ToModel(this BuildingEnvelope entity, bool firstTime = true)
        {
            BuildingEnvelopeModel model = entity switch
            {
                OuterWall wall => new OuterWallModel
                {
                    ShouldReduceHeatingArea = wall.ShouldReduceHeatingArea,
                    Direction = wall.Direction
                },
                Opening opening => new OpeningModel
                {
                    JointLength = opening.JointLength,
                    VentilationCoefficient = opening.VentilationCoefficient,
                    Direction = opening.Direction
                },
                Floor floor => new FloorModel
                {
                    ThermalConductivityResistance = floor.ThermalConductivityResistance,
                    GroundWaterLength = floor.GroundWaterLength,
                    GroundWaterTemperature = floor.GroundWaterTemperature,
                },
                InternalFence internalFence => new InternalFenceModel
                {
                    Count = internalFence.Count,
                },
                Roof roof => new RoofModel(),
                _ => throw new Exception($"Unsupported type: {entity.GetType().Name}")
            };

            MapSharedToModel(entity, model, firstTime);
            return model;
        }

        public static BuildingEnvelope ToEntity(this BuildingEnvelopeModel model, bool firstTime = true)
        {
            BuildingEnvelope entity = model switch
            {
                OuterWallModel wallModel => new OuterWall
                {
                    ShouldReduceHeatingArea = wallModel.ShouldReduceHeatingArea,
                    Direction = wallModel.Direction
                },
                OpeningModel openingModel => new Opening
                {
                    JointLength = openingModel.JointLength,
                    VentilationCoefficient = openingModel.VentilationCoefficient,
                    Count = openingModel.Count,
                    Direction = openingModel.Direction
                },
                FloorModel floorModel => new Floor
                {
                    ThermalConductivityResistance = floorModel.ThermalConductivityResistance,
                    GroundWaterLength = floorModel.GroundWaterLength,
                    GroundWaterTemperature = floorModel.GroundWaterTemperature,
                },
                InternalFenceModel internalFenceModel => new InternalFence
                {
                    Count = internalFenceModel.Count,
                },
                RoofModel roofModel => new Roof(),
                _ => throw new Exception($"Unsupported type: {model.GetType().Name}")
            };

            MapSharedToEntity(model, entity, firstTime);
            return entity;
        }

        // Helper methods for the common properties
        private static void MapSharedToModel(BuildingEnvelope entity, BuildingEnvelopeModel model, bool firstTime)
        {
            model.Id = entity.Id;
            model.Height = entity.Height;
            model.Width = entity.Width;
            model.Area = entity.Area;
            model.AdjustedTemperature = entity.AdjustedTemperature;
            model.HeatTransferCoefficient = entity.HeatTransferCoefficient;
            model.Density = entity.Density;
            model.IsDeleted = entity.IsDeleted;
            model.RoomId = entity.RoomId;
            model.MaterialId = entity.MaterialId;
            model.Room = firstTime ? entity.Room.ToModel(false) : null!;
            model.Material = firstTime ? entity.Material.ToModel(false) : null!;
        }

        private static void MapSharedToEntity(BuildingEnvelopeModel model, BuildingEnvelope entity, bool firstTime)
        {
            entity.Id = model.Id;
            entity.Height = model.Height;
            entity.Width = model.Width;
            entity.Area = model.Area;
            entity.AdjustedTemperature = model.AdjustedTemperature;
            entity.HeatTransferCoefficient = model.HeatTransferCoefficient;
            entity.Density = model.Density;
            entity.IsDeleted = model.IsDeleted;
            entity.RoomId = model.RoomId;
            entity.MaterialId = model.MaterialId;
            entity.Room = firstTime ? model.Room.ToEntity(false) : null!;
            entity.Material = firstTime ? model.Material.ToEntity(false) : null!;
        }
    }
}
