using HVACrate.Application.Models;
using HVACrate.Domain.Entities;

namespace HVACrate.Application.Mappers
{
    internal static class UserMapper
    {
        public static HVACUserModel ToModel(this HVACUser entity, bool firstTime = true)
            => new()
            {
                Id = entity.Id,
                UserName = entity.UserName,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email,
                ProfilePictureUrl = entity.ProfilePictureUrl,
                Projects = firstTime ? entity.Projects.Select(p => p.ToModel()).ToList() : null!
            };

        public static HVACUser ToEntity(this HVACUserModel model, bool firstTime = true)
            => new()
            {
                Id = model.Id,
                UserName = model.UserName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                ProfilePictureUrl = model.ProfilePictureUrl,
                Projects = firstTime ? model.Projects.Select(p => p.ToEntity()).ToList() : null!
            };
    }
}
