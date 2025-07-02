using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace HeimdallPower.Entities;

public class LineDto
{
    public Guid Id { get; set; }
    public string Name;
    [JsonProperty("available_forecast_hours")]
    public int AvailableForecastHours;
    public string Owner {get; set;}
    public List<SpanDto> Spans;
}
