﻿namespace HVACrate.Presentation.Models.ViewModels.Projects
{
    public class ProjectViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public DateTimeOffset LastModified { get; set; }
    }
}
