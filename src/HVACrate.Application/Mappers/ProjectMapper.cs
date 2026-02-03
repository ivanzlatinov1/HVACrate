using HVACrate.Application.Models.Projects;
using HVACrate.Domain.Entities;
using HVACrate.Presentation.Models.Projects;

using static HVACrate.GCommon.GlobalConstants;

namespace HVACrate.Application.Mappers
{
    public static class ProjectMapper
    {
        public static ProjectModel ToModel(this Project entity, bool firstTime = true)
            => new()
            {
                Id = entity.Id,
                Name = entity.Name,
                RegionTemperature = entity.RegionTemperature,
                CreatedAt = entity.CreatedAt,
                LastModified = entity.LastModified,
                IsDeleted = entity.IsDeleted,
                HVACUserId = entity.HVACUserId,
                HVACUser = entity.HVACUser,
                Buildings = firstTime ? entity.Buildings.Select(b => b.ToModel(false)).ToList() : null!,
            };

        public static Project ToEntity(this ProjectModel model, bool firstTime = true)
            => new()
            {
                Id = model.Id,
                Name = model.Name,
                RegionTemperature = model.RegionTemperature,
                CreatedAt = model.CreatedAt,
                LastModified = model.LastModified,
                IsDeleted = model.IsDeleted,
                HVACUserId = model.HVACUserId,
                HVACUser = model.HVACUser,
                Buildings = firstTime ? model.Buildings.Select(b => b.ToEntity(false)).ToList() : null!,
            };

        public static ProjectViewModel ToView(this ProjectModel model)
            => new()
            {
                Id = model.Id,
                Name = model.Name,
                LastModified = model.LastModified.ToLocalTime().ToString(DescriptiveDateFormat),
            };

        public static ProjectFormModel ToForm(this ProjectModel model, Guid userId)
           => new()
           {
               Id = model.Id,
               Name = model.Name,
               RegionTemperature = model.RegionTemperature,
               UserId = userId,
           };

        public static ProjectModel ToModel(this ProjectFormModel form)
            => new()
            {
                Name = form.Name,
                RegionTemperature = form.RegionTemperature,
                HVACUserId = form.UserId,
            };

        public static void UpdateFromForm(this ProjectModel model, ProjectFormModel form)
        {
            model.Name = form.Name;
            model.RegionTemperature = form.RegionTemperature;
        }
    }
}
