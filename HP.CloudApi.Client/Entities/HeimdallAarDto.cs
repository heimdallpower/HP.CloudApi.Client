using System;

namespace HeimdallPower.Entities;

public class HeimdallAarDto
{
    public HeimdallAar Aar { get; set; }
}

public class HeimdallAar
{
    public DateTime Timestamp { get; set; }
    public double Value { get; set; }
}