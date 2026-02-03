namespace HVACrate.Domain.ValueObjects
{
    public record Pagination(int Page = 1, int Limit = 10);
}