using System.Collections.Generic;
using Newtonsoft.Json;

namespace HeimdallPower.Entities;

public record CircuitRatingForecastDto([JsonProperty("circuit_rating_forecasts")]IReadOnlyList<ForecastDto> CircuitRatingForecasts);