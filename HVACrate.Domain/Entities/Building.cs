﻿namespace HVACrate.Domain.Entities
{
    public class Building : BaseEntity, IDeletableModel
    {
        public string Name { get; set; } = null!;

        public string Location { get; set; } = null!;

        public double TotalHeight { get; set; }

        public double WindSpeed { get; set; }

        public int Floors { get; set; }

        public string Orientation { get; set; } = null!;

        public string? ImageUrl { get; set; }

        public Guid ProjectId { get; set; }

        public virtual Project Project { get; set; } = null!;

        public bool IsDeleted { get; set; }

        public virtual ICollection<Room> Rooms { get; set; } = [];
    }
}
