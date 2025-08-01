﻿using HVACrate.Application.Models.BuildingEnvelopes;
using HVACrate.Domain.Entities;
using HVACrate.Domain.Entities.BuildingEnvelopes;
using HVACrate.Domain.Enums;
using HVACrate.Presentation.Models.BuildingEnvelopes;

namespace HVACrate.Application.Mappers
{
    public static class BuildingEnvelopeMapper
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
                    Count = opening.Count,
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
                Roof => new RoofModel(),
                _ => throw new Exception($"Unsupported type: {entity.GetType().Name}")
            };

            MapSharedToModel(entity, model, firstTime);
            return model;
        }

        public static BuildingEnvelopeModel ToModelFromForm(this BuildingEnvelopeFormModel form)
        {
            BuildingEnvelopeModel model = form switch
            {
                OuterWallFormModel wall => new OuterWallModel
                {
                    ShouldReduceHeatingArea = wall.ShouldReduceHeatingArea,
                    Direction = Enum.Parse<Direction>(wall.Direction),
                },
                OpeningFormModel opening => new OpeningModel
                {
                    Count = opening.Count,
                    JointLength = opening.JointLength,
                    VentilationCoefficient = opening.VentilationCoefficient,
                    Direction = Enum.Parse<Direction>(opening.Direction),
                },
                FloorFormModel floor => new FloorModel
                {
                    ThermalConductivityResistance = floor.ThermalConductivityResistance,
                    GroundWaterLength = floor.GroundWaterLength,
                    GroundWaterTemperature = floor.GroundWaterTemperature,
                },
                InternalFenceFormModel internalFence => new InternalFenceModel
                {
                    Count = internalFence.Count,
                },
                RoofFormModel => new RoofModel(),
                _ => throw new Exception($"Unsupported type: {form.GetType().Name}")
            };

            MapSharedToModelFromForm(form, model);
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
                RoofModel => new Roof(),
                _ => throw new Exception($"Unsupported type: {model.GetType().Name}")
            };

            MapSharedToEntity(model, entity, firstTime);
            return entity;
        }

        public static BuildingEnvelopeViewModel ToView(this BuildingEnvelopeModel model)
        {
            BuildingEnvelopeViewModel view = model switch
            {
                OuterWallModel wall => new OuterWallViewModel
                {
                    Id = wall.Id,
                    Type = wall.Type.ToString(),
                    Direction = wall.Direction.ToString(),
                },
                OpeningModel opening => new OpeningViewModel
                {
                    Id = opening.Id,
                    Type = opening.Type.ToString(),
                    Direction = opening.Direction.ToString(),
                },
                BuildingEnvelopeModel buildingEnvelope => new BuildingEnvelopeViewModel
                {
                    Id = buildingEnvelope.Id,
                    Type = buildingEnvelope.Type.ToString(),
                },
                _ => throw new Exception($"Unsupported type: {model.GetType().Name}")
            };

            return view;
        }

        public static BuildingEnvelopeDetailViewModel ToDetailsView(this BuildingEnvelopeModel model)
            => new()
            {
                Id = model.Id,
                Type = model.Type.ToString(),
                Direction = model.GetDirection(),
                Width = model.Width,
                Height = model.Height,
                Area = model.Area,
                Density = model.Density,
                AdjustedTemperature = model.AdjustedTemperature,
                HeatTransferCoefficient = model.HeatTransferCoefficient,
                Material = model.Material.Type,
                RoomName = model.Room.Type + " " + model.Room.Number,
            };

        public static BuildingEnvelopeCalculatableViewModel ToCalculatableViewModel(this BuildingEnvelopeModel model)
            => new()
            {
                Id = model.Id,
                Type = model.Type.ToString(),
                Direction = model.GetDirection(),
                Width = model.Width,
                Height = model.Height,
                Area = model.Area,
                Count = model.GetCount(),
                AdjustedTemperature = model.AdjustedTemperature,
                RoomTemperature = model.Room.Temperature,
                HeatTransferCoefficient = model.HeatTransferCoefficient,
                RoomName = model.Room.Type + " " + model.Room.Number,
            };

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

        private static void MapSharedToModelFromForm(BuildingEnvelopeFormModel form, BuildingEnvelopeModel model)
        {
            model.Id = form.Id;
            model.Height = form.Height;
            model.Width = form.Width;
            model.Area = form.Area;
            model.AdjustedTemperature = form.AdjustedTemperature;
            model.HeatTransferCoefficient = form.HeatTransferCoefficient;
            model.Density = form.Density;
            model.IsDeleted = form.IsDeleted;
            model.RoomId = form.RoomId;
            model.MaterialId = form.MaterialId;
        }

        private static string? GetDirection(this BuildingEnvelopeModel model)
        {
            if(model is OuterWallModel outerWall)
                return outerWall.Direction.ToString();
            else if(model is OpeningModel openingModel)
                return openingModel.Direction.ToString();
            return null;
        }

        private static int GetCount (this BuildingEnvelopeModel model)
        {
            if(model is OpeningModel opening)
                return opening.Count;
            else if(model is InternalFenceModel internalFence)
                return internalFence.Count;
            else return 1;
        }
    }
}
