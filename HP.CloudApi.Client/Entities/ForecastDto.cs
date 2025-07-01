using System;

namespace HeimdallPower.Entities;

public sealed class ForecastDto
{
    public DateTime Timestamp { get; }
    public PredictionDto Prediction { get; }

    public ForecastDto(DateTime timestamp, PredictionDto prediction)
    {
        Timestamp = timestamp;
        Prediction = prediction;
    }
}