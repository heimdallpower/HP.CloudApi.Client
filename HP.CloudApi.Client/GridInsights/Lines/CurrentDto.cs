using System;
using System.Text.Json.Serialization;

namespace HeimdallPower.GridInsights.Lines;

public record CurrentDto(
    [property: JsonPropertyName("timestamp")] DateTime Timestamp, 
    [property: JsonPropertyName("value")] double Value);