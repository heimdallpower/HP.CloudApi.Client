using System.Collections.Generic;
using Newtonsoft.Json;

namespace HeimdallPower.Entities;

public sealed class AarForecastDto
{
    [JsonProperty("aar_forecasts")]
    public List<ForecastDto> AarForecasts { get; }

    public AarForecastDto(List<ForecastDto> aarForecasts)
    {
        AarForecasts = aarForecasts;
    }
}