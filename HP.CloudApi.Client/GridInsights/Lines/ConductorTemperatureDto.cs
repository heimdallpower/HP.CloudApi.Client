using System;
using System.Text.Json.Serialization;

namespace HeimdallPower.GridInsights.Lines;

public record ConductorTemperatureDto(
    [property: JsonPropertyName("timestamp")] DateTime Timestamp, 
    [property: JsonPropertyName("max")] double Max, 
    [property: JsonPropertyName("min")] double Min);