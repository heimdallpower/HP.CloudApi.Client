using System.Collections.Generic;

namespace HeimdallPower.Entities;

public record CircuitRatingForecastResponse(
    string Metric, 
    string Unit,
    IReadOnlyList<ForecastDto> CircuitRatingForecasts);