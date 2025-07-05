using HVACrate.Application.Models.Projects;
using HVACrate.Domain.Entities;
using HVACrate.Domain.Repositories;

using static HVACrate.GCommon.GlobalConstants.QueryProperties;

namespace HVACrate.Application.Mappers
{
    internal static class ProjectMapper
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
                Buildings = firstTime ? entity.Buildings.Select(b => b.ToModel()).ToList() : null!,
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
                Buildings = firstTime ? model.Buildings.Select(b => b.ToEntity()).ToList() : null!,
            };

        public static BaseQuery ToQuery(this ProjectQueryModel queryModel)
           => new()
           {
               SearchParam = queryModel.SearchParam,
               QueryProperty = ProjectQueryProperty,
               Pagination = queryModel.Pagination,
           };
    }
}
