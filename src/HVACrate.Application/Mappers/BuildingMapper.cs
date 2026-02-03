using HVACrate.Application.Models.Buildings;
using HVACrate.Domain.Entities;
using HVACrate.Presentation.Models.Buildings;

using static HVACrate.GCommon.GlobalConstants;

namespace HVACrate.Application.Mappers
{
    public static class BuildingMapper
    {
        public static BuildingModel ToModel(this Building entity, bool firstTime = true)
            => new()
            {
                Id = entity.Id,
                Name = entity.Name,
                Location = entity.Location,
                Floors = entity.Floors,
                Orientation = entity.Orientation,
                TotalHeight = entity.TotalHeight,
                WindSpeed = entity.WindSpeed,
                ImageUrl = entity.ImageUrl,
                IsDeleted = entity.IsDeleted,
                ProjectId = entity.ProjectId,
                Project = firstTime ? entity.Project.ToModel(false) : null!,
                Rooms = firstTime ? entity.Rooms.Select(r => r.ToModel(false)).ToList() : null!,
            };

        public static Building ToEntity(this BuildingModel model, bool firstTime = true)
            => new()
            {
                Id = model.Id,
                Name = model.Name,
                Location = model.Location,
                Floors = model.Floors,
                Orientation = model.Orientation,
                TotalHeight = model.TotalHeight,
                WindSpeed = model.WindSpeed,
                ImageUrl = model.ImageUrl,
                IsDeleted = model.IsDeleted,
                ProjectId = model.ProjectId,
                Project = firstTime ? model.Project.ToEntity(false) : null!,
                Rooms = firstTime ? model.Rooms.Select(r => r.ToEntity(false)).ToList() : null!,
            };

        public static BuildingViewModel ToView(this BuildingModel model)
            => new()
            {
                Id = model.Id,
                Name = model.Name,
                Location = model.Location,
                ImageUrl = model.ImageUrl ?? DefaultBuildingImageUrl,
                ProjectId = model.ProjectId,
            };

        public static BuildingModel ToModel(this BuildingFormModel form)
            => new()
            {
                Name = form.Name,
                Location = form.Location,
                ImageUrl = form.ImageUrl,
                ProjectId = form.ProjectId,
                Floors = form.Floors,
                WindSpeed = form.WindSpeed,
                Orientation = form.Orientation,
                TotalHeight = form.TotalHeight,
            };

        public static EditBuildingFormModel ToEditFormModel(this BuildingModel model)
            => new()
            {
                Id = model.Id,
                Name = model.Name,
                WindSpeed = model.WindSpeed,
                Floors = model.Floors,
                ImageUrl = model.ImageUrl ?? DefaultBuildingImageUrl,
                Location = model.Location,
                Orientation = model.Orientation,
                TotalHeight = model.TotalHeight,
                ProjectId = model.ProjectId,
            };

        public static BuildingDetailsViewModel ToDetailsView(this BuildingModel model)
            => new()
            {
                Id = model.Id,
                Name = model.Name,
                Location = model.Location,
                ImageUrl = model.ImageUrl ?? DefaultBuildingImageUrl,
                TotalHeight = model.TotalHeight,
                Floors = model.Floors,
                Orientation = model.Orientation,
                ProjectId = model.ProjectId,
                ProjectName = model.Project.Name,
                WindSpeed = model.WindSpeed
            };

        public static void UpdateFromForm(this BuildingModel model, EditBuildingFormModel form)
        {
            model.Name = form.Name;
            model.WindSpeed = form.WindSpeed;
            model.Floors = form.Floors;
            model.Location = form.Location;
            model.Orientation = form.Orientation;
            model.TotalHeight = form.TotalHeight;
            model.ProjectId = form.ProjectId;
        }
    }
}
