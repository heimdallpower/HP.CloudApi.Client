using System.Collections.Generic;
using Newtonsoft.Json;

namespace HeimdallPower.Entities;

public sealed class DlrForecastDto
{
    [JsonProperty("dlr_forecasts")]
    public List<ForecastDto> DlrForecasts { get; }

    public DlrForecastDto(List<ForecastDto> dlrForecasts)
    {
        DlrForecasts = dlrForecasts;
    }
}