using HVACrate.Domain.ValueObjects;

namespace HVACrate.Domain.Repositories
{
    public class BaseQuery
    {
        public string? SearchParam { get; set; }

        public string QueryProperty { get; set; } = string.Empty;

        public Pagination Pagination { get; set; } = new();
    }
}
