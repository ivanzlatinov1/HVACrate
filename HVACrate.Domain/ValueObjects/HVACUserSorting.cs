namespace HVACrate.Domain.ValueObjects
{
    public record HVACUserSorting
    (
        HVACUserSortingType Type = HVACUserSortingType.RegisteredOn,
        SortingDirection Direction = SortingDirection.Descending
    );
}
