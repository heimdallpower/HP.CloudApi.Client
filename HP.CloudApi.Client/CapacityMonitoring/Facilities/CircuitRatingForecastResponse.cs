using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HeimdallPower.CapacityMonitoring.Facilities;

public record CircuitRatingForecastResponse(
    [property: JsonPropertyName("metric")] string Metric, 
    [property: JsonPropertyName("unit")] string Unit,
    [property: JsonPropertyName("circuit_rating_forecasts")] IReadOnlyList<ForecastDto> CircuitRatingForecasts);