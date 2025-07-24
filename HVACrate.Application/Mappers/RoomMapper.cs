using HVACrate.Application.Models.Projects;
using HVACrate.Application.Models.Rooms;
using HVACrate.Domain.Entities;
using HVACrate.Presentation.Models.Projects;
using HVACrate.Presentation.Models.Rooms;

namespace HVACrate.Application.Mappers
{
    public static class RoomMapper
    {
        public static RoomModel ToModel(this Room entity, bool firstTime = true)
            => new()
            {
                Id = entity.Id,
                Type = entity.Type,
                Number = entity.Number,
                Floor = entity.Floor,
                Temperature = entity.Temperature,
                IsEnclosed = entity.IsEnclosed,
                IsDeleted = entity.IsDeleted,
                BuildingId = entity.BuildingId,
                Building = firstTime ? entity.Building.ToModel(false) : null!,
                BuildingEnvelopes = firstTime ? entity.BuildingEnvelopes.Select(x => x.ToModel(false)).ToList() : null!,
            };

        public static Room ToEntity(this RoomModel model, bool firstTime = true)
            => new()
            {
                Id = model.Id,
                Type = model.Type,
                Number = model.Number,
                Floor = model.Floor,
                Temperature = model.Temperature,
                IsEnclosed = model.IsEnclosed,
                IsDeleted = model.IsDeleted,
                BuildingId = model.BuildingId,
                Building = firstTime ? model.Building.ToEntity(false) : null!,
                BuildingEnvelopes = firstTime ? model.BuildingEnvelopes.Select(x => x.ToEntity(false)).ToList() : null!
            };

        public static RoomViewModel ToView(this RoomModel model)
            => new()
            {
                Id = model.Id,
                Type = model.Type,
                Number = model.Number,
                Floor = model.Floor,
                IsEnclosed = model.IsEnclosed,
                BuildingId = model.BuildingId,
            };

        public static RoomModel ToModel(this RoomFormModel form)
           => new()
           {
               Type = form.Type,
               Number = form.Number,
               Temperature = form.Temperature,
               Floor = form.Floor,
               BuildingId = form.BuildingId,
           };

        public static RoomFormModel ToForm(this RoomModel model)
           => new()
           {
               Id = model.Id,
               Type = model.Type,
               Number = model.Number,
               Floor = model.Floor,
               Temperature = model.Temperature,
               BuildingId = model.BuildingId,
           };

        public static RoomDetailsViewModel ToDetailsViewModel(this RoomModel model)
           => new()
           {
               Id = model.Id,
               Type = model.Type,
               Number = model.Number,
               Floor = model.Floor,
               Temperature = model.Temperature,
               BuildingId = model.BuildingId,
               BuildingName = model.Building.Name,
               IsEnclosed = model.IsEnclosed,
           };

        public static void UpdateFromForm(this RoomModel model, RoomFormModel form)
        {
            model.Type = form.Type;
            model.Number = form.Number;
            model.Floor = form.Floor;
            model.Temperature = form.Temperature;
        }
    }
}
