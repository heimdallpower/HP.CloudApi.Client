using System;
using System.Text.Json.Serialization;

namespace HeimdallPower.CapacityMonitoring.Facilities;

public record CircuitRatingDto(
    [property: JsonPropertyName("timestamp")] DateTime Timestamp, 
    [property: JsonPropertyName("value")] double Value);