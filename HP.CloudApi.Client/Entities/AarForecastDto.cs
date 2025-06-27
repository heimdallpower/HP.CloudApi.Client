using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace HeimdallPower.Entities;

public class AarForecastDto
{
    [JsonProperty("aar_forecasts")]
    public AarForecast AarForecast { get; set; }
}

public class AarForecast
{
    public DateTime UpdatedTimestamp { get; set; }
    public List<ForecastDto> Forecasts { get; set; }
}
