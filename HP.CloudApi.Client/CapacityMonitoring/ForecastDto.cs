using System;
using System.Text.Json.Serialization;

namespace HeimdallPower.CapacityMonitoring;

public record ForecastDto(
    [property: JsonPropertyName("timestamp")] DateTime Timestamp, 
    [property: JsonPropertyName("prediction")] PredictionDto Prediction,
    [property: JsonPropertyName("p80")] PredictionDto P80,
    [property: JsonPropertyName("p90")]PredictionDto P90,
    [property: JsonPropertyName("p95")] PredictionDto P95,
    [property: JsonPropertyName("p99")] PredictionDto P99);