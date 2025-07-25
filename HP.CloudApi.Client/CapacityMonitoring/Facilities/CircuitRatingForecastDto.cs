using System;

namespace HeimdallPower.CapacityMonitoring.Facilities;

public class CircuitRatingForecastDto
{
    /// <summary>
    /// Timestamp for the predicted forecast.
    /// </summary>
    /// <example>2024-01-01T12:00:00Z</example>
    public DateTime Timestamp { get; init; }

    /// <summary>
    /// The base prediction value for the forecast.
    /// </summary>
    public ProbabilisticCircuitRatingDto Prediction { get; init; }

    /// <summary>
    /// The 80th percentile prediction value, representing an 80% confidence interval.
    /// </summary>
    public ProbabilisticCircuitRatingDto P80 { get; init; }

    /// <summary>
    /// The 90th percentile prediction value, representing a 90% confidence interval.
    /// </summary>
    public ProbabilisticCircuitRatingDto P90 { get; init; }

    /// <summary>
    /// The 95th percentile prediction value, representing a 95% confidence interval.
    /// </summary>
    public ProbabilisticCircuitRatingDto P95 { get; init; }

    /// <summary>
    /// The 99th percentile prediction value, representing a 99% confidence interval.
    /// </summary>
    public ProbabilisticCircuitRatingDto P99 { get; init; }
}