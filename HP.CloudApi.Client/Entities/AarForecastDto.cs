using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace HeimdallPower.Entities;

public sealed class AarForecastDto
{
    [JsonProperty("aar_forecasts")]
    public List<AarForecast> AarForecasts { get; }

    public AarForecastDto(List<AarForecast> aarForecasts)
    {
        AarForecasts = aarForecasts;
    }
}

public sealed class AarForecast
{
    public DateTime Timestamp { get; }
    public PredictionDto Prediction { get; }

    public AarForecast(DateTime timestamp, PredictionDto prediction)
    {
        Timestamp = timestamp;
        Prediction = prediction;
    }
}