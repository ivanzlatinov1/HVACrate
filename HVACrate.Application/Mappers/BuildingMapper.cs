using HVACrate.Application.Models.Buildings;
using HVACrate.Domain.Entities;

namespace HVACrate.Application.Mappers
{
    internal static class BuildingMapper
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
    }
}
