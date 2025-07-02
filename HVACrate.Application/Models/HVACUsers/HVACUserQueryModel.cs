using HVACrate.Domain.Enums;

namespace HVACrate.Application.Models.HVACUsers
{
    public class HVACUserQueryModel
    {
        public string? SearchParam { get; set; }

        public HVACUserSortingType? SortingType { get; set; }

        public SortingDirection? SortingDirection { get; set; }
    }
}
