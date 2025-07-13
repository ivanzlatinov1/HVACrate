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
                _ => throw new Exception($"Unsupported type: {entity.GetType().Name}")
            };

            MapSharedToModel(entity, model);
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
                    Direction = openingModel.Direction
                },
                FloorModel floorModel => new Floor
                {
                    ThermalConductivityResistance = floorModel.ThermalConductivityResistance,
                    GroundWaterLength = floorModel.GroundWaterLength,
                    GroundWaterTemperature = floorModel.GroundWaterTemperature,
                },
                _ => throw new Exception($"Unsupported type: {model.GetType().Name}")
            };

            MapSharedToEntity(model, entity);
            return entity;
        }

        // Helper methods for the common properties
        private static void MapSharedToModel(BuildingEnvelope entity, BuildingEnvelopeModel model)
        {
            model.Id = entity.Id;
            model.Height = entity.Height;
            model.Width = entity.Width;
            model.AdjustedTemperature = entity.AdjustedTemperature;
            model.HeatTransferCoefficient = entity.HeatTransferCoefficient;
            model.Density = entity.Density;
            model.Count = entity.Count;
            model.IsDeleted = entity.IsDeleted;
            model.RoomId = entity.RoomId;
            model.MaterialId = entity.MaterialId;
            model.Room = entity.Room.ToModel(false);
            model.Material = entity.Material.ToModel(false);
        }

        private static void MapSharedToEntity(BuildingEnvelopeModel model, BuildingEnvelope entity)
        {
            entity.Id = model.Id;
            entity.Height = model.Height;
            entity.Width = model.Width;
            entity.AdjustedTemperature = model.AdjustedTemperature;
            entity.HeatTransferCoefficient = model.HeatTransferCoefficient;
            entity.Density = model.Density;
            entity.Count = model.Count;
            entity.IsDeleted = model.IsDeleted;
            entity.RoomId = model.RoomId;
            entity.MaterialId = model.MaterialId;
            entity.Room = model.Room.ToEntity(false);
            entity.Material = model.Material.ToEntity(false);
        }
    }
}
