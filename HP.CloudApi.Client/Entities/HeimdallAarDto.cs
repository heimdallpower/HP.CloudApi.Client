using System;

namespace HeimdallPower.Entities;

public sealed class HeimdallAarDto
{
    public HeimdallAar Aar { get; }

    public HeimdallAarDto(HeimdallAar aar)
    {
        Aar = aar;
    }
}

public sealed class HeimdallAar
{
    public DateTime Timestamp { get; }
    public double Value { get; }

    public HeimdallAar(DateTime timestamp, double value)
    {
        Timestamp = timestamp;
        Value = value;
    }
}