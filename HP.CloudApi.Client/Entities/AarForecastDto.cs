using System.Collections.Generic;

namespace HeimdallPower.Entities;

public sealed class AarForecastDto
{
    public List<ForecastDto> AarForecasts { get; }

    public AarForecastDto(List<ForecastDto> aarForecasts)
    {
        AarForecasts = aarForecasts;
    }
}