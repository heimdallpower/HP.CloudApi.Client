using System;

namespace HeimdallPower.Entities;

public sealed class ForecastDto
{
    public DateTime Timestamp { get; }
    public PredictionDto Prediction { get; }
    public PredictionDto P80 { get; }
    public PredictionDto P90 { get; }
    public PredictionDto P95 { get; }
    public PredictionDto P99 { get; }

    public ForecastDto(DateTime timestamp, PredictionDto prediction,  PredictionDto p80, PredictionDto p90, PredictionDto p95, PredictionDto p99)
    {
        Timestamp = timestamp;
        Prediction = prediction;
        P80 = p80;
        P90 = p90;
        P95 = p95;
        P99 = p99;
    }
}