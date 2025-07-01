using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace HeimdallPower.Entities;

public class AarForecastDto
{
    [JsonProperty("aar_forecasts")]
    public List<AarForecast> AarForecast { get; set; }
}

public class AarForecast
{
    public DateTime Timestamp { get; set; }
    public PredictionDto Prediction { get; set; }
}