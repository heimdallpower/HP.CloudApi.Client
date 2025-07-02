using System;

namespace HeimdallPower.Entities;

public sealed class HeimdallDlrDto
{
    public HeimdallDlr Dlr { get; }

    public HeimdallDlrDto(HeimdallDlr dlr)
    {
        Dlr = dlr;
    }
}

public sealed class HeimdallDlr
{
    public DateTime Timestamp { get; }
    public double Value { get; }

    public HeimdallDlr(DateTime timestamp, double value)
    {
        Timestamp = timestamp;
        Value = value;
    }
}