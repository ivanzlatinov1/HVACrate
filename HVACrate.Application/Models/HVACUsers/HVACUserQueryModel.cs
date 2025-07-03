using HVACrate.Domain.ValueObjects;

namespace HVACrate.Application.Models.HVACUsers
{
    public class HVACUserQueryModel
    {
        public string? SearchParam { get; set; }

        public HVACUserSorting Sorting { get; set; } = new();

        public Pagination Pagination { get; set; } = new();
    }
}
