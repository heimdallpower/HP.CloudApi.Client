using System.Collections.Generic;
using Newtonsoft.Json;

namespace HeimdallPower.Entities;

public record CircuitRatingForecastResponse(
    string Metric, 
    string Unit,
    [JsonProperty("circuit_rating_forecasts")]IReadOnlyList<ForecastDto> CircuitRatingForecasts);