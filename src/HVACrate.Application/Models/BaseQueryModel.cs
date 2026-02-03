using HVACrate.Domain.ValueObjects;

namespace HVACrate.Application.Models
{
    public class BaseQueryModel
    {
        public string? SearchParam { get; set; }

        public string QueryParam { get; set; } = string.Empty;

        public Pagination Pagination { get; set; } = new();
    }
}
