using HVACrate.Application.Models.HVACUsers;
using HVACrate.Domain.Entities;

namespace HVACrate.Application.Mappers
{
    internal static class UserMapper
    {
        public static HVACUserModel ToModel(this HVACUser entity, bool firstTime = true)
            => new()
            {
                Id = entity.Id,
                Username = entity.Username,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email,
                ProfilePictureUrl = entity.ProfilePictureUrl,
                RegisteredOn = entity.RegisteredOn,
                Projects = firstTime ? entity.Projects.Select(p => p.ToModel(false)).ToList() : null!
            };

        public static HVACUser ToEntity(this HVACUserModel model, bool firstTime = true)
            => new()
            {
                Id = model.Id,
                UserName = model.Username,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                ProfilePictureUrl = model.ProfilePictureUrl,
                RegisteredOn = model.RegisteredOn,
                Projects = firstTime ? model.Projects.Select(p => p.ToEntity(false)).ToList() : null!
            };
    }
}
