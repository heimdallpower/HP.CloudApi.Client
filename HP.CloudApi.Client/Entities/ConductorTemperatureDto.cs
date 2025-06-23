using System;
using Newtonsoft.Json;

namespace HeimdallPower.Entities;

public class ConductorTemperatureDto
{
    public string Unit {  get; set; }
    [JsonProperty("conductor_temperatures")]
    public ConductorTemperatures ConductorTemperatures { get; set; }
}

public class ConductorTemperatures
{
    public DateTime Timestamp { get; set; }
    public double Max { get; set; }
    public double Min { get; set; }
}
