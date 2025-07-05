using HVACrate.Domain.ValueObjects;

namespace HVACrate.Application.Models.Projects
{
    public class ProjectQueryModel
    {
        public string? SearchParam { get; set; }

        public Pagination Pagination { get; set; } = new();
    }
}
