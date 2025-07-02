using HVACrate.Application.Models.Projects;
using HVACrate.Domain.Entities;

namespace HVACrate.Application.Mappers
{
    internal static class ProjectMapper
    {
        public static ProjectModel ToModel(this Project entity, bool firstTime = true)
            => new()
            {
            };

        public static Project ToEntity(this ProjectModel model, bool firstTime = true)
            => new()
            {
            };
    }
}
