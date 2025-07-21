using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HeimdallPower.Assets;

public record LineDto(
    [property: JsonPropertyName("id")] Guid Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("available_forecast_hours")] int AvailableForecastHours,
    [property: JsonPropertyName("spans")] IReadOnlyCollection<SpanDto> Spans);