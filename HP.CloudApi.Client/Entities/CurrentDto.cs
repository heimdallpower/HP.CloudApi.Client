using System;

namespace HeimdallPower.Entities;

public record CurrentDto
{
    public string Unit {  get; set; }
    public Current Current { get; set; }
}

public record Current
{
    public DateTime Timestamp { get; set; }
    public double Value { get; set; }
}