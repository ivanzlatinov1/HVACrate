using HVACrate.Domain.ValueObjects;

namespace HVACrate.Domain.Repositories.Users
{
    public class HVACUserQuery
    {
        public string? SearchParam { get; set; }

        public HVACUserSorting Sorting { get; set; } = new();

        public Pagination Pagination { get; set; } = new();
    }
}
