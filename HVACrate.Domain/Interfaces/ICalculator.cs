namespace HVACrate.Domain.Interfaces
{
    /// <summary>
    /// Provides calculation methods for determining total heat transmission and infiltration.
    /// </summary>
    public interface ICalculator
    {
        /// <summary>
        /// Calculates the total heat transmission for a given entity (e.g., room, building).
        /// </summary>
        /// <param name="id">The unique identifier of the entity (typically a room or building).</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        Task<double> CalculateTotalHeatTransmission(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Calculates the total heat infiltration for a given entity (e.g., room, building).
        /// </summary>
        /// <param name="id">The unique identifier of the entity (typically a room or building).</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        Task<double> CalculateTotalHeatInfiltration(Guid id, CancellationToken cancellationToken);
    }
}
