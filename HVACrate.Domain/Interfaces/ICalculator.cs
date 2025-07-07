namespace HVACrate.Domain.Interfaces
{
    public interface ICalculator
    {
        Task<double> CalculateTotalHeatTransmission(Guid id, CancellationToken cancellationToken);

        Task<double> CalculateTotalHeatInfiltration(Guid id, CancellationToken cancellationToken);
    }
}
