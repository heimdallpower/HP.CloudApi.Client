using System;
using Newtonsoft.Json;

namespace HeimdallPower.Entities;

public record ConductorTemperatureDto
{
    public string Unit {  get; set; }
    [JsonProperty("conductor_temperature")]
    public ConductorTemperature ConductorTemperatures { get; set; }
}

public record ConductorTemperature
{
    public DateTime Timestamp { get; set; }
    public double Max { get; set; }
    public double Min { get; set; }
}