namespace HVACrate.Domain.ValueObjects
{
    public record Result<TItem>(int Count, TItem[] Items);
}
