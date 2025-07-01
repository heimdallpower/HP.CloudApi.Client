using System;
using Newtonsoft.Json;

namespace HeimdallPower.Entities;

public sealed class ConductorTemperatureDto
{
    public string Unit {  get; }
    [JsonProperty("conductor_temperature")]
    public ConductorTemperature ConductorTemperatures { get; }

    public ConductorTemperatureDto(string unit, ConductorTemperature conductorTemperatures)
    {
        Unit = unit;
        ConductorTemperatures = conductorTemperatures;
    }
}

public sealed class ConductorTemperature
{
    public DateTime Timestamp { get; }
    public double Max { get; }
    public double Min { get; }

    public ConductorTemperature(DateTime timestamp, double max, double min)
    {
        Timestamp = timestamp;
        Max = max;
        Min = min;
    }
}