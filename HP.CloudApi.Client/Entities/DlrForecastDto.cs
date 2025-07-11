using System.Collections.Generic;

namespace HeimdallPower.Entities;

public sealed class DlrForecastDto
{
    public List<ForecastDto> DlrForecasts { get; }

    public DlrForecastDto(List<ForecastDto> dlrForecasts)
    {
        DlrForecasts = dlrForecasts;
    }
}