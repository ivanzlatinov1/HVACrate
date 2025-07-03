using HVACrate.Domain.Enums;

namespace HVACrate.Presentation.Models.FormModels
{
    public class HVACUserQueryFormModel
    {
        public string? SearchParam { get; set; }

        public HVACUserSortingType SortingType { get; set; }

        public SortingDirection SortingDirection { get; set; }

        public int Page { get; set; } = 1;

        public int Limit { get; set; } = 5;
    }
}
