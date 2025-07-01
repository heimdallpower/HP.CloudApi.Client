using System;

namespace HeimdallPower.Entities;

public sealed class CurrentDto
{
    public string Unit {  get;}
    public Current Current { get; }
    
    public CurrentDto(string unit, Current current)
    {
        Unit = unit;
        Current = current;
    }
}

public sealed class Current
{
    public DateTime Timestamp { get; }
    public double Value { get; }

    public Current(DateTime timestamp, double value)
    {
        Timestamp = timestamp;
        Value = value;
    }
}