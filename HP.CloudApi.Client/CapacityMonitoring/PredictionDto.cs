using System;
using System.Text.Json.Serialization;

namespace HeimdallPower.CapacityMonitoring;

public record PredictionDto(
    [property: JsonPropertyName("value")] double Value, 
    [property: JsonPropertyName("span_id")] Guid SpanId);